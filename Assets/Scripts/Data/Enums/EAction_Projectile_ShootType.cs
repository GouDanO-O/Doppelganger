using Sirenix.OdinInspector;

namespace GameFrame
{
    [LabelText("发射弹体形式")]
    public enum EAction_Projectile_ShootType
    {
        [LabelText("直线")]
        Line,
        [LabelText("抛物线")]
        Parabola,
    }
}