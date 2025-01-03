using System;
using System.Collections.Generic;
using GameFrame.World;
using QFramework;
using UnityEngine;

namespace GameFrame
{
    /// <summary>
    /// 元素伤害计算块
    /// 所有玩家施加的同类型元素不会进行叠加,伤害以最高伤害为基准,最高层数以被施加的最高层数为基准
    /// 并刷新总持续时长(间隔时间不会刷新且当前累计层数也不会刷新)
    /// </summary>
    public class TriggerElementDamageData_TemporalityPoolable : TemporalityData_Pool
    {
        /// <summary>
        /// 当前元素配置数据
        /// </summary>
        private Dictionary<EElementType, SElementTriggerData> elementDataDict =
            new Dictionary<EElementType, SElementTriggerData>();
        
        /// <summary>
        /// 当前要消失的元素队列
        /// </summary>
        private Queue<EElementType> expiredElementsQueue = new Queue<EElementType>();
        
        /// <summary>
        /// 当前元素伤害数据
        /// </summary>
        private Dictionary<EElementType, TriggerDamageData_TemporalityPoolable> elementDamageDataDict =
            new Dictionary<EElementType, TriggerDamageData_TemporalityPoolable>();
        
        public static TriggerElementDamageData_TemporalityPoolable Allocate()
        {
            return SafeObjectPool<TriggerElementDamageData_TemporalityPoolable>.Instance.Allocate();
        }
        
        /// <summary>
        /// 设置施加者和受害者
        /// 叠加元素类型
        /// </summary>
        /// <param name="healthyController"></param>
        public void UpdateSuffererAndEnforcer(WorldObj enforcer,WorldObj sufferer,EElementType elementType)
        {
            //如果当前受害者已经有这个元素伤害块,则更新这个元素伤害块的施加者(覆盖式伤害)
            if (elementDamageDataDict.TryGetValue(elementType,out TriggerDamageData_TemporalityPoolable curDamageData))
            { 
                curDamageData = elementDamageDataDict[elementType];
                curDamageData.UpdateEnforcer(enforcer);
            }
            else
            { 
                curDamageData = TriggerDamageData_TemporalityPoolable.Allocate();
                curDamageData.InitDamageData(enforcer,sufferer,0,elementType);
            }
            
            AddElement(elementType, enforcer);
            ElementCaculateManager.onAddElementEffecterEvent.Invoke(this);
        }
        
        /// <summary>
        /// 添加元素效果（增加层数）
        /// </summary>
        /// <param name="element"></param>
        private void AddElement(EElementType element,WorldObj enforcer)
        {
            int curLevel = 0;
            ElementDamageData_Persistent elementDamageData=enforcer.worldObjPropertyDataTemporality.GetElementDamageData(element);
            if (elementDataDict.TryGetValue(element, out var elementData))
            {
                curLevel = elementData.AddLevel();
            }
            else
            {
                elementData = new SElementTriggerData();
                curLevel = elementData.SetLevel(elementDamageData.MaxElementAccLevel);
                elementData.SetElementTriggerTime(elementDamageData.MaxElementDuration,elementDamageData.BasicElementTriggerInterval);
            }
            elementDataDict[element] = elementData;
            elementDamageDataDict[element].UpdateElementDamage(curLevel);
        }
        
        /// <summary>
        /// 由管理器更新当前元素的持续时间
        /// 到达间隔时间就计算伤害
        /// </summary>
        /// <param name="deltaTime"></param>
        public void UpdateElementDuration(float deltaTime)
        {
            if (elementDataDict.Count > 0)
            {
                foreach (var pair in elementDataDict)
                {
                    var element = pair.Key;
                    var elementTime = pair.Value;
                    
                    elementTime.CaculateInterval(deltaTime);
                    
                    if (elementTime.IsTimeOver())
                    {
                        expiredElementsQueue.Enqueue(element);
                        HandleElementExpire(element);
                    }
                    else
                    {
                        ApplyElementDamageToTarget(element);
                    }
                }

                while (expiredElementsQueue.Count > 0)
                {
                    var element = expiredElementsQueue.Dequeue();
                    elementDataDict.Remove(element);
                }
            }
        }

        /// <summary>
        /// 元素过期时的处理
        /// 这里可以做一些额外的逻辑，比如播放特效、触发事件等
        /// </summary>
        /// <param name="element"></param>
        private void HandleElementExpire(EElementType element)
        {
            elementDamageDataDict[element].Recycle2Cache();
            elementDataDict.Remove(element);
            elementDamageDataDict.Remove(element);
        }

        /// <summary>
        /// 处理伤害应用逻辑
        /// </summary>
        /// <param name="damage"></param>
        private void ApplyElementDamageToTarget(EElementType element)
        {
            if (elementDamageDataDict.TryGetValue(element, out TriggerDamageData_TemporalityPoolable curDamageData))
            {
                if (curDamageData.IsRecycled)
                {
                    return;
                }
                else
                {
                    curDamageData.HarmedSufferer();
                }
            }
            
        }
        
        public override void DeInitData()
        {
            elementDataDict.Clear();
            foreach (var elementDamage in elementDamageDataDict)
            {
                elementDamage.Value.Recycle2Cache();
            }
            elementDamageDataDict.Clear();
        }

        public override void OnRecycled()
        {
            DeInitData();
            ElementCaculateManager.onRemoveElementEffecterEvent?.Invoke(this);
        }

        public override void Recycle2Cache()
        {
            SafeObjectPool<TriggerElementDamageData_TemporalityPoolable>.Instance.Recycle(this);
        }
    }
}