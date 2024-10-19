using System.Collections.Generic;
using GameFrame.World;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    public class SCommonProjectileData : IData_Temporality
    {
        /// <summary>
        /// 当前会造成的伤害
        /// </summary>
        public float curWillTriggerDamage;

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
        /// 当前飞行距离
        /// </summary>
        public float curFlyDistance;

        /// <summary>
        /// 计算当前伤害
        /// </summary>
        /// <param name="damageAttenuationRate"></param>
        /// <returns></returns>
        public float CaculateDamage(float damageAttenuationRate=1)
        {
            float randomCriticalRate = Random.Range(curCriticalRate, 100);
            bool isCritical = randomCriticalRate <= curCriticalRate;
            return (isCritical ? curWillTriggerDamage * curCriticalDamage : curWillTriggerDamage)*damageAttenuationRate;
        }

        /// <summary>
        /// 检查当前伤害衰减等级
        /// </summary>
        /// <returns></returns>
        public bool CheckDamageAttenuationLevel()
        {
            if (maxDamageAttenuationLevel == 0)
                return false;
            curDamageAttenuationLevel++;
            return curDamageAttenuationLevel >= maxDamageAttenuationLevel;
        }

        public void ClearData()
        {
            curDamageAttenuationLevel = 0;
            curFlyDistance = 0;
        }
    }
    
    /// <summary>
    /// 普通弹体类型
    /// </summary>
    [CreateAssetMenu(fileName = "CommonProjectile",menuName = "配置/技能/行为/弹体/普通弹体")]
    public class SActionClip_DetailAction_CommonProjectile : SActionClip_DetailAction_Basic
    {
        [LabelText("发射次数"),MinValue(1)]
        public int FireCount;

        [LabelText("飞行速度")]
        public float MoveSpeed;
        
        [ShowIf("@FireCount >1"),LabelText("开火间隔"),MinValue(0)]
        public float FireDelayTime;

        [ShowIf("@FireCount >1"),LabelText("间隔角度(以一个扇形来计算)"),MinValue(0)]
        public float FireFurcationAngle;
        
        [LabelText("最大飞行距离"),MinValue(0)]
        public int MaxFlyDistance;
        
        [LabelText("碰撞到正常碰撞等级物体时")]
        public EAction_Projectile_CollisionType NormalCollisionType;
        
        [LabelText("碰撞到特殊碰撞等级物体时")]
        public EAction_Projectile_CollisionType SpecialCollisionType;

        [LabelText("伤害衰减比例(只有当碰到敌人时才会进行衰减,如果没有就代表没有衰减)")]
        public float[] DamageAttenuations;

        public EAction_Projectile_ShootType ShootProjectileType;

        public EAction_Skill_ElementType elementType;

        [ShowIf("@elementType!=EAction_Skill_ElementType.None"),LabelText("最大元素可积累等级")]
        public float maxElementAccLevel;
        
        [ShowIf("@elementType!=EAction_Skill_ElementType.None"),LabelText("元素伤害")]
        public float elementDamage;
        
        [ShowIf("@elementType!=EAction_Skill_ElementType.None"),LabelText("最大元素伤害")]
        public float maxElementDamage;
        
        [ShowIf("@elementType!=EAction_Skill_ElementType.None"),LabelText("每次升级增加的伤害比例")]
        public List<float> elementLevelUpAddedDamage;
        
        [ShowIf("@elementType!=EAction_Skill_ElementType.None"),LabelText("元素造成伤害的间隔时间")]
        public float elementTriggerInterval;
        
        [ShowIf("@elementType!=EAction_Skill_ElementType.None"),LabelText("每次升级减少造成伤害的间隔时间")]
        public List<float> elementLevelUpDesriggerInterval;

        [ShowIf("@ShootProjectileType==EAction_Projectile_ShootType.Parabola"),LabelText("抛物线最高点")]
        public float MaxParabolaHeight;
        
        /// <summary>
        /// 临时数据
        /// </summary>
        protected SCommonProjectileData tempProjectileData;

        public override void ExecuteCheck(WorldObj target)
        {
            base.ExecuteCheck(target);
            SetTemData();
        }

        protected virtual void SetTemData()
        {
            tempProjectileData = new SCommonProjectileData();
            tempProjectileData.maxDamageAttenuationLevel=DamageAttenuations.Length;
        }

        public override void ResetExecution()
        {
            tempProjectileData.ClearData();
        }

        public override void StartExecute()
        {
            base.StartExecute();
        }
        
        

        public override void UpdateExecute()
        {
            base.UpdateExecute();
            Fly();
        }
        

        public override void EndExecute()
        {
            base.EndExecute();
        }

        public override void Trigger(WorldObj triggerTarget)
        {
            if (tempProjectileData.CheckDamageAttenuationLevel())
            {
                triggerTarget.GetController().healthyController.Beharmed(tempProjectileData.CaculateDamage());
                EndExecute();
            }
        }

        public override void TriggerTypeCheck()
        {
            base.TriggerTypeCheck();
        }

        /// <summary>
        /// 飞行
        /// </summary>
        protected virtual void Fly()
        {
            if (ShootProjectileType == EAction_Projectile_ShootType.Line)
            {
                FlyWithLine();
            }
            else if (ShootProjectileType == EAction_Projectile_ShootType.Parabola)
            {
                FlyWithParabola();
            }
            FlyDistanceCheck();
        }

        /// <summary>
        /// 直线飞行
        /// </summary>
        protected virtual void FlyWithLine()
        {
            Vector3 direction = transform.forward;
            transform.position += direction * MoveSpeed * Time.deltaTime;
        }

        /// <summary>
        /// 抛物线
        /// </summary>
        protected virtual void FlyWithParabola()
        {
            float gravity = Physics.gravity.y;
            float time = Time.deltaTime;

            // 使用抛物线的公式计算弹体的位移
            Vector3 horizontalMove = transform.forward * MoveSpeed * time;
            Vector3 verticalMove = Vector3.up * (MaxParabolaHeight * time + 0.5f * gravity * time * time);

            // 更新弹体的位置
            transform.position += horizontalMove + verticalMove;
        }
        
        /// <summary>
        /// 飞行距离检测
        /// </summary>
        protected virtual void FlyDistanceCheck()
        {
            if (MaxFlyDistance > 0)
            {
                tempProjectileData.curFlyDistance += MoveSpeed * Time.deltaTime;
                if (tempProjectileData.curFlyDistance >= MaxFlyDistance)
                {
                    EndExecute();
                }
            }
        }
        
        
        // 使用 OnValidate 来动态调整列表长度
        private void OnValidate()
        {
            // 限制 elementLevelUpAddedDamage 列表长度
            if (elementLevelUpAddedDamage.Count > maxElementAccLevel)
            {
                elementLevelUpAddedDamage.RemoveRange((int)maxElementAccLevel, elementLevelUpAddedDamage.Count - (int)maxElementAccLevel);
            }
            else
            {
                while (elementLevelUpAddedDamage.Count < maxElementAccLevel)
                {
                    elementLevelUpAddedDamage.Add(0f); // 填充默认值
                }
            }

            // 限制 elementLevelUpDesriggerInterval 列表长度
            if (elementLevelUpDesriggerInterval.Count > maxElementAccLevel)
            {
                elementLevelUpDesriggerInterval.RemoveRange((int)maxElementAccLevel, elementLevelUpDesriggerInterval.Count - (int)maxElementAccLevel);
            }
            else
            {
                while (elementLevelUpDesriggerInterval.Count < maxElementAccLevel)
                {
                    elementLevelUpDesriggerInterval.Add(0f); // 填充默认值
                }
            }
        }
    }
}