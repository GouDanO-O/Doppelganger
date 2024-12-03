using System.Collections.Generic;
using QFramework;

namespace GameFrame
{
    public class ProjectileTriggerDamageData_TemporalityPoolable : TriggerDamageData_TemporalityPoolable
    {
        /// <summary>
        /// 当前飞行速度
        /// </summary>
        public float curFlySpeed { get;private set; }

        /// <summary>
        /// 当前飞行距离
        /// </summary>
        public float curFlyDistance{ get;private set; }
        
        /// <summary>
        /// 最大飞行距离
        /// </summary>
        public float maxFlyDistance{ get;private set; }

        /// <summary>
        /// 当前飞行高度
        /// </summary>
        public float curFlyHeight{ get;private set; }

        /// <summary>
        /// 飞行最高点
        /// </summary>
        public float maxFlyHeight{ get;private set; }
        
        
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
        /// 更新伤害衰减等级
        /// </summary>
        /// <param name="damageAttenuationLevel"></param>
        public void UpdateDamageAttenuationLevel(List<float> damageAttenuationLevel)
        {
            this.damageAttenuationLevel = damageAttenuationLevel;
            this.curDamageAttenuationLevel = damageAttenuationLevel.Count;
        }
        
        /// <summary>
        /// 更新最大飞行距离
        /// </summary>
        /// <param name="flyDistance"></param>
        public void UpdateMaxFlyDistance(float flyDistance)
        {
            this.maxFlyDistance = flyDistance;
        }

        /// <summary>
        /// 更新飞行速度
        /// </summary>
        /// <param name="flySpeed"></param>
        public void UpdateFlySpeed(float flySpeed)
        {
            this.curFlySpeed = flySpeed;    
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
        
        /// <summary>
        /// 增加伤害衰减等级(要在造成伤害之后才计算)
        /// </summary>
        public void AddDamageAttenuationLevel()
        {
            if (maxDamageAttenuationLevel > 0)
                curDamageAttenuationLevel++;
        }

        /// <summary>
        /// 飞行
        /// </summary>
        /// <param name="distance"></param>
        public void AddFlyDistance(float distance)
        {
            this.curFlyDistance += distance;
        }

        /// <summary>
        /// 是否抵达最大距离
        /// </summary>
        /// <returns></returns>
        public bool IsArriveMaxDistance()
        {
            return curFlyHeight >= maxFlyDistance;
        }

        public override void DeInitData()
        {
            base.DeInitData();
            curDamageAttenuationLevel = 0;
            maxDamageAttenuationLevel = 0;
            damageAttenuationLevel.Clear();
        }

        public static ProjectileTriggerDamageData_TemporalityPoolable Allocate()
        {
            return SafeObjectPool<ProjectileTriggerDamageData_TemporalityPoolable>.Instance.Allocate();
        }
        
        
    }
}