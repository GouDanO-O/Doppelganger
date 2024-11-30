using Sirenix.OdinInspector;

namespace GameFrame
{
    [LabelText("行为类型")]
    public enum EActionType
    {
        [LabelText("具体行为")]
        DetailAction,
        [LabelText("动画")]
        Animation,
        [LabelText("音效")]
        Audio,
        [LabelText("特效")]
        ParticleSystem,
    }
}