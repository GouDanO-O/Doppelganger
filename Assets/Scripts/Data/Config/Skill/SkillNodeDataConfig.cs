using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.Config
{
    public enum ESkillNodeType
    {
        Live,
        Combat,
        Special
    }

    /// <summary>
    /// 触发类型
    /// </summary>
    public enum ETriggerType
    {
        /// <summary>
        /// 主动触发
        /// </summary>
        Active,
        /// <summary>
        /// 被动触发
        /// </summary>
        Passive
    }

    /// <summary>
    /// 触发目标
    /// </summary>
    public enum ETriggerTarget
    {
        /// <summary>
        /// 使用者
        /// </summary>
        User,
        /// <summary>
        /// 技能目标
        /// </summary>
        Target,
        /// <summary>
        /// 技能持有者(可以是自己也可以是其他对象)
        /// </summary>
        SkillOwner,
        /// <summary>
        /// 其他
        /// </summary>
        Other
    }
    
    [CreateAssetMenu(fileName = "BasicSkill",menuName = "配置/技能/基础技能配置")]
    public class SkillNodeDataConfig : ScriptableObject
    {
        [Header("技能所属节点类型")]
        public ESkillNodeType SkillNodeType;
        
        [Header("技能触发类型\n主动--会添加到主动技能列表中(当玩家互动且满足使用条件时会释放)\n被动--当满足使用条件时会自动释放")]
        public ETriggerType TriggerType;
        
        [Header("技能ID")]
        public int SkillID;
        
        [Header("技能名称")]
        public string SkillName;
        
        [Header("技能描述")]
        public string SkillDescription;
        
        [Header("所需等级")]
        public int RequiredLevel;
        
        [Header("所需技能点数")]
        public int SkillPointsCost;
        
        [Header("前置技能列表")]
        public List<SkillNodeDataConfig> PrerequisiteSkills;

        [Header("CD")]
        public float SkillCooldown;

        [Header("最大等级")]
        public int MaxLevel;
    }
}

