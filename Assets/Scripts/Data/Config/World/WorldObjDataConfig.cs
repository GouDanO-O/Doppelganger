using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.Config
{
    [CreateAssetMenu(menuName = "配置/世界/基础物体配置")]
    public class WorldObjDataConfig : ScriptableObject
    {
        public GameObject thisPrefab;
        
        public float gravity;
        
        public bool healthyable;
        
        public SHealthyData healthyData;
        
        public SkillTree skillTree;
    }
    
    /// <summary>
    /// 生命数据配置
    /// </summary>
    [Serializable]
    public struct SHealthyData
    {
        public float maxHealth;
        
        public float maxArmor;

        public float damageReductionRatio;
    }
    
}

