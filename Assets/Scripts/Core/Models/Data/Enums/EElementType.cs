using Sirenix.OdinInspector;

namespace GameFrame
{
    [LabelText("元素伤害类型")]
    public enum EElementType
    {
        [LabelText("无特殊属性")]
        None,
        [LabelText("真实伤害")]
        TrueInjury,
        [LabelText("火属性--灼伤")]
        Fire,
        [LabelText("冰属性--减速")]
        Ice,
        [LabelText("毒属性--毒伤+破防")]
        Poision,
        [LabelText("雷属性--易伤")]
        Electricity,
        [LabelText("光属性--达到层数时眩晕")]
        Light,
        [LabelText("暗属性--虚弱")]
        Dark
    }
}