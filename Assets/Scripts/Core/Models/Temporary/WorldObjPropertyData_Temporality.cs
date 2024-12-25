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
        /// 伤害加成
        /// </summary>
        public float DamageAddition;

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

        public void InitData(WorldObjData_Config dataConfig)
        {
            this.MaxHealth = dataConfig.HealthyData.maxHealth;
            this.MaxArmor = dataConfig.HealthyData.maxArmor;
            this.DamageReductionRatio = dataConfig.HealthyData.damageReductionRatio;

            this.CriticalDamage = dataConfig.PlayerWeaponData.CriticalDamage;
            this.CriticalRate=dataConfig.PlayerWeaponData.CriticalRate;
            
            this.ElementDamageDataDict = dataConfig.ElementDamageDataDict;
            this.ElementPropertyDataDict = dataConfig.ElementPropertyDataDict;
        }

        /// <summary>
        /// 伤害倍率
        /// </summary>
        /// <returns></returns>
        public float GetDamageAddition()
        {
            return DamageAddition;
        }

        /// <summary>
        /// 元素伤害倍率
        /// 要叠加总伤害倍率
        /// </summary>
        /// <param name="elementType"></param>
        /// <returns></returns>
        public float GetElementDamageAddition(EElementType elementType)
        {
            return DamageAddition * GetElementBonusDamage(elementType);
        }
        
        /// <summary>
        /// 伤害减免
        /// </summary>
        /// <returns></returns>
        public float GetDamageReductionRatio()
        {
            return 1 - DamageReductionRatio;
        }

        /// <summary>
        /// 元素伤害抗性
        /// 要再叠加一层伤害抗性
        /// </summary>
        /// <param name="elementType"></param>
        /// <returns></returns>
        public float GetElementDamageReductionRatio(EElementType elementType)
        {
            return (1+DamageReductionRatio) * GetElementReductionRatio(elementType);
        }

        /// <summary>
        /// 获取元素配置
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public ElementDamageData_Persistent GetElementDamageData(EElementType element)
        {
            if (ElementDamageDataDict.ContainsKey(element))
            {
                return ElementDamageDataDict[element];
            }
            return null;
        }

        private float GetElementBonusDamage(EElementType element)
        {
            if (ElementPropertyDataDict.ContainsKey(element))
            {
                return ElementPropertyDataDict[element].ElementBonusDamage;
            }

            return 1;
        }

        private float GetElementReductionRatio(EElementType element)
        {
            if (ElementPropertyDataDict.ContainsKey(element))
            {
                return ElementPropertyDataDict[element].ElementResistance;
            }

            return 1;
        }

        public override void DeInitData()
        {
            
        }
    }
}