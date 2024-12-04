using System;
using Sirenix.OdinInspector;

namespace GameFrame.Config
{
    [Serializable]
    public class SkillActionClip_BasicData : SerializedScriptableObject
    {
        [HorizontalGroup("Timing")]
        [LabelText("开始时间"), LabelWidth(60), MinValue(0)]
        public float StartTime;

        [HorizontalGroup("Timing")]
        [LabelText("持续时间"), LabelWidth(60)]
        public float Duration = 1f;

        [HorizontalGroup("Timing")]
        [LabelText("结束时间"), LabelWidth(60), ReadOnly]
        public float EndTime => StartTime + Duration;
    }
}