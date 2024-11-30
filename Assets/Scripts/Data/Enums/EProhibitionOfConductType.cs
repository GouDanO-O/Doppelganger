using System;
using Sirenix.OdinInspector;

namespace GameFrame
{
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
}