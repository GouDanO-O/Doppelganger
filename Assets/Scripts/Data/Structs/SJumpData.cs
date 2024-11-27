using System;
using Sirenix.OdinInspector;

namespace GameFrame
{
    /// <summary>
    /// 跳跃数据配置
    /// </summary>
    [Serializable,LabelText("跳跃数据", SdfIconType.Box)]
    public struct SJumpData
    {
        [LabelText("跳跃高度")]
        public float jumpHeight;
        
        [LabelText("能否二段跳")]
        public bool canDoubleJump;

        [ShowIf("canDoubleJump"),LabelText("二段跳间隔时间")]
        public float doubleJumpDeepTime;

        [ShowIf("canDoubleJump"),LabelText("二段跳高度")]
        public float doubleJumpHeight;
    }
}