using Sirenix.OdinInspector;

namespace GameFrame
{
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