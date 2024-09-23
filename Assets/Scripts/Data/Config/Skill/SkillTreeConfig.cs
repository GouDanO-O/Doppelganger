using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    [CreateAssetMenu(fileName = "SkillTree",menuName = "配置/技能/技能树")]
    public class SkillTreeConfig : SerializedScriptableObject
    {
        [Header("局内技能树")]
        public List<SkillNodeDataConfig> Skill_Inside = new List<SkillNodeDataConfig>();
        
        [Header("局外技能树")]
        public List<SkillNodeDataConfig> Skill_Outside = new List<SkillNodeDataConfig>();
    }
}

