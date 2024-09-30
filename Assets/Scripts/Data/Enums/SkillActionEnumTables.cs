using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame
{
    [LabelText("目标传入类型")]
    public enum EAction_TargetInputType
    {
        [LabelText("None")]
        None = 0,
        [LabelText("传入目标实体")]
        Target = 1,
        [LabelText("传入目标点")]
        Point = 2,
    }
    
    [LabelText("触发类型"), Flags]
    public enum EAction_TriggerType
    {
        [LabelText("无")]
        None = 0,
        [LabelText("初始触发")]
        StartTrigger = 1 << 1,
        [LabelText("碰撞触发单次")]
        CollisionTrigger = 1 << 2,
        [LabelText("结束触发")]
        EndTrigger = 1 << 3,
        [LabelText("碰撞触发多次")]
        CollisionTriggerMultiple = 1 << 4,
    }

    [LabelText("发射弹体形式")]
    public enum EAction_ShootProjectileType
    {
        [LabelText("直线")]
        Line,
        [LabelText("抛物线")]
        Parabola,
    }
    
    /// <summary>
    /// 目标类型
    /// </summary>
    [LabelText("目标类型")]
    public enum EAction_TargetType
    {
        [LabelText("单体检测")]
        Single = 0,
        [LabelText("多人检测")]
        Multiple = 1,
    }
    
    /// <summary>
    /// 技能触发执行类型
    /// </summary>
    [LabelText("技能触发执行类型")]
    public enum EAction_TriggerConditionFormulaTypes
    {
        [LabelText("立即执行")]
        ImmediateExecution,
        [LabelText("条件执行")]
        ConditionalExecution,
        [LabelText("延时执行")]
        TimedExecution,
    }
    
    /// <summary>
    /// 效果类型
    /// </summary>
    [LabelText("效果类型")]
    public enum EAction_TypeOfEffect
    {
        /// <summary>
        /// 无
        /// </summary>
        [LabelText("(添加效果)")]
        None = 0,
        /// <summary>
        /// 造成伤害
        /// </summary>
        [LabelText("造成伤害")]
        CauseDamage = 1,
        /// <summary>
        /// 治疗英雄
        /// </summary>
        [LabelText("治疗英雄")]
        CureHero = 2,
        /// <summary>
        /// 施加状态
        /// </summary>
        [LabelText("施加状态")]
        AddStatus = 3,
        /// <summary>
        /// 移除状态
        /// </summary>
        [LabelText("移除状态")]
        RemoveStatus = 4,
        /// <summary>
        /// 增减数值
        /// </summary>
        [LabelText("增减数值")]
        NumericModify = 6,
        /// <summary>
        /// 中毒
        /// </summary>
        [LabelText("中毒")]
        Poison = 7,
        /// <summary>
        /// 灼烧
        /// </summary>
        [LabelText("灼烧")]
        Burn = 8,
    }
}

