using System;
using System.Collections;
using System.Collections.Generic;

using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace GameFrame.Config
{
    public class SkillActionClip_Config : SerializationConfig
    {
        [LabelText("行为片段名称")]
        public string ActionClipName;
        
        [LabelText("开始时间")]
        public double StartTime;
        
        // 验证结束时间必须在开始时间和总时间之间
        [LabelText("结束时间")]
        public double EndTime;

        [LabelText("行为片段类型")]
        public EActionClipType ActionClipType;
        
        [LabelText("特效数据"),ShowIf("actionClipType",EActionClipType.ParticleEffect)]
        public SActionClip_ParticleEffectData ActionClip_ParticleEffectData;
        
        [LabelText("音频数据"),ShowIf("actionClipType",EActionClipType.Audio)]
        public SActionClip_AudioData ActionClip_AudioData;
        
        [LabelText("动画数据"),ShowIf("actionClipType",EActionClipType.Animation)]
        public SActionClip_AniamtionData ActionClip_AniamtionData;
        
        [LabelText("行为触发条件")]
        public SAction_TriggerConditionFormula ActionTriggerConditionFormula;
        
        [LabelText("技能触发时的行为")]
        public EAction_TriggerType ActionTriggerType;
    }

    public enum EActionClipType
    {
        [LabelText("物体处理")]
        ItemExecute,
        [LabelText("行为处理")]
        ActionEvent,
        [LabelText("动画")]
        Animation,
        [LabelText("音频")]
        Audio,
        [LabelText("特效")]
        ParticleEffect
    }
    
    [Serializable]
    public class SAction_TriggerConditionFormula
    {
        [LabelText("执行方式")]
        public EAction_TriggerConditionFormulaTypes actionTriggerConditionType;

        [LabelText("条件"),ShowIf("actionTriggerConditionType",EAction_TriggerConditionFormulaTypes.ConditionalExecution)]
        public string condintionFormula;
        
        [LabelText("延时时间"),ShowIf("actionTriggerConditionType",EAction_TriggerConditionFormulaTypes.TimedExecution)]
        public float timeDelayTime;
    }
    
    [Serializable]
    public class SActionClip_ParticleEffectData
    {
        public GameObject particleEffectPrefab;
    }
    
    [Serializable]
    public class SActionClip_AniamtionData
    {
        public AnimationClip animationClip;
    }
    
    [Serializable]
    public class SActionClip_AudioData
    {
        public AudioClip audioClip;
    }
    
}

