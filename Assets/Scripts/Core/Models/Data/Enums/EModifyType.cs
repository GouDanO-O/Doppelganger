using Sirenix.OdinInspector;

namespace GameFrame
{
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
}