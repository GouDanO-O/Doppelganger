using System;
using System.Collections.Generic;
using GameFrame.World;
using QFramework;
using UnityEngine;

namespace GameFrame
{
    public struct SElementTriggerTime
    {
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
        
        public void CaculateInterval(float deltaTime)
        {
            lastInterval -= deltaTime;
            lastDuration -= deltaTime;
            if (lastInterval <= 0)
            {
                lastInterval = intervalTime;
            }
        }

        public bool IsTimeOver()
        {
            return lastDuration <= 0;
        }
    }

    public struct SElementLevel
    {
        public int curLevel;

        public int maxLevel;

        public void SetLevel(int maxLevel)
        {
            this.maxLevel = maxLevel;
            this.curLevel = 0;
        }
        
        public void AddLevel()
        {
            curLevel ++;
            if (curLevel >= maxLevel)
            {
                curLevel = maxLevel;
            }
        }
    }

    public class TriggerElementDamageData_Temporality : TemporalityData_Pool,ICanSendCommand
    {
        /// <summary>
        /// 当前元素等级
        /// </summary>
        private Dictionary<EElementType, SElementLevel> elementLevelDic =
            new Dictionary<EElementType, SElementLevel>();

        /// <summary>
        /// 当前元素伤害
        /// </summary>
        private Dictionary<EElementType, float> elementDamageDic =
            new Dictionary<EElementType, float>();

        /// <summary>
        /// 当前元素间隔剩余触发时长
        /// </summary>
        private Dictionary<EElementType, SElementTriggerTime> elementTimeDic =
            new Dictionary<EElementType, SElementTriggerTime>();
        
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
        public void AddElement(EElementType element, int maxLevel = 0)
        {
            if (elementLevelDic.TryGetValue(element, out var levelData))
            {
                levelData.AddLevel();
                elementLevelDic[element] = levelData; 
            }
            else
            {
                SElementLevel newLevelData = new SElementLevel();
                newLevelData.SetLevel(maxLevel);
                elementLevelDic[element] = newLevelData;
            }
        }

        
        /// <summary>
        /// 添加元素效果（增加层数）
        /// </summary>
        /// <param name="element"></param>
        public void AddElement(EElementType element,ElementDamageData_Persistent elementData)
        {
            if (elementLevelDic.TryGetValue(element, out var levelData))
            {
                levelData.AddLevel();
                elementLevelDic[element] = levelData; 
            }
            else
            {
                SElementLevel newLevelData = new SElementLevel();
                newLevelData.SetLevel(elementData.MaxElementAccLevel);
                elementLevelDic[element] = newLevelData; 
            }
        }

        // 主动更新当前元素的持续时间，计算伤害
        public void UpdateElementDuration(float deltaTime)
        {
            if (elementLevelDic.Count > 0)
            {
                foreach (var pair in elementLevelDic)
                {
                    var element = pair.Key;
                    var elementTime = elementTimeDic[element];
                    
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
                    elementLevelDic.Remove(element);
                    elementDamageDic.Remove(element);
                    elementTimeDic.Remove(element);
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
            elementLevelDic.Clear();
            elementDamageDic.Clear();
            elementTimeDic.Clear();
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