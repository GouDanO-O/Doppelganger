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
        [HideIf("@IsExtendWeaponData")] [LabelText("是否从对象池中进行加载")]
        public bool IsLoadFromPool;

        [HideIf("@IsExtendWeaponData || !IsLoadFromPool")]
        public EObjectPoolType ObjectPoolType;

        [HideIf("@IsExtendWeaponData || IsLoadFromPool")] [LabelText("物体")]
        public GameObject ObjectPrefab;

        [HideIf("@IsExtendWeaponData")] [LabelText("发射次数"), MinValue(1)]
        public int FireCount = 1;

        [Header("造成伤害的类型")]
        public EAction_Skill_ElementType ElementType;
        
        [HideIf("@IsExtendWeaponData")] [LabelText("基础伤害")]
        public float BaiscDamage;

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