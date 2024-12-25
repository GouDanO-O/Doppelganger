using System;
using Sirenix.OdinInspector;

namespace GameFrame
{
    /// <summary>
    /// 元素属性配置--归属玩家的可成长数据
    /// </summary>
    [Serializable]
    public struct SElementPropertyData
    {
        [LabelText("元素伤害倍率")]
        public float ElementBonusDamage;
        
        [LabelText("元素伤害抗性")]
        public float ElementResistance;
    }
}