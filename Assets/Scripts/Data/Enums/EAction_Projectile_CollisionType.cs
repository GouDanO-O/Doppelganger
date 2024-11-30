using Sirenix.OdinInspector;

namespace GameFrame
{
    /// <summary>
    /// 弹体碰撞时会如何处理
    /// </summary>
    public enum EAction_Projectile_CollisionType
    {
        [LabelText("继续飞行")]
        ContinueFly,
        [LabelText("碰到就消失")]
        CollisionAndDestroy,
        [LabelText("碰到就反弹")]
        CollisionAndRebound
    }
}