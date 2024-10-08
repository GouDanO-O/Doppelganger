using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    [Serializable]
    [InlineProperty]
    [HideReferenceObjectPicker]
    public abstract class SkillActionClip_Config
    {
        #region 时间参数

        [HorizontalGroup("Timing")]
        [LabelText("开始时间"), LabelWidth(60), MinValue(0)]
        public float StartTime;

        [HorizontalGroup("Timing")]
        [LabelText("持续时间"), LabelWidth(60)]
        public float Duration = 1f;

        [HorizontalGroup("Timing")]
        [LabelText("结束时间"), LabelWidth(60), ReadOnly]
        public float EndTime => StartTime + Duration;

        #endregion

        #region 触发条件

        [FoldoutGroup("触发条件")]
        [LabelText("行为触发条件")]
        public SAction_TriggerConditionFormula ActionTriggerConditionFormula = new SAction_TriggerConditionFormula();

        [FoldoutGroup("触发条件")]
        [LabelText("技能触发时的行为")]
        public EAction_TriggerType ActionTriggerType;

        #endregion
    }

    [Serializable]
    public class SAction_TriggerConditionFormula
    {
        [LabelText("执行方式")]
        public EAction_TriggerConditionFormulaTypes ActionTriggerConditionType;

        [LabelText("条件"), ShowIf("@ActionTriggerConditionType == EAction_TriggerConditionFormulaTypes.ConditionalExecution")]
        public string ConditionFormula;

        [LabelText("延时时间"), ShowIf("@ActionTriggerConditionType == EAction_TriggerConditionFormulaTypes.TimedExecution")]
        public float TimeDelayTime;
    }

    [Serializable]
    [InlineProperty]
    [HideReferenceObjectPicker]
    public class SActionClip_ParticleEffectData : SkillActionClip_Config
    {
        [BoxGroup("特效设置")]
        [LabelText("特效预制体")]
        public GameObject ParticleEffectPrefab;
    }

    [Serializable]
    [InlineProperty]
    [HideReferenceObjectPicker]
    public class SActionClip_AnimationData : SkillActionClip_Config
    {
        [BoxGroup("动画设置")]
        [LabelText("动画剪辑")]
        public AnimationClip AnimationClip;
    }

    [Serializable]
    [InlineProperty]
    [HideReferenceObjectPicker]
    public class SActionClip_AudioData : SkillActionClip_Config
    {
        [BoxGroup("音频设置")]
        [LabelText("音频剪辑")]
        public AudioClip AudioClip;
    }
}
