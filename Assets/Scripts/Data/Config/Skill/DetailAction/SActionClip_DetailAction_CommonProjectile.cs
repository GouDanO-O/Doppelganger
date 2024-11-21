using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.World;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameFrame.Config
{
    [Serializable]
    public class SCommonProjectileData_Persistence : PersistentData
    {
        [InfoBox("即以武器数据为准,且会计算来自玩家的增减益,而不是弹体自己的数据")] [LabelText("是否从武器上继承数据")]
        public bool IsExtendWeaponData;

        [HideIf("@IsExtendWeaponData")] [LabelText("是否从对象池中进行加载")]
        public bool IsLoadFromPool;

        [HideIf("@IsExtendWeaponData || !IsLoadFromPool")]
        public EObjectPoolType ObjectPoolType;

        [HideIf("@IsExtendWeaponData || IsLoadFromPool")] [LabelText("物体")]
        public GameObject ObjectPrefab;

        [HideIf("@IsExtendWeaponData")] [LabelText("发射次数"), MinValue(1)]
        public int FireCount = 1;

        [HideIf("@IsExtendWeaponData")] [LabelText("基础伤害")]
        public float BaiscDamage;

        [HideIf("@IsExtendWeaponData")] [LabelText("暴击率")]
        public float CriticalRate;

        [HideIf("@IsExtendWeaponData")] [LabelText("暴击伤害")]
        public float CriticalDamage;

        [HideIf("@IsExtendWeaponData")] [LabelText("飞行速度"), MinValue(0)]
        public float FlySpeed;

        [HideIf("@IsExtendWeaponData || FireCount <= 1")] [LabelText("开火间隔"), MinValue(0)]
        public float FireDelayTime;

        [HideIf("@IsExtendWeaponData || FireCount <= 1")] [LabelText("间隔角度(以一个扇形来计算)"), MinValue(0)]
        public float FireFurcationAngle;

        [HideIf("@IsExtendWeaponData")] [LabelText("最大飞行距离"), MinValue(0)]
        public int MaxFlyDistance;

        [HideIf("@IsExtendWeaponData")] [LabelText("碰撞到不同碰撞等级物体时的处理(共有4种碰撞等级)")]
        public List<EAction_Projectile_CollisionType> CollisionTypes = new List<EAction_Projectile_CollisionType>(4);

        [HideIf("@IsExtendWeaponData")] [LabelText("伤害衰减比例(只有当碰到敌人时才会进行衰减,如果没有就代表没有衰减)")]
        public List<float> DamageAttenuations;

        [HideIf("@IsExtendWeaponData")] 
        public EAction_Projectile_ShootType ShootProjectileType;

        [HideIf("@IsExtendWeaponData || ShootProjectileType != EAction_Projectile_ShootType.Parabola")]
        [LabelText("抛物线最高点")]
        public float MaxParabolaHeight;

        [HideIf("@IsExtendWeaponData")] 
        public EAction_Skill_ElementType ElementType;

        [HideIf("@IsExtendWeaponData || ElementType == EAction_Skill_ElementType.None")] [LabelText("最大元素可积累等级")]
        public float MaxElementAccLevel;

        [HideIf("@IsExtendWeaponData || ElementType == EAction_Skill_ElementType.None")] [LabelText("基础元素伤害")]
        public float BasicElementDamage;

        [HideIf("@IsExtendWeaponData || ElementType == EAction_Skill_ElementType.None")] [LabelText("每次升级增加的伤害比例")]
        public List<float> ElementLevelUpAddedDamage;

        [HideIf("@IsExtendWeaponData || ElementType == EAction_Skill_ElementType.None")] [LabelText("元素造成伤害的间隔时间")]
        public float BasicElementTriggerInterval;

        [HideIf("@IsExtendWeaponData || ElementType == EAction_Skill_ElementType.None")] [LabelText("每次升级减少造成伤害的间隔时间")]
        public List<float> ElementLevelUpDesriggerInterval;

        // 使用 OnValidate 来动态调整列表长度
        private void OnValidate()
        {
            // 限制 elementLevelUpAddedDamage 列表长度
            if (ElementLevelUpAddedDamage.Count > MaxElementAccLevel)
            {
                ElementLevelUpAddedDamage.RemoveRange((int)MaxElementAccLevel,
                    ElementLevelUpAddedDamage.Count - (int)MaxElementAccLevel);
            }
            else
            {
                while (ElementLevelUpAddedDamage.Count < MaxElementAccLevel)
                {
                    ElementLevelUpAddedDamage.Add(0f); // 填充默认值
                }
            }

            // 限制 elementLevelUpDesriggerInterval 列表长度
            if (ElementLevelUpDesriggerInterval.Count > MaxElementAccLevel)
            {
                ElementLevelUpDesriggerInterval.RemoveRange((int)MaxElementAccLevel,
                    ElementLevelUpDesriggerInterval.Count - (int)MaxElementAccLevel);
            }
            else
            {
                while (ElementLevelUpDesriggerInterval.Count < MaxElementAccLevel)
                {
                    ElementLevelUpDesriggerInterval.Add(0f); // 填充默认值
                }
            }
        }
    }
    
    
    /// <summary>
    /// 普通弹体类型
    /// </summary>
    [CreateAssetMenu(fileName = "CommonProjectile", menuName = "配置/技能/行为/弹体/普通弹体")]
    public class SActionClip_DetailAction_CommonProjectile : SActionClip_DetailAction_Basic
    {
        [LabelText("弹体数据")] public SCommonProjectileData_Persistence persistenceProjectileData;

        public override void StartExecute()
        {
            base.StartExecute();
            Trigger(clipDataTemporality.owner);
        }

        public override void Trigger(WorldObj owner)
        {
            base.Trigger(owner);
            ShootProjectileCheck();
        }

        /// <summary>
        /// 发射弹体前置检查
        /// </summary>
        protected virtual void ShootProjectileCheck()
        {
            for (int i = 0; i < persistenceProjectileData.FireCount; i++)
            {
                if (persistenceProjectileData.FireDelayTime > 0)
                {
                    Main.Interface.GetUtility<CoroutineUtility>().StartRoutine(FireTimeDelay());
                }
                else
                {
                    ShootProjectile();
                }
            }
        }

        IEnumerator FireTimeDelay()
        {
            yield return new WaitForSeconds(persistenceProjectileData.FireDelayTime);
            ShootProjectile();
        }

        /// <summary>
        /// 发射弹体
        /// </summary>
        protected virtual void ShootProjectile()
        {
            GameObject projectile = null;
            if (persistenceProjectileData.IsLoadFromPool)
            {
                projectile = PoolManager.Instance.LoadObjFromPool(persistenceProjectileData.ObjectPoolType);
            }
            else
            {
                projectile = Instantiate(persistenceProjectileData.ObjectPrefab);
            }

            if (projectile == null)
                return;

            ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
            projectileController.InitData(clipDataTemporality.owner, persistenceProjectileData);
        }
    }
}