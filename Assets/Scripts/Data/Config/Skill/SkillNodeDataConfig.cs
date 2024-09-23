using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    [CreateAssetMenu(fileName = "BasicSkill",menuName = "配置/技能/基础技能配置")]
    public class SkillNodeDataConfig : SerializedScriptableObject
    {
        [LabelText("技能ID")]
        public int SkillID;
        
        [LabelText("技能名称")]
        public string SkillName;
        
        [LabelText("技能描述")]
        public string SkillDescription;
        
        [LabelText("技能所属节点类型")]
        public ESkillNodeType SkillNodeType;
        
        [LabelText("技能触发类型\n主动--会添加到主动技能列表中(当玩家互动且满足使用条件时会释放)\n被动--当满足使用条件时会自动释放",true)]
        public ESkillSpellType skillSpellType;
        
        [LabelText("所需等级")]
        public int RequiredLevel;
        
        [LabelText("所需技能点数")]
        public int SkillPointsCost;
        
        [LabelText("前置技能列表")]
        public List<SkillNodeDataConfig> PrerequisiteSkills;
        
        [LabelText("CD")]
        public float SkillCooldown;

        [LabelText("最大等级")]
        public int MaxLevel;
    }
}

