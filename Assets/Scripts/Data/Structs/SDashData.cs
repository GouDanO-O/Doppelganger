using System;
using Sirenix.OdinInspector;

namespace GameFrame
{
    /// <summary>
    /// 冲刺数据配置
    /// </summary>
    [Serializable,LabelText("冲刺数据", SdfIconType.Box)]
    public struct SDashData
    {
        [LabelText("冲刺时长")]
        public float dashDuration;

        [LabelText("冲刺距离")]
        public float dashDistance;

        [LabelText("冲刺CD")]
        public float dashCD;
    }
}