using Sirenix.OdinInspector;

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
}