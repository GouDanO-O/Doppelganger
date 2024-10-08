using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    [CreateAssetMenu(fileName = "SkillTree",menuName = "配置/技能/技能树")]
    public class SkillTreeConfig : SerializedScriptableObject
    {
        [LabelText("局内技能树")]
        public List<SkillNodeDataConfig> Skill_Inside = new List<SkillNodeDataConfig>();
        
        [LabelText("局外技能树--固有技能,当进入游戏就会自动初始化")]
        public List<SkillNodeDataConfig> Skill_Outside = new List<SkillNodeDataConfig>();
    }
}

