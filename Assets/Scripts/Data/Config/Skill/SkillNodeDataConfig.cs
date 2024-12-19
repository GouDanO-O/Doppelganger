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

        [BoxGroup("技能行为轨道"),LabelText("具体的技能逻辑由这条轨道中的行为各自去决定")]
        public List<SkillTrackConfig> SkillTracks = new List<SkillTrackConfig>();
        #endregion

        /// <summary>
        /// 这是一个技能
        /// 一个技能里面可能会有多条轨道
        /// 分别执行每条行为轨道里面的行为
        /// 且每条轨道总时序必须保持一致
        /// 目前的做法是每条轨道里面的时序由自己触发
        /// 而非由一个总轴去执行
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="target"></param>
        public void TriggerSkill(WorldObj owner=null)
        {
            for (int i = 0; i < SkillTracks.Count; i++)
            {
                SkillTracks[i].Trigger(owner);
            }
        }
    }
}
