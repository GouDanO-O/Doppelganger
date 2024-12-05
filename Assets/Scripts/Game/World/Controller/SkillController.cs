using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using QFramework;
using UnityEngine;

namespace GameFrame.World
{
    public class SkillController : AbstractController
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
        public BindableList<SOwnedSkillData> curOwnedSkillNodes { get;protected set; } = new BindableList<SOwnedSkillData>();
        
        /// <summary>
        /// 主动技能
        /// </summary>
        public BindableDictionary<int, SOwnedSkillData> curOwnedSkills_Initiative { get;protected set; } = new BindableDictionary<int, SOwnedSkillData>();
        
        /// <summary>
        /// 被动技能
        /// </summary>
        public BindableList<SOwnedSkillData> curOwnedSkills_Passive { get;protected set; } = new BindableList<SOwnedSkillData>();
        
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
        protected virtual void SetOutSideSkill()
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
        protected virtual void LevelUp()
        {
            curLevel.Value++;
            AddSkillPoint(1);
        }

        /// <summary>
        /// 增加技能点
        /// </summary>
        /// <param name="addCount"></param>
        public virtual void AddSkillPoint(int addCount)
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
        public virtual void GetSkill(SkillNodeDataConfig skillNode)
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
                    curOwnedSkillNodes.Add(new SOwnedSkillData(skillNode));
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
        protected bool CheckIsSatisfySkill(SOwnedSkillData skill)
        {
            if (CheckHasSkill(skill.skillNodeDataConfig) != -1)
            {
                SkillConditionConfig formula=skill.skillNodeDataConfig.SkillCondition;
                if (formula == default)
                    return true;

                
            }
            return false;
        }
        
        
        /// <summary>
        /// 使用指定技能
        /// </summary>
        /// <param name="skillNode"></param>
        public virtual void UseSkill(SOwnedSkillData skill)
        {
            if (CheckIsSatisfySkill(skill))
            {
                skill.TriggerSkill(owner);
            }
        }

        /// <summary>
        /// 流程式使用技能
        /// </summary>
        public virtual void UseSkill()
        {
            for (int i = 0; i < Skill_Outside.Count; i++)
            {
                Skill_Outside[i].TriggerSkill(owner);
            }
        }
    }
}

