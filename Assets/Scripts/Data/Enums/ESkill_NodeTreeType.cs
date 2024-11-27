using Sirenix.OdinInspector;

namespace GameFrame
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
}