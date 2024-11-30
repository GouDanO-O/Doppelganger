using Sirenix.OdinInspector;

namespace GameFrame
{
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
}