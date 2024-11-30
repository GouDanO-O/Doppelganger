using Sirenix.OdinInspector;

namespace GameFrame
{
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
}