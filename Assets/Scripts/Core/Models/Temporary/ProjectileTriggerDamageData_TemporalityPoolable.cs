using System.Collections.Generic;
using QFramework;

namespace GameFrame
{
    public class ProjectileTriggerDamageData_TemporalityPoolable : TriggerDamageData_TemporalityPoolable
    {
        /// <summary>
        /// 当前飞行速度
        /// </summary>
        public float CurFlySpeed { get; private set; }

        /// <summary>
        /// 当前飞行距离
        /// </summary>
        public float CurFlyDistance { get; private set; }

        /// <summary>
        /// 最大飞行距离
        /// </summary>
        public float MaxFlyDistance { get; private set; }

        /// <summary>
        /// 当前飞行高度
        /// </summary>
        public float CurFlyHeight { get; private set; }

        /// <summary>
        /// 飞行最高点
        /// </summary>
        public float MaxFlyHeight { get; private set; }

        /// <summary>
        /// 当前伤害过敌人的数量
        /// </summary>
        public int CurDamageAttenuationLevel { get; private set; }

        /// <summary>
        /// 最大伤害敌人数量
        /// </summary>
        public int MaxDamageAttenuationLevel { get; private set; }

        /// <summary>
        /// 伤害衰减等级列表
        /// </summary>
        public List<float> DamageAttenuationLevel { get; private set; } = new List<float>();
        
        /// <summary>
        /// 飞行方式
        /// </summary>
        public EAction_Projectile_ShootType ShootType { get; private set; }

        /// <summary>
        /// 更新伤害衰减等级
        /// </summary>
        /// <param name="damageAttenuationLevel"></param>
        public void UpdateDamageAttenuationLevel(List<float> damageAttenuationLevel)
        {
            if (damageAttenuationLevel == null || damageAttenuationLevel.Count == 0)
            {
                // 如果传入的列表为空，初始化为默认值
                DamageAttenuationLevel = new List<float> { 1.0f };
            }
            else
            {
                DamageAttenuationLevel = new List<float>(damageAttenuationLevel);
            }

            CurDamageAttenuationLevel = 0;
            MaxDamageAttenuationLevel = DamageAttenuationLevel.Count;
        }

        /// <summary>
        /// 更新最大飞行距离
        /// </summary>
        /// <param name="flyDistance"></param>
        public void UpdateMaxFlyDistance(float flyDistance)
        {
            MaxFlyDistance = flyDistance;
        }

        /// <summary>
        /// 更新飞行速度
        /// </summary>
        /// <param name="flySpeed"></param>
        public void UpdateFlySpeed(float flySpeed)
        {
            CurFlySpeed = flySpeed;
        }
        
        /// <summary>
        /// 更新当前飞行高度
        /// </summary>
        /// <param name="flyHeight"></param>
        public void UpdateFlyHeight(float flyHeight)
        {
            CurFlyHeight = flyHeight;
        }

        /// <summary>
        /// 更新飞行最高点
        /// </summary>
        /// <param name="maxFlyHeight"></param>
        public void UpdateMaxFlyHeight(EAction_Projectile_ShootType shootType,float maxFlyHeight)
        {
            this.ShootType = shootType;
            MaxFlyHeight = maxFlyHeight;
        }

        /// <summary>
        /// 检查当前伤害衰减等级，从而计算是否要进行伤害衰减计算
        /// </summary>
        /// <returns></returns>
        public virtual bool CheckDamageAttenuationLevel()
        {
            if (MaxDamageAttenuationLevel == 0)
                return false;
            return CurDamageAttenuationLevel >= MaxDamageAttenuationLevel;
        }

        /// <summary>
        /// 增加伤害衰减等级（要在造成伤害之后才计算）
        /// </summary>
        public void AddDamageAttenuationLevel()
        {
            if (CurDamageAttenuationLevel < MaxDamageAttenuationLevel)
            {
                CurDamageAttenuationLevel++;
            }
        }

        /// <summary>
        /// 获取当前伤害衰减系数
        /// </summary>
        /// <returns></returns>
        public float GetCurDamageAttenuation()
        {
            if (CurDamageAttenuationLevel >= DamageAttenuationLevel.Count)
            {
                // 防止超出范围，返回最后一个衰减系数
                return DamageAttenuationLevel[DamageAttenuationLevel.Count - 1];
            }

            return DamageAttenuationLevel[CurDamageAttenuationLevel];
        }

        /// <summary>
        /// 飞行距离增加
        /// </summary>
        /// <param name="distance"></param>
        public void AddFlyDistance(float distance)
        {
            CurFlyDistance += distance;
        }

        /// <summary>
        /// 是否抵达最大距离
        /// </summary>
        /// <returns></returns>
        public bool IsArriveMaxDistance()
        {
            return CurFlyDistance >= MaxFlyDistance;
        }

        protected override float CaculateFinalDamage_Normal()
        {
            return base.CaculateFinalDamage_Normal() * GetCurDamageAttenuation();
        }

        protected override float CaculateFinalDamage_Element()
        {
            return base.CaculateFinalDamage_Element() * GetCurDamageAttenuation();
        }

        public override void DeInitData()
        {
            base.DeInitData();
            CurFlyDistance = 0;
            CurFlyHeight = 0;
            CurFlySpeed = 0;
            MaxFlyHeight = 0;
            MaxFlyDistance = 0;
            CurDamageAttenuationLevel = 0;
            MaxDamageAttenuationLevel = 0;
            DamageAttenuationLevel.Clear();
        }

        public static ProjectileTriggerDamageData_TemporalityPoolable Allocate()
        {
            return SafeObjectPool<ProjectileTriggerDamageData_TemporalityPoolable>.Instance.Allocate();
        }
    }
}
