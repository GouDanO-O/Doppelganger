using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.World;
using QFramework;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameFrame.Config
{
    /// <summary>
    /// 普通弹体类型
    /// 行为:发射一枚弹体
    /// </summary>
    [CreateAssetMenu(fileName = "CommonProjectile", menuName = "配置/技能/行为/弹体/普通弹体")]
    public class SActionClip_DetailAction_CommonProjectile : SkillActionClip_DetailAction_Basic
    {
        [LabelText("弹体数据")] 
        public CommonProjectileData_Persistence persistenceProjectileData;

        protected override void StartExecute()
        {
            base.StartExecute();
            ShootProjectileCheck();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnTriggerStart()
        {
        }

        /// <summary>
        /// 发射弹体前置检查
        /// </summary>
        protected virtual void ShootProjectileCheck()
        {

            for (int i = 0; i < persistenceProjectileData.FireCount; i++)
            {
                float delayTime = persistenceProjectileData.FireDelayTime;
                if (delayTime > 0)
                {
                    ActionKit.Delay(i * delayTime, () =>
                    {
                        ShootProjectile();
                    }).StartGlobal();
                }
                else
                {
                    ShootProjectile();
                }
            }
            

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

            projectile.Show();
            ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
            projectileController.InitData(persistenceProjectileData);
        }
    }
}