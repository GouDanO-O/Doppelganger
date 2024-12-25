using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    [CreateAssetMenu(fileName = "SkillTree",menuName = "配置/技能/技能树")]
    public class SkillTree_Config : SerializedScriptableObject
    {
        [LabelText("局内技能树")]
        public List<SkillNodeData_Config> Skill_Inside = new List<SkillNodeData_Config>();
        
        [LabelText("局外技能树--固有技能,当进入游戏就会自动初始化")]
        public List<SkillNodeData_Config> Skill_Outside = new List<SkillNodeData_Config>();
    }
}

