using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using GameFrame.World;
using QFramework;
using UnityEngine;

namespace GameFrame.World
{
    /// <summary>
    /// 技能控制器
    /// </summary>
    public class SkillController : AbstractController
    {
        /// <summary>
        /// 当前拥有的局内技能
        /// </summary>
        protected List<SkillNodeDataConfig> Skill_Inside = new List<SkillNodeDataConfig>();
        
        /// <summary>
        /// 当前拥有的局外技能
        /// </summary>
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
        /// 当前拥有的所有技能--所有的技能
        /// 这些技能仅代表--当前玩家获得了这些技能,并不代表他们要使用这些技能
        /// 需要玩家主动添加到技能列表中,才会激活
        /// 仅用来查询,不能进行使用
        /// </summary>
        public BindableList<OwnedSkillModel> curOwnedSkillNodes { get;protected set; } = new BindableList<OwnedSkillModel>();
        
        /// <summary>
        /// 主动技能
        /// 只有添加到主动列表中的技能,才会添加进列表
        /// 如果仅激活技能,但是没有放进主动列表中,则不会添加进列表
        /// </summary>
        public BindableDictionary<int, OwnedSkillModel> curOwnedSkills_Initiative { get;protected set; } = new BindableDictionary<int, OwnedSkillModel>();
        
        /// <summary>
        /// 被动技能
        /// 只有添加到被动列表中的技能,才会添加进列表
        /// 如果仅获得技能,但是没有放进被动列表中,则不会添加进列表
        /// </summary>
        public BindableList<OwnedSkillModel> curOwnedSkills_Passive { get;protected set; } = new BindableList<OwnedSkillModel>();
        
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
                    OwnedSkillModel newSkill = OwnedSkillModel.Allocate();
                    newSkill.InitData(owner,skillNode);
                    curOwnedSkillNodes.Add(newSkill);
                }
            }
        }

        /// <summary>
        /// 激活主动技能--即添加到主动技能列表
        /// </summary>
        public virtual void ActivateSkill_Initiative()
        {
            
        }

        /// <summary>
        /// 激活被动技能--即添加到被动技能列表
        /// </summary>
        public virtual void ActivateSkill_Passivity()
        {
            
        }

        /// <summary>
        /// 注销主动技能
        /// 从主动技能列表中移除
        /// 并不代表将这个技能从拥有的技能中移除
        /// 玩家后续仍可以再次激活这个技能
        /// </summary>
        public virtual void UnActivateSkill_Initiative()
        {
            
        }
        
        /// <summary>
        /// 注销被动技能
        /// 从被动技能列表中移除
        /// 并不代表将这个技能从拥有的技能中移除
        /// 玩家后续仍可以再次激活这个技能
        /// </summary>
        public virtual void UnActivateSkill_Passivity()
        {
            
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
        protected bool CheckIsSatisfySkill(OwnedSkillModel skill)
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
        public virtual void UseSkill(OwnedSkillModel skill)
        {
            if (CheckIsSatisfySkill(skill))
            {
                skill.ExcuateSkillCheck();
            }
        }

        /// <summary>
        /// 流程式使用技能
        /// </summary>
        public virtual void UseSkill()
        {
            for (int i = 0; i < Skill_Outside.Count; i++)
            {
                
            }
        }
    }
}

