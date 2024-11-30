using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame
{
    /// <summary>
    /// 生命数据配置
    /// </summary>
    [Serializable,LabelText("生命数据", SdfIconType.Box)]
    public struct SHealthyData
    {
        [LabelText("最大生命值")]
        public float maxHealth;
        
        [LabelText("最大护甲值")]
        public float maxArmor;
        
        [LabelText("伤害减免率"),Range(0,1)]
        public float damageReductionRatio;
    }
}