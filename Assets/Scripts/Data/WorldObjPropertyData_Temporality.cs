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
        /// 元素伤害配置--归属玩家的不可成长属性--是对其他物体或玩家造成的伤害
        /// </summary>
        public Dictionary<EElementType,ElementDamageData_Persistent> ElementDamageDataDict=new Dictionary<EElementType, ElementDamageData_Persistent>();
        
        /// <summary>
        /// 元素属性配置--归属玩家的可成长数据
        /// </summary>
        public Dictionary<EElementType,SElementPropertyData> ElementPropertyDataDict=new Dictionary<EElementType, SElementPropertyData>();

        public void InitData(WorldObjDataConfig dataConfig)
        {
            this.MaxHealth = dataConfig.HealthyData.maxHealth;
            this.MaxArmor = dataConfig.HealthyData.maxArmor;
            this.DamageReductionRatio = dataConfig.HealthyData.damageReductionRatio;

            this.CriticalDamage = dataConfig.PlayerWeaponData.CriticalDamage;
            this.CriticalRate=dataConfig.PlayerWeaponData.CriticalRate;
            
            this.ElementDamageDataDict = dataConfig.ElementDamageDataDict;
            this.ElementPropertyDataDict = dataConfig.ElementPropertyDataDict;
        }
        
        public float GetDamageReductionRatio()
        {
            return 1 - DamageReductionRatio;
        }

        public ElementDamageData_Persistent GetElementDamageData(EElementType element)
        {
            if (ElementDamageDataDict.ContainsKey(element))
            {
                return ElementDamageDataDict[element];
            }
            return null;
        }

        public float GetElementBonusDamage(EElementType element)
        {
            if (ElementPropertyDataDict.ContainsKey(element))
            {
                return ElementPropertyDataDict[element].ElementBonusDamage;
            }

            return 1;
        }

        public float GetElementDamageReductionRatio(EElementType element)
        {
            if (ElementPropertyDataDict.ContainsKey(element))
            {
                return ElementPropertyDataDict[element].ElementResistance;
            }

            return 0;
        }

        public override void DeInitData()
        {
            
        }
    }
}