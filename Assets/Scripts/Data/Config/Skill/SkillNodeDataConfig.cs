using System;
using System.Collections.Generic;
using GameFrame.World;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    
    [CreateAssetMenu(fileName = "BasicSkill", menuName = "配置/技能/基础技能配置")]
    public class SkillNodeDataConfig : SerializedScriptableObject
    {
        #region 技能基本信息

        [BoxGroup("技能基本信息")]
        [LabelText("技能ID")]
        public int SkillID;

        [BoxGroup("技能基本信息")]
        [LabelText("技能名称")]
        public string SkillName;

        [BoxGroup("技能基本信息")]
        [LabelText("技能描述"), TextArea]
        public string SkillDescription;

        [BoxGroup("技能基本信息")]
        [LabelText("技能所属节点类型")]
        public ESkill_NodeTreeType SkillNodeType;

        [BoxGroup("技能基本信息")]
        [LabelText("技能触发类型\n主动--会添加到主动技能列表中(当玩家互动且满足使用条件时会释放)\n被动--当满足使用条件时会自动释放", true)]
        public ETriggerType SkillSpellType;

        [BoxGroup("技能基本信息")]
        [LabelText("主动技能释放方式"), ShowIf("@SkillSpellType == ETriggerType.ExecuteTrigger")]
        public ESkill_TriggerConditionType SkillTriggerConditionType;

        [BoxGroup("技能基本信息")]
        [LabelText("所需等级(-1表示无等级限制)")]
        public int RequiredLevel;

        [BoxGroup("技能基本信息")]
        [LabelText("所需技能点数")]
        public int SkillPointsCost;

        [BoxGroup("技能基本信息")]
        [LabelText("前置技能列表")]
        public List<SkillNodeDataConfig> PrerequisiteSkills;

        [BoxGroup("技能基本信息")]
        [LabelText("CD")]
        public float SkillCooldown;

        [BoxGroup("技能基本信息")]
        [LabelText("最大可升级等级")]
        public int MaxLevel;

        [BoxGroup("技能基本信息")]
        [LabelText("每个升级的等级配置"), ShowIf("@this.MaxLevel > 0")]
        public SkillNodeDataConfig[] LevelUpSkills;
        
        [BoxGroup("技能基本信息")]
        [LabelText("技能执行条件")]
        public SkillConditionConfig SkillCondition;

        #endregion

        #region 轨道列表

        [BoxGroup("技能行为轨道"),LabelText("技能行为轨道")]
        public List<SkillTrackConfig> SkillTracks = new List<SkillTrackConfig>();
        #endregion

        public void TriggerSkill(WorldObj owner=null, WorldObj target=null)
        {
            for (int i = 0; i < SkillTracks.Count; i++)
            {
                SkillTracks[i].Trigger(owner, target);
            }
        }
    }
}
