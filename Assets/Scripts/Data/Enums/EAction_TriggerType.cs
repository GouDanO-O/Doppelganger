using Sirenix.OdinInspector;

namespace GameFrame
{
    [LabelText("触发类型")]
    public enum EAction_TriggerType
    {
        [LabelText("立即触发(如有延时,会在延时结束后触发)")]
        StartTrigger = 1 << 0,
        [LabelText("碰撞时触发")]
        CollisionTrigger = 1 << 1,
        [LabelText("生命周期结束时触发")]
        LifeTimeEndTrigger = 1 << 2,
    }
}