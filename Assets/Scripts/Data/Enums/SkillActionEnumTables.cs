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
        [LabelText("立即触发(如有延时,会在延时结束后触发)")]
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
        ContinueFly,
        [LabelText("碰到就消失")]
        CollisionAndDestroy,
        [LabelText("碰到就反弹")]
        CollisionAndRebound
    }

    [LabelText("元素伤害类型")]
    public enum EAction_Skill_ElementType
    {
        [LabelText("无特殊属性")]
        None,
        [LabelText("火属性--灼伤")]
        Fire,
        [LabelText("冰属性--减速")]
        Ice,
        [LabelText("毒属性--毒伤+破防")]
        Poision,
        [LabelText("雷属性--易伤")]
        Electricity,
        [LabelText("光属性--达到层数时眩晕")]
        Light,
        [LabelText("暗属性--虚弱")]
        Dark
    }
    
}

