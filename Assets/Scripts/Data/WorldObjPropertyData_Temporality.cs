using System;
using System.Collections.Generic;
using GameFrame.Config;
using GameFrame.World;

namespace GameFrame
{
    /// <summary>
    /// 物体当前对局数据
    /// </summary>
    [Serializable]
    public class WorldObjPropertyData_Temporality : TemporalityData
    {
        /// <summary>
        /// 最大生命值
        /// </summary>
        public float MaxHealth;
        
        /// <summary>
        /// 最大护甲
        /// </summary>
        public float MaxArmor;

        /// <summary>
        /// 伤害减免率
        /// </summary>
        public float DamageReductionRatio;

        /// <summary>
        /// 暴击率
        /// </summary>
        public float CriticalRate;

        /// <summary>
        /// 暴击伤害
        /// </summary>
        public float CriticalDamage;
        
        /// <summary>
        /// 元素伤害配置
        /// </summary>
        public Dictionary<EElementType,ElementDamageData_Persistent> ElementDamageData=new Dictionary<EElementType, ElementDamageData_Persistent>();
        
        /// <summary>
        /// 元素伤害倍率
        /// </summary>
        public Dictionary<EElementType,float> ElementBonusDamage=new Dictionary<EElementType, float>();
        
        /// <summary>
        /// 元素伤害抗性
        /// </summary>
        public Dictionary<EElementType,float> ElementResistance=new Dictionary<EElementType, float>();

        public float GetDamageReductionRatio()
        {
            return 1 - DamageReductionRatio;
        }

        public override void DeInitData()
        {
            
        }
    }
}