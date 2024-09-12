using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.Config
{
    [CreateAssetMenu(fileName = "MonsterDataConfig", menuName = "配置/世界/怪物配置")]
    public class MonsterDataConfig : BiologyDataConfig
    {
        public bool attackable;

        public SAttackData attackData;
        
        public SkillTree skillTree;
    }

    /// <summary>
    /// 攻击数据配置
    /// </summary>
    [Serializable]
    public struct SAttackData
    {
        public float basicDamage;
        
        public float criticalRate;
        
        public float criticalDamage;

        public EAttackType attackType;
        
        public float attackDistance;
    }

    public enum EAttackType
    {
        None = -1,
        CloseAttack,
        RemoteAttack
    }
}

