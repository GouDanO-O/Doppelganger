using System;
using System.Collections.Generic;
using GameFrame;
using GameFrame.World;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame
{
    [Serializable]
    public class CommonProjectileData_Persistence : PersistentData
    { 
        [LabelText("是否从对象池中进行加载")]
        public bool IsLoadFromPool;

        [ShowIf("IsLoadFromPool")]
        public EObjectPoolType ObjectPoolType;

        [HideIf("IsLoadFromPool")] [LabelText("物体")]
        public GameObject ObjectPrefab;

        [LabelText("发射次数"), MinValue(1)]
        public int FireCount = 1;

        [Header("造成伤害的类型")]
        public EElementType ElementType;
        
        [LabelText("基础伤害")]
        public float BaiscDamage;
        
        [LabelText("飞行速度"), MinValue(0)]
        public float FlySpeed;

        [HideIf("@FireCount <= 1")] [LabelText("开火间隔"), MinValue(0)]
        public float FireDelayTime;

        [HideIf("@FireCount <= 1")] [LabelText("间隔角度(以一个扇形来计算)"), MinValue(0)]
        public float FireFurcationAngle;

        [LabelText("最大飞行距离"), MinValue(0)]
        public int MaxFlyDistance;

        [LabelText("碰撞到不同碰撞等级物体时的处理(共有4种碰撞等级)")]
        public List<EAction_Projectile_CollisionType> CollisionTypesList = new List<EAction_Projectile_CollisionType>(4);

        [LabelText("伤害衰减比例(只有当碰到敌人时才会进行衰减,如果没有就代表没有衰减)")]
        public List<float> DamageAttenuationsList = new List<float>();
        
        public EAction_Projectile_ShootType ShootProjectileType;

        [HideIf("@ShootProjectileType != EAction_Projectile_ShootType.Parabola")]
        [LabelText("抛物线最高点")]
        public float MaxParabolaHeight;
    }
}