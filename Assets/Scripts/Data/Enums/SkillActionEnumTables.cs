using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame
{
    [LabelText("行为类型")]
    public enum EActionType
    {
        [LabelText("具体行为")]
        DetailAction,
        [LabelText("动画")]
        Animation,
        [LabelText("音效")]
        Audio,
        [LabelText("特效")]
        ParticleSystem,
    }
    
    [LabelText("触发类型")]
    public enum EAction_TriggerType
    {
        [LabelText("立即触发")]
        StartTrigger = 1 << 0,
        [LabelText("碰撞时触发")]
        CollisionTrigger = 1 << 1,
        [LabelText("生命周期结束时触发")]
        LifeTimeEndTrigger = 1 << 2,
    }

    [LabelText("发射弹体形式")]
    public enum EAction_Projectile_ShootType
    {
        [LabelText("直线")]
        Line,
        [LabelText("抛物线")]
        Parabola,
    }

    /// <summary>
    /// 弹体碰撞时会如何处理
    /// </summary>
    public enum EAction_Projectile_CollisionType
    {
        [LabelText("继续飞行")]
        ContinuteFly,
        [LabelText("碰到就消失")]
        CollisionAndDestroy,
        [LabelText("碰到就反弹")]
        CollisionAndRebound
    }

    [LabelText("元素伤害类型")]
    public enum EAction_Skill_ElementType
    {
        [LabelText("无属性")]
        None,
        [LabelText("火属性")]
        Fire,
        [LabelText("雷属性")]
        Electricity,
    }
    
}

