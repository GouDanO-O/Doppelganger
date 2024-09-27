using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame
{
    /// <summary>
    /// 伤害类型
    /// </summary>
    [LabelText("伤害类型")]
    public enum EDamageType
    {
        /// <summary>
        /// 物理伤害
        /// </summary>
        [LabelText("物理伤害")]
        Physic = 0,
        /// <summary>
        /// 魔法伤害
        /// </summary>
        [LabelText("魔法伤害")]
        Magic = 1,
        /// <summary>
        /// 真实伤害
        /// </summary>
        [LabelText("真实伤害")]
        Real = 2,
    }
    
    /// <summary>
    /// 行为禁制
    /// </summary>
    [Flags]
    [LabelText("行为禁制")]
    public enum EProhibitionOfConductType
    {
        /// <summary>
        /// 无
        /// </summary>
        [LabelText("（空）")]
        None = 0,
        /// <summary>
        /// 禁止移动
        /// </summary>
        [LabelText("禁止移动")]
        MoveForbid = 1 << 1,
        /// <summary>
        /// 禁止施法
        /// </summary>
        [LabelText("禁止施法")]
        SkillForbid = 1 << 2,
        /// <summary>
        /// 禁止攻击
        /// </summary>
        [LabelText("禁止攻击")]
        AttackForbid = 1 << 3,
    }
    
    /// <summary>
    /// 附加数值类型
    /// </summary>
    [LabelText("附加数值类型")]
    public enum EModifyType
    {
        /// <summary>
        /// 固定增加数值
        /// </summary>
        [LabelText("固定增加数值")]
        Add = 0,
        /// <summary>
        /// 百分比增加数值
        /// </summary>
        [LabelText("百分比增加数值")]
        PercentAdd = 1,
        /// <summary>
        /// 基础值
        /// </summary>
        [LabelText("基础值")]
        BaseValue = 2,
    }
    
    /// <summary>
    /// 触发类型
    /// </summary>
    [LabelText("触发类型")]
    public enum ETriggerType
    {
        /// <summary>
        /// 主动触发
        /// </summary>
        [LabelText("主动触发")]
        ExecuteTrigger = 1,
        /// <summary>
        /// 被动触发
        /// </summary>
        [LabelText("被动触发")]
        AutoTrigger = 2
    }
}

