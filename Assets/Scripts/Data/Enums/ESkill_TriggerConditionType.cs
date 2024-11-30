using Sirenix.OdinInspector;

namespace GameFrame
{
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
}