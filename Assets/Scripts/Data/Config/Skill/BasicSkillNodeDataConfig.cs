using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.Config
{
    [CreateAssetMenu(fileName = "BasicSkill",menuName = "配置/技能/基础技能配置")]
    public class BasicSkillNodeDataConfig : ScriptableObject
    {
        public int SkillID;

        public string SkillName;
        
        public string SkillDescription;

        public List<BasicSkillNodeDataConfig> PrerequisiteSkills;

        public int RequiredLevel;

        public int SkillPointsCost;
    }
}

