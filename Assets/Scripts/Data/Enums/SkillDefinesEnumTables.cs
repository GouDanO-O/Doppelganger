using System;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.Config
{
    /// <summary>
    /// 技能所属节点树类别
    /// </summary>
    [LabelText("技能所属节点树类别")]
    public enum ESkill_NodeTreeType
    {
        [LabelText("生存类")]
        Live,
        [LabelText("战斗类别")]
        Combat,
        [LabelText("特殊类别")]
        Special
    }
    
    /// <summary>
    /// 技能目标阵营
    /// </summary>
    [LabelText("技能目标阵营")]
    public enum ESkill_AffectTargetCampsType
    {
        /// <summary>
        /// 自身
        /// </summary>
        [LabelText("自身")]
        Self = 0,
        /// <summary>
        /// 己方
        /// </summary>
        [LabelText("己方--可包括自己")]
        SelfTeam = 1,
        /// <summary>
        /// 敌方
        /// </summary>
        [LabelText("敌方")]
        EnemyTeam = 2,
    }
    
    /// <summary>
    /// 区域场类型
    /// </summary>
    [LabelText("区域场类型")]
    public enum ESkill_AffectAreaType
    {
        /// <summary>
        /// 圆形
        /// </summary>
        [LabelText("圆形")]
        Circle = 0,
        /// <summary>
        /// 矩形
        /// </summary>
        [LabelText("矩形")]
        Rect = 1,
        /// <summary>
        /// 三角形
        /// </summary>
        [LabelText("三角形")]
        Triangle = 2,
        /// <summary>
        /// 组合
        /// </summary>
        [LabelText("组合")]
        Compose = 3
    }

    /// <summary>
    /// 主动技能释放方式
    /// </summary>
    public enum ESkill_TriggerConditionType
    {
        [LabelText("只有放到槽位上时且主动释放才会执行")]
        PutIntoSkillSlots,
        [LabelText("当满足某些条件时自动执行")]
        ConditionalExecution,
        [LabelText("定时执行--即CD时间结束就执行")]
        TimedExecution,
    }
    
    /// <summary>
    /// 技能选取目标方式类型
    /// </summary>
    [LabelText("技能选取目标方式类型")]
    public enum ESkill_SelectionTargetWayType
    {
        /// <summary>
        /// 手动指定
        /// </summary>
        [LabelText("手动指定")]
        PlayerSelect,
        /// <summary>
        /// 碰撞检测
        /// </summary>
        [LabelText("碰撞检测")]
        CollisionSelect,
        /// <summary>
        /// 固定区域场检测
        /// </summary>
        [LabelText("固定区域场检测")]
        AreaSelect,
        /// <summary>
        /// 条件指定
        /// </summary>
        [LabelText("条件指定")]
        ConditionSelect,
        /// <summary>
        /// 自定义
        /// </summary>
        [LabelText("自定义")]
        Custom,
    }
}

