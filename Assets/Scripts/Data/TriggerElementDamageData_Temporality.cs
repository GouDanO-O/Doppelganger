using System;
using System.Collections.Generic;
using GameFrame.World;
using QFramework;
using UnityEngine;

namespace GameFrame
{
    public struct SElementTriggerData
    {
        public int curLevel;

        public int maxLevel;
        
        private float maxDuration;
        
        private float lastInterval;

        private float intervalTime;

        private float lastDuration;

        public void SetElementTriggerTime(float duration, float intervalTime)
        {
            this.maxDuration = duration;
            this.lastDuration = duration;
            this.lastInterval = intervalTime;
            this.intervalTime = intervalTime;
        }

        public void UpdateIntervalTime(float intervalTime)
        {
            this.intervalTime = intervalTime;
        }

        public void UpdateDuration(float duration)
        {
            if (maxDuration < duration)
            {
                this.maxDuration = duration;
                this.lastDuration = duration;
            }
        }
        
        public void CaculateInterval(float deltaTime)
        {
            lastInterval -= deltaTime;
            lastDuration -= deltaTime;
            if (lastInterval <= 0)
            {
                lastInterval = intervalTime;
            }
        }
        
        public void SetLevel(int maxLevel)
        {
            if (this.maxLevel < maxLevel)
            {
                this.maxLevel = maxLevel;
            }

            if (maxLevel == 0)
            {
                this.curLevel = 0;
            }
        }
        
        public void AddLevel()
        {
            curLevel ++;
            if (curLevel >= maxLevel)
            {
                curLevel = maxLevel;
            }
        }

        public bool IsTimeOver()
        {
            return lastDuration <= 0;
        }
    }

    /// <summary>
    /// 元素伤害计算块
    /// 所有玩家施加的同类型元素不会进行叠加,伤害以最高伤害为基准,最高层数以被施加的最高层数为基准
    /// 并刷新总持续时长(间隔时间不会刷新且当前累计层数也不会刷新)
    /// </summary>
    public class TriggerElementDamageData_Temporality : TemporalityData_Pool,ICanSendCommand
    {
        /// <summary>
        /// 当前元素数据
        /// </summary>
        private Dictionary<EElementType, SElementTriggerData> elementDataDic =
            new Dictionary<EElementType, SElementTriggerData>();
        
        private Queue<EElementType> expiredElements = new Queue<EElementType>();

        private HealthyController healthyController;
        
        public static TriggerElementDamageData_Temporality Allocate()
        {
            return SafeObjectPool<TriggerElementDamageData_Temporality>.Instance.Allocate();
        }

        /// <summary>
        /// 设置所有者
        /// </summary>
        /// <param name="healthyController"></param>
        public void SetOwner(HealthyController healthyController)
        {
            this.healthyController = healthyController;
            ElementCaculateManager.onAddElementEffecterEvent.Invoke(this);
        }
        
        /// <summary>
        /// 添加元素效果（增加层数）
        /// </summary>
        /// <param name="element"></param>
        public void AddElement(EElementType element,ElementDamageData_Persistent elementDamageData)
        {
            if (elementDataDic.TryGetValue(element, out var elementData))
            {
                elementData.AddLevel();
                elementDataDic[element] = elementData; 
            }
            else
            {
                SElementTriggerData newData = new SElementTriggerData();
                newData.SetLevel(elementDamageData.MaxElementAccLevel);
                newData.SetElementTriggerTime(elementDamageData.MaxElementDuration,elementDamageData.BasicElementTriggerInterval);
                elementDataDic[element] = newData;
            }
        }

        // 主动更新当前元素的持续时间，计算伤害
        public void UpdateElementDuration(float deltaTime)
        {
            if (elementDataDic.Count > 0)
            {
                foreach (var pair in elementDataDic)
                {
                    var element = pair.Key;
                    var elementTime = pair.Value;
                    
                    elementTime.CaculateInterval(deltaTime);
                    
                    if (elementTime.IsTimeOver())
                    {
                        expiredElements.Enqueue(element);
                        HandleElementExpire(element);
                    }
                    else
                    {
                        ApplyElementDamageToTarget(element);
                    }
                }

                while (expiredElements.Count > 0)
                {
                    var element = expiredElements.Dequeue();
                    elementDataDic.Remove(element);
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

        }

        /// <summary>
        /// 处理伤害应用逻辑
        /// </summary>
        /// <param name="damage"></param>
        private void ApplyElementDamageToTarget(EElementType element)
        {

            
        }

        
        public override void DeInitData()
        {
            elementDataDic.Clear();
        }

        public override void OnRecycled()
        {
            DeInitData();
            ElementCaculateManager.onRemoveElementEffecterEvent.Invoke(this);
        }

        public override void Recycle2Cache()
        {
            SafeObjectPool<TriggerElementDamageData_Temporality>.Instance.Recycle(this);
        }

        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }
    }
}