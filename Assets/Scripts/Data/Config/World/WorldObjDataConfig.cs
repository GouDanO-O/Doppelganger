using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.Config
{
    [CreateAssetMenu(menuName = "配置/世界/基础物体配置")]
    public class WorldObjDataConfig : ScriptableObject
    {
        public float gravity;
        
        public bool moveable;
        
        public SMoveData moveData;

        public bool healthyable;
        
        public SHealthyData healthyData;
        
        public GameObject thisPrefab;
    }

    /// <summary>
    /// 移动数据配置
    /// </summary>
    [Serializable]
    public struct SMoveData
    {
        public float walkSpeed;
        
        public float runSpeed;
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

