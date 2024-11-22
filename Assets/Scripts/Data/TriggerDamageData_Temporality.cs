using System;
using System.Collections.Generic;
using QFramework;
using Random = UnityEngine.Random;

namespace GameFrame.World
{
    public class TriggerDamageData_Temporality : TemporalityData_Pool
    {
        /// <summary>
        /// 当前会造成的元素类型
        /// </summary>
        public EAction_Skill_ElementType curElementType;
        
        /// <summary>
        /// 当前会造成的伤害
        /// </summary>
        public float curBasicDamage;

        /// <summary>
        /// 当前暴击率
        /// </summary>
        public float curCriticalRate;

        /// <summary>
        /// 当前暴击伤害
        /// </summary>
        public float curCriticalDamage;

        /// <summary>
        /// 当前伤害过敌人的数量
        /// </summary>
        public int curDamageAttenuationLevel;

        /// <summary>
        /// 最大伤害敌人数量
        /// </summary>
        public int maxDamageAttenuationLevel;
        
        /// <summary>
        /// 伤害衰减等级
        /// </summary>
        public List<float> damageAttenuationLevel;

        /// <summary>
        /// 计算当前伤害
        /// </summary>
        /// <param name="damageAttenuationRate"></param>
        /// <returns></returns>
        public float CaculateDamage(float damageAttenuationRate = 1)
        {
            float randomCriticalRate = Random.Range(curCriticalRate, 100);
            bool isCritical = randomCriticalRate <= curCriticalRate;
            float willTriggerDamage = (isCritical ? curBasicDamage * curCriticalDamage : curBasicDamage) *
                                      damageAttenuationRate;
            
            if (maxDamageAttenuationLevel > 0 && curDamageAttenuationLevel < maxDamageAttenuationLevel)
            {
                willTriggerDamage *= damageAttenuationLevel[curDamageAttenuationLevel];
            }
            
            return willTriggerDamage;
        }

        /// <summary>
        /// 增加伤害衰减等级(要在造成伤害之后才计算)
        /// </summary>
        public void AddDamageAttenuationLevel()
        {
            if (maxDamageAttenuationLevel > 0)
                curDamageAttenuationLevel++;
        }

        /// <summary>
        /// 检查当前伤害衰减等级,从而计算是否要进行伤害衰减计算
        /// </summary>
        /// <returns></returns>
        public virtual bool CheckDamageAttenuationLevel()
        {
            if (maxDamageAttenuationLevel == 0)
                return false;
            return curDamageAttenuationLevel >= maxDamageAttenuationLevel;
        }

        public static TriggerDamageData_Temporality Allocate()
        {
            return SafeObjectPool<TriggerDamageData_Temporality>.Instance.Allocate();
        }
        
        public override void OnRecycled()
        {
            DeInitData();
        }
        
        public override void Recycle2Cache()
        {
            
        }

        public override void DeInitData()
        {
            curBasicDamage = 0;
            curCriticalDamage = 0;
            curCriticalRate = 0;
            curDamageAttenuationLevel = 0;
            maxDamageAttenuationLevel = 0;
        }
    }
}