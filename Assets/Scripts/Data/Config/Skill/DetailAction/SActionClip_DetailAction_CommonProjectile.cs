using GameFrame.World;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    /// <summary>
    /// 普通弹体类型
    /// </summary>
    [CreateAssetMenu(fileName = "CommonProjectile",menuName = "配置/技能/行为/弹体/普通弹体")]
    public class SActionClip_DetailAction_CommonProjectile : SActionClip_DetailAction_Basic
    {
        [LabelText("发射次数"),Min(0)]
        public int FireCount;

        [LabelText("飞行速度")]
        public float MoveSpeed;
        
        [ShowIf("@FireCount >1"),LabelText("开火间隔"),Min(0)]
        public float FireDelayTime;

        [ShowIf("@FireCount >1"),LabelText("间隔角度(以一个扇形来计算)"),Min(0)]
        public float FireFurcationAngle;
        
        [LabelText("最大飞行距离"),Min(0)]
        public int MaxFlyDistance;
        
        [LabelText("碰撞到正常碰撞等级物体时")]
        public EAction_Projectile_CollisionType NormalCollisionType;
        
        [LabelText("碰撞到特殊碰撞等级物体时")]
        public EAction_Projectile_CollisionType SpecialCollisionType;

        public EAction_Projectile_ShootType ShootProjectileType;

        [ShowIf("@ShootProjectileType==EAction_Projectile_ShootType.Parabola"),LabelText("抛物线最高点")]
        public float MaxParabolaHeight;
        

        
        protected float curFlyDistance;


        public override void ResetExecution()
        {
            
        }

        public override void StartExecute()
        {
            
        }
        
        

        public override void UpdateExecute()
        {
            base.UpdateExecute();
            FlyDistanceCheck();
        }
        

        public override void EndExecute()
        {
            
        }

        public override void Trigger()
        {
            CrossOneTarget();
        }

        public override void TriggerTypeCheck()
        {
            base.TriggerTypeCheck();
        }

        /// <summary>
        /// 飞行距离检测
        /// </summary>
        protected virtual void FlyDistanceCheck()
        {
            if (MaxFlyDistance > 0)
            {
                
            }
        }

        /// <summary>
        /// 穿过一个目标(计算伤害衰减或其他操作)
        /// </summary>
        protected virtual void CrossOneTarget()
        {
            
        }
    }
}