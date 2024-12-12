using System;
using GameFrame.Config;
using QFramework;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameFrame.World
{
    public class ProjectileController : NetworkBehaviour
    {
        public bool willRecycle { get; protected set; }
        
        [HideInInspector] public WorldObj owner;

        private CommonProjectileData_Persistence persistenceProjectileData;
        
        /// <summary>
        /// 临时子弹数据
        /// </summary>
        protected ProjectileTriggerDamageData_TemporalityPoolable tempProjectileData;

        /// <summary>
        /// 弹体控制器初始化
        /// 发射者可以世界中任意物体,但不能为空
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="persistenceProjectileData"></param>
        public void InitData(WorldObj owner, CommonProjectileData_Persistence persistenceProjectileData)
        {
            this.owner = owner;
            this.persistenceProjectileData = persistenceProjectileData;
            SetTemData();
            OnCreate();
        }
        
        /// <summary>
        /// 设置临时数据
        /// 读取配置中的数据转成自己当前生命周期中的临时数据
        /// </summary>
        protected virtual void SetTemData()
        {
            willRecycle = false;
            
            tempProjectileData = ProjectileTriggerDamageData_TemporalityPoolable.Allocate();
            tempProjectileData.UpdateEnforcer(owner);
            tempProjectileData.UpdateDamageAttenuationLevel(persistenceProjectileData.DamageAttenuationsList);
            tempProjectileData.UpdateBasicDamage(persistenceProjectileData.BaiscDamage);
            tempProjectileData.UpdateFlySpeed(persistenceProjectileData.FlySpeed);
            tempProjectileData.UpdateMaxFlyDistance(persistenceProjectileData.MaxFlyDistance);

            tempProjectileData.UpdateFlyHeight(0);
            tempProjectileData.UpdateMaxFlyHeight(persistenceProjectileData.ShootProjectileType,persistenceProjectileData.MaxParabolaHeight);
        }
        
        /// <summary>
        /// 当创建时
        /// 在初始化后才进行调用
        /// </summary>
        public virtual void OnCreate()
        {
            
        }

        /// <summary>
        /// 当碰撞到其他物体时
        /// </summary>
        /// <param name="otherWorldObj"></param>
        public virtual void OnHit(WorldObj otherWorldObj)
        {
            Trigger(otherWorldObj);
        }

        /// <summary>
        /// 当生命周期即将结束时
        /// </summary>
        public virtual void OnRemove()
        {
            EndExecute();
        }
        
        private void FixedUpdate()
        {
            FixedUpdateExecuttion();
        }

        private void Update()
        {
            UpdateExecution();
        }

        public virtual void FixedUpdateExecuttion()
        {
            if (willRecycle)
                return;
            Fly();
        }

        public virtual void UpdateExecution()
        {
            if (willRecycle)
                return;

             tempProjectileData.UpdateFlyHeight(transform.position.y);
        }

        /// <summary>
        /// 无具体的作用对象时的触发
        /// 一般作用于计时形行为的触发
        /// </summary>
        public virtual void Trigger()
        {
            CrossOneTarget();
            if (tempProjectileData.CheckDamageAttenuationLevel())
            {
                OnRemove();
            }
        }

        /// <summary>
        /// 有具体的作用对象时的触发
        /// </summary>
        /// <param name="curTriggerTarget"></param>
        public virtual void Trigger(WorldObj curTriggerTarget)
        {
            TriggerDamage(curTriggerTarget);

            CrossOneTarget();
            CheckCollisionTarget(curTriggerTarget);
            if (tempProjectileData.CheckDamageAttenuationLevel())
            {
                OnRemove();
            }
        }

        /// <summary>
        /// 弹体飞行
        /// 可以用Tween配置的形式去做飞行动画和位移
        /// </summary>
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
            Vector3 direction = transform.forward;
            float distance = tempProjectileData.CurFlySpeed * Time.fixedDeltaTime;
            transform.position += direction * distance;
            tempProjectileData.AddFlyDistance(distance);
        }

        /// <summary>
        /// 抛物线
        /// </summary>
        protected virtual void FlyWithParabola()
        {
            Vector3 velocity = transform.forward * tempProjectileData.CurFlySpeed;
            velocity += Physics.gravity * Time.fixedDeltaTime;
            transform.position += velocity * Time.fixedDeltaTime;
            tempProjectileData.AddFlyDistance(velocity.magnitude * Time.fixedDeltaTime);
        }

        /// <summary>
        /// 飞行距离检测
        /// 当抵达了最大飞行距离就会销毁
        /// </summary>
        protected virtual void FlyDistanceCheck()
        {
            if (tempProjectileData.MaxFlyDistance > 0)
            {
                if (tempProjectileData.IsArriveMaxDistance())
                {
                    OnRemove();
                }
            }
        }

        /// <summary>
        /// 造成伤害
        /// </summary>
        /// <param name="sufferer"></param>
        protected virtual void TriggerDamage(WorldObj sufferer)
        {
            tempProjectileData.UpdateSufferer(sufferer);
            tempProjectileData.UpdateElementType(tempProjectileData.elementType, true);
            tempProjectileData.UpdateBasicDamage(tempProjectileData.CaculateFinalDamage());
            sufferer.BeHarmed(tempProjectileData);
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
            int index = (int)curObjCollisionType;

            // 确保索引在范围内
            if (index < 0 || index >= persistenceProjectileData.CollisionTypesList.Count)
            {
                Debug.LogWarning("越界");
                return;
            }

            EAction_Projectile_CollisionType curCollisionType = persistenceProjectileData.CollisionTypesList[index];
            switch (curCollisionType)
            {
                case EAction_Projectile_CollisionType.ContinueFly:
                    // 继续飞行
                    break;
                case EAction_Projectile_CollisionType.CollisionAndDestroy:
                    OnRemove();
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
            // 示例实现：反转飞行方向
            Vector3 currentDirection = transform.forward;
            Vector3 reboundDirection = Vector3.Reflect(currentDirection, Vector3.up); // 假设反弹平面为水平面
            transform.forward = reboundDirection.normalized;
        }

        /// <summary>
        /// 结束生命周期
        /// </summary>
        protected virtual void EndExecute()
        {
            DeInitData();
            if (persistenceProjectileData.IsLoadFromPool)
            {
                PoolManager.Instance.RecycleObj(persistenceProjectileData.ObjectPoolType, gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        protected virtual void DeInitData()
        {
            tempProjectileData.Recycle2Cache();
            willRecycle = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            WorldObj otherWorldObj = other.GetComponent<WorldObj>();
            if (otherWorldObj)
            {
                OnHit(otherWorldObj);
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
