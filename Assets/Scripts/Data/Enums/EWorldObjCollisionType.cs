using Sirenix.OdinInspector;

namespace GameFrame
{
    /// <summary>
    /// 物体的碰撞等级
    /// </summary>
    public enum EWorldObjCollisionType
    {
        [LabelText("正常等级--通常场景中的静态物体")]
        NormalLevel,
        [LabelText("中等等级--")]
        MidLevel,
        [LabelText("高等等级--")]
        HighLevel,
        [LabelText("特殊等级--")]
        SpecialLevel,
    }
}