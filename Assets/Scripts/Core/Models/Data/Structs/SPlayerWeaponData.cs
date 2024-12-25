using System;
using Sirenix.OdinInspector;

namespace GameFrame
{
    [Serializable,LabelText("攻击数据", SdfIconType.Box)]
    public struct SPlayerWeaponData
    {
        [LabelText("暴击率")]
        public float CriticalRate;
        
        [LabelText("暴击伤害")]
        public float CriticalDamage;
    }
}