using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.World;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameFrame.Config
{
    /// <summary>
    /// 普通弹体类型
    /// </summary>
    [CreateAssetMenu(fileName = "CommonProjectile", menuName = "配置/技能/行为/弹体/普通弹体")]
    public class SActionClip_DetailAction_CommonProjectile : SkillActionClip_DetailAction_Basic
    {
        [LabelText("弹体数据")] public CommonProjectileData_Persistence persistenceProjectileData;

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
                projectile = GameObject.Instantiate(persistenceProjectileData.ObjectPrefab);
            }

            if (projectile == null)
                return;

            ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
            projectileController.InitData(clipDataTemporality.owner, persistenceProjectileData);
        }
    }
}