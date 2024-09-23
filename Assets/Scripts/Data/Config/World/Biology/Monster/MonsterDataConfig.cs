using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Sirenix.Utilities.Editor;
using UnityEditor;

namespace GameFrame.Config
{
    [CreateAssetMenu(fileName = "MonsterDataConfig", menuName = "配置/世界/怪物配置")]
    public class MonsterDataConfig : BiologyDataConfig
    {
        [LabelText("能否进行攻击")]
        public bool attackable;
        
        [ShowIf("attackable"),LabelText("能否进行攻击")]
        public SAttackData attackData;
    }
    
    /// <summary>
    /// 攻击数据配置
    /// </summary>
    [Serializable]
    public struct SAttackData
    {
        [LabelText("基础伤害")]
        public float basicDamage;
        
        [LabelText("暴击率"),Range(0,100)]
        public float criticalRate;
        
        [LabelText("暴击伤害")]
        public float criticalDamage;
        
        [LabelText("攻击距离")]
        public float attackDistance;
    }
    
}

