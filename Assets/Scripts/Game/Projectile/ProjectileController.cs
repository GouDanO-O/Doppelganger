using System;
using GameFrame.Config;
using QFramework;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameFrame.World
{
    public class ProjectileTriggerDamageData_Temporality : TriggerDamageData_Temporality
    {
        /// <summary>
        /// 当前飞行速度
        /// </summary>
        public float curFlySpeed;

        /// <summary>
        /// 当前飞行距离
        /// </summary>
        public float curFlyDistance;
        
        /// <summary>
        /// 最大飞行距离
        /// </summary>
        public float maxFlyDistance;

        /// <summary>
        /// 当前飞行高度
        /// </summary>
        public float curFlyHeight;

        /// <summary>
        /// 飞行最高点
        /// </summary>
        public float maxFlyHeight;
        
        public static ProjectileTriggerDamageData_Temporality Allocate()
        {
            return SafeObjectPool<ProjectileTriggerDamageData_Temporality>.Instance.Allocate();
        }
    }
    
    public class ProjectileController : NetworkBehaviour
    {
        [HideInInspector] public WorldObj owner;
        
        private SCommonProjectileData_Persistence persistenceProjectileData;
        
        /// <summary>
        /// 临时子弹数据
        /// </summary>
        protected ProjectileTriggerDamageData_Temporality tempProjectileData;

        public void InitData(SCommonProjectileData_Persistence persistenceProjectileData)
        {
            this.persistenceProjectileData = persistenceProjectileData;
            SetTemData();
        }
        
        public void InitData(WorldObj owner,SCommonProjectileData_Persistence persistenceProjectileData)
        {
            this.owner = owner;
            this.persistenceProjectileData = persistenceProjectileData;
            SetTemData();
        }
        
        protected virtual void SetTemData()
        {
            tempProjectileData = ProjectileTriggerDamageData_Temporality.Allocate();

            ExtendData();
        }

        /// <summary>
        /// 继承数据
        /// </summary>
        protected virtual void ExtendData()
        {
            tempProjectileData.damageAttenuationLevel = persistenceProjectileData.DamageAttenuations;
            tempProjectileData.maxDamageAttenuationLevel = persistenceProjectileData.DamageAttenuations.Count;
            tempProjectileData.curBasicDamage = persistenceProjectileData.BaiscDamage;
            tempProjectileData.curFlySpeed = persistenceProjectileData.FlySpeed;
            tempProjectileData.maxFlyDistance=persistenceProjectileData.MaxFlyDistance;
        }
        
        /// <summary>
        /// 重设行为
        /// </summary>
        public virtual void ResetExecution()
        {
            tempProjectileData.Recycle2Cache();
        }

        private void FixedUpdate()
        {
            FixedUpdateExecuttion();
        }

        private void Update()
        {   
            
        }

        public virtual void FixedUpdateExecuttion()
        {
            Fly();
        }

        public virtual void UpdateExecution()
        {
            
        }
        
        public virtual void Trigger()
        {
            CrossOneTarget();
            if (tempProjectileData.CheckDamageAttenuationLevel())
            {
                EndExecute();
            }
        }
        
        public virtual void Trigger(WorldObj curTriggerTarget)
        {
            TriggerDamage(curTriggerTarget);

            CrossOneTarget();
            CheckCollisionTarget(curTriggerTarget);
            if (tempProjectileData.CheckDamageAttenuationLevel())
            {
                EndExecute();
            }
        }
        
        protected virtual void Fly()
        {
            if (persistenceProjectileData.ShootProjectileType == EAction_Projectile_ShootType.Line)
            {
                FlyWithLine();
            }
            else if (persistenceProjectileData.ShootProjectileType == EAction_Projectile_ShootType.Parabola)
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
            
        }

        /// <summary>
        /// 抛物线
        /// </summary>
        protected virtual void FlyWithParabola()
        {
            
        }
        
        /// <summary>
        /// 飞行距离检测
        /// </summary>
        protected virtual void FlyDistanceCheck()
        {
            if (tempProjectileData.maxFlyDistance > 0)
            {
                tempProjectileData.curFlyDistance += Time.deltaTime;
                
                if (tempProjectileData.curFlyDistance >= tempProjectileData.maxFlyDistance)
                {
                    EndExecute();
                }
            }
        }

        /// <summary>
        /// 造成伤害
        /// </summary>
        /// <param name="curTriggerTarget"></param>
        protected virtual void TriggerDamage(WorldObj curTriggerTarget)
        {
            curTriggerTarget.BeHarmed(tempProjectileData.CaculateDamage(),tempProjectileData.curElementType);
        }

        /// <summary>
        /// 穿过或伤害了一个目标
        /// </summary>
        protected virtual void CrossOneTarget()
        {
            tempProjectileData.AddDamageAttenuationLevel();
        }

        /// <summary>
        /// 检测碰撞体的等级
        /// </summary>
        /// <param name="curTriggerTarget"></param>
        protected virtual void CheckCollisionTarget(WorldObj curTriggerTarget)
        {
            EWorldObjCollisionType curObjCollisionType = curTriggerTarget.CollisionType;
            int index=(int)curObjCollisionType;

            EAction_Projectile_CollisionType curCollisionType = persistenceProjectileData.CollisionTypes[index];
            switch (curCollisionType)
            {
                case EAction_Projectile_CollisionType.ContinueFly:
                    
                    break;
                case EAction_Projectile_CollisionType.CollisionAndDestroy:
                    EndExecute();
                    break;
                case EAction_Projectile_CollisionType.CollisionAndRebound:
                    ReboundProjectile();
                    break;
            }
        }
        
        /// <summary>
        /// 反弹弹体
        /// </summary>
        protected virtual void ReboundProjectile()
        {
            
        }

        /// <summary>
        /// 结束生命周期
        /// </summary>
        public virtual void EndExecute()
        {
            if (persistenceProjectileData.IsLoadFromPool)
            {
                PoolManager.Instance.RecycleObj(persistenceProjectileData.ObjectPoolType,gameObject);
            }
            else
            {
                
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            WorldObj otherWorldObj = other.GetComponent<WorldObj>();
            if (otherWorldObj)
            {
                Trigger(otherWorldObj);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            
        }

        private void OnTriggerExit(Collider other)
        {
            
        }
    }
}

