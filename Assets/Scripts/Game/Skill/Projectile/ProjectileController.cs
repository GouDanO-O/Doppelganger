using System;
using GameFrame.Config;
using QFramework;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameFrame.World
{
    
    public class ProjectileController : MonoBehaviour
    {
        [HideInInspector] public WorldObj owner;
        
        private SCommonProjectileData_Persistence persistenceProjectileData;
        
        /// <summary>
        /// 临时数据
        /// </summary>
        protected SCommonProjectileData_Temporality tempProjectileData;

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
            tempProjectileData = SCommonProjectileData_Temporality.Allocate();

            tempProjectileData.maxDamageAttenuationLevel = persistenceProjectileData.DamageAttenuations.Length;
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
            if (persistenceProjectileData.MaxFlyDistance > 0)
            {
                if (tempProjectileData.curFlyDistance >= persistenceProjectileData.MaxFlyDistance)
                {
                    EndExecute();
                }
                tempProjectileData.curFlyDistance += Time.deltaTime;
            }
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
                case EAction_Projectile_CollisionType.ContinuteFly:
                    
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
            if (persistenceProjectileData.isLoadFromPool)
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
            
        }

        private void OnTriggerStay(Collider other)
        {
            
        }

        private void OnTriggerExit(Collider other)
        {
            
        }
    }
    
    public class SCommonProjectileData_Temporality : TemporalityData_Pool
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
        
        public bool IsRecycled { get; set; }

        /// <summary>
        /// 计算当前伤害
        /// </summary>
        /// <param name="damageAttenuationRate"></param>
        /// <returns></returns>
        public float CaculateDamage(float damageAttenuationRate=1)
        {
            float randomCriticalRate = Random.Range(curCriticalRate, 100);
            bool isCritical = randomCriticalRate <= curCriticalRate;
            return (isCritical ?  curWillTriggerDamage* curCriticalDamage : curWillTriggerDamage)*damageAttenuationRate;
        }

        /// <summary>
        /// 增加伤害等级
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
        public bool CheckDamageAttenuationLevel()
        {
            if (maxDamageAttenuationLevel == 0)
                return false;
            return curDamageAttenuationLevel >= maxDamageAttenuationLevel;
        }

        public static SCommonProjectileData_Temporality Allocate()
        {
            return SafeObjectPool<SCommonProjectileData_Temporality>.Instance.Allocate();
        }
        
        public override void OnRecycled()
        {
            curDamageAttenuationLevel = 0;
            curFlyDistance = 0;
        }
        
        public override void Recycle2Cache()
        {
            
        }
    }
}

