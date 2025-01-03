using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameFrame.World;
using GameFrame;
using QFramework;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    [Serializable]
    public class SkillActionClip
    {
        [LabelText("行为类型")]
        public EActionType ActionType;

        [LabelText("行为描述")]
        public string ActionDes;
        
        [HorizontalGroup("Timing")]
        [LabelText("开始时间"), LabelWidth(60), MinValue(0)]
        public float StartTime;

        [HorizontalGroup("Timing")]
        [LabelText("生命周期)"), LabelWidth(60)]
        public float Duration = 1f;

        [HorizontalGroup("Timing")]
        [ShowInInspector,LabelText("结束时间"), LabelWidth(60), ReadOnly]
        public float EndTime => StartTime + Duration;
        
        [BoxGroup("行为参数")]
        [SerializeReference]
        public SkillActionClip_BasicData Parameters;
    }
}
