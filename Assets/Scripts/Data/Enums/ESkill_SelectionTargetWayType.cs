using Sirenix.OdinInspector;

namespace GameFrame
{
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