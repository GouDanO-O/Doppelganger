using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    [CreateAssetMenu(fileName = "",menuName = "配置/世界/基础物体配置")]
    public class WorldObjDataConfig : SerializedScriptableObject
    {
        [Required(InfoMessageType.Error),LabelText("预制体")]
        public GameObject thisPrefab;
        
        [LabelText("是否有技能")]
        public bool hasSkill;
        
        [ShowIf("hasSkill"),LabelText("技能树")]
        public SkillTreeConfig skillTree;
        
        [LabelText("重力"),Range(0,20)]
        public float gravity;
        
        [LabelText("是否具有生命")]
        public bool healthyable;
        
        [ShowIf("healthyable"),LabelText("生命数据")]
        public SHealthyData healthyData;
    }
    
    /// <summary>
    /// 生命数据配置
    /// </summary>
    [Serializable]
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

