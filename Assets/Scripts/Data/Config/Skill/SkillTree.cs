using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.Config
{
    [CreateAssetMenu(fileName = "SkillTree",menuName = "配置/技能/技能树")]
    public class SkillTree : ScriptableObject
    {
        [Header("局内技能树")]
        public List<SkillNodeDataConfig> Skill_Inside = new List<SkillNodeDataConfig>();
        
        [Header("局外技能树")]
        public List<SkillNodeDataConfig> Skill_Outside = new List<SkillNodeDataConfig>();
    }
}

