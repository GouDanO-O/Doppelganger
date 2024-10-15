using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using QFramework;
using UnityEngine;

namespace GameFrame.World
{
    public class SOwnedSkill
    {
        public SkillNodeDataConfig skillNodeDataConfig { get;protected set; }
        
        public int curLevel { get; set; }

        public int maxLevel { get; set; }
        
        /// <summary>
        /// 上一次使用技能的时间戳
        /// </summary>
        public float lastSkillUseTime { get; set; }
        
        /// <summary>
        /// 检测是否正确抵达时间--防止本地和服务器的时间戳对不上
        /// </summary>
        public float willEndTime { get; set; }
        
        public float skillCooldown { get; set; }

        public SOwnedSkill(SkillNodeDataConfig skillNodeDataConfig)
        {
            this.skillNodeDataConfig = skillNodeDataConfig;
            this.curLevel = 0;
            this.lastSkillUseTime = 0;
            this.skillCooldown=skillNodeDataConfig.SkillCooldown;
            this.maxLevel = skillNodeDataConfig.MaxLevel;
        }

        /// <summary>
        /// 触发技能
        /// </summary>
        public void TriggerSkill(WorldObj owner=null,WorldObj target=null)
        {
            if (IsSkillReady())
            {
                lastSkillUseTime = Time.time;
                willEndTime = lastSkillUseTime + skillCooldown;
            }
        }
        
        /// <summary>
        /// 如果当前时间 - 上次使用技能的时间 >= 冷却时间，技能就可以再次使用
        /// </summary>
        /// <returns></returns>
        public bool IsSkillReady()
        {
            if (Time.time <= willEndTime)
            {
                return false;
            }
            return Time.time >= lastSkillUseTime + skillCooldown;
        }
        
        /// <summary>
        /// 升级
        /// </summary>
        public void LevelUp()
        {
            curLevel++;
            if (curLevel >= maxLevel)
            {
                curLevel = maxLevel;
            }
        }   
        
        /// <summary>
        /// 检测是否是相同技能
        /// </summary>
        /// <param name="skillNodeDataConfig"></param>
        /// <returns></returns>
        public bool CheckSkill(SkillNodeDataConfig skillNodeDataConfig)
        {
            if (this.skillNodeDataConfig == skillNodeDataConfig)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    
    public class SkillController : BasicController
    {
        protected List<SkillNodeDataConfig> Skill_Inside = new List<SkillNodeDataConfig>();
        
        protected List<SkillNodeDataConfig> Skill_Outside = new List<SkillNodeDataConfig>();

        /// <summary>
        /// 当前玩家等级(可变变量--当发生改变时会自动调用订阅的事件
        /// </summary>
        public BindableProperty<int> curLevel { get; protected set; } = new BindableProperty<int>();
        
        /// <summary>
        /// 当前拥有的技能点数
        /// </summary>
        public BindableProperty<int> curOwnedSkillPoint { get;protected set; } = new BindableProperty<int>();
        
        /// <summary>
        /// 当前拥有的技能
        /// </summary>
        public BindableList<SOwnedSkill> curOwnedSkillNodes { get;protected set; } = new BindableList<SOwnedSkill>();
        
        /// <summary>
        /// 主动技能
        /// </summary>
        public BindableDictionary<int, SOwnedSkill> curOwnedSkills_Initiative { get;protected set; } = new BindableDictionary<int, SOwnedSkill>();
        
        /// <summary>
        /// 被动技能
        /// </summary>
        public BindableList<SOwnedSkill> curOwnedSkills_Passive { get;protected set; } = new BindableList<SOwnedSkill>();
        
        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="skillTree"></param>
        public override void InitData(WorldObj owner)
        {
            base.InitData(owner);
            SkillTreeConfig skillTree = owner.thisDataConfig.skillTree;
            Skill_Inside.AddRange(skillTree.Skill_Inside);
            Skill_Outside.AddRange(skillTree.Skill_Outside);

            SetOutSideSkill();
        }
        
        /// <summary>
        /// 注销
        /// </summary>
        public override void DeInitData()
        {
            
        }

        /// <summary>
        /// 设置局外技能
        /// </summary>
        protected void SetOutSideSkill()
        {
            for (int i = 0; i < Skill_Outside.Count; i++)
            {
                if (Skill_Outside[i])
                {   
                    GetSkill(Skill_Outside[i]);
                }
            }
        }

        /// <summary>
        /// 升级
        /// </summary>
        protected void LevelUp()
        {
            curLevel.Value++;
            AddSkillPoint(1);
        }

        /// <summary>
        /// 增加技能点
        /// </summary>
        /// <param name="addCount"></param>
        public void AddSkillPoint(int addCount)
        {
            this.curOwnedSkillPoint.Value += addCount;
        }

        /// <summary>
        /// 尝试获取技能
        /// </summary>
        /// <param name="pointCost"></param>
        /// <returns></returns>
        public bool TryGetSkill(int pointCost)
        {
            if (curOwnedSkillPoint.Value - pointCost >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// 获取技能
        /// </summary>
        public void GetSkill(SkillNodeDataConfig skillNode)
        {
            int skillIndex = CheckHasSkill(skillNode);
            if (skillIndex!=-1)
            {
                if (curOwnedSkillNodes[skillIndex].CheckSkill(skillNode))
                {
                    curOwnedSkillNodes[skillIndex].LevelUp();
                }
                else
                {
                    curOwnedSkillNodes.Add(new SOwnedSkill(skillNode));
                }
            }
        }
        
        /// <summary>
        /// 检查是否含有当前技能
        /// </summary>
        /// <param name="skillNode"></param>
        /// <returns></returns>
        protected int CheckHasSkill(SkillNodeDataConfig skillNode)
        {
            int skills = -1;
            for (int i = 0; i < curOwnedSkillNodes.Count; i++)
            {
                if (curOwnedSkillNodes[i].CheckSkill(skillNode))
                {
                    skills=i;
                    break;
                }
            }

            return skills;
        }
        
        /// <summary>
        /// 检查是否满足技能的施法条件
        /// </summary>
        /// <returns></returns>
        protected bool CheckIsSatisfySkill(SOwnedSkill skill)
        {
            if(CheckHasSkill(skill.skillNodeDataConfig)!=-1)
                return true;
            
            
            
            
            return false;
        }   
        
        /// <summary>
        /// 使用技能
        /// </summary>
        /// <param name="skillNode"></param>
        protected void UseSkill(SOwnedSkill skill)
        {
            if (CheckIsSatisfySkill(skill))
            {
                skill.TriggerSkill(owner);
            }
        }

        /// <summary>
        /// 流程式使用技能
        /// </summary>
        public void UseSkill()
        {
            for (int i = 0; i < curOwnedSkills_Passive.Count; i++)
            {
                
            }
        }
    }
}

