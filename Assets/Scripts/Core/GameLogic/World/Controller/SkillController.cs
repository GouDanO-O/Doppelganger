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
        protected List<SkillNodeData_Config> Skill_Inside = new List<SkillNodeData_Config>();
        
        /// <summary>
        /// 当前拥有的局外技能
        /// </summary>
        protected List<SkillNodeData_Config> Skill_Outside = new List<SkillNodeData_Config>();

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
        public BindableList<OwnedSkillData_TemporalityPoolable> curOwnedSkillNodes { get;protected set; } = new BindableList<OwnedSkillData_TemporalityPoolable>();
        
        /// <summary>
        /// 主动技能
        /// 只有添加到主动列表中的技能,才会添加进列表
        /// 如果仅激活技能,但是没有放进主动列表中,则不会添加进列表
        /// </summary>
        public BindableDictionary<int, OwnedSkillData_TemporalityPoolable> curOwnedSkills_Initiative { get;protected set; } = new BindableDictionary<int, OwnedSkillData_TemporalityPoolable>();
        
        /// <summary>
        /// 被动技能
        /// 只有添加到被动列表中的技能,才会添加进列表
        /// 如果仅获得技能,但是没有放进被动列表中,则不会添加进列表
        /// </summary>
        public BindableList<OwnedSkillData_TemporalityPoolable> curOwnedSkills_Passive { get;protected set; } = new BindableList<OwnedSkillData_TemporalityPoolable>();

        #region  初始化

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="skillTree"></param>
        public override void InitData(WorldObj owner)
        {
            base.InitData(owner);
            SkillTree_Config skillTree = owner.thisDataConfig.skillTree;
            Skill_Inside.AddRange(skillTree.Skill_Inside);
            Skill_Outside.AddRange(skillTree.Skill_Outside);

            SetInsideSkill();
            SetOutSideSkill();
        }
        
        /// <summary>
        /// 注销
        /// </summary>
        public override void DeInitData()
        {
            
        }

        /// <summary>
        /// 设置局内技能
        /// </summary>
        protected virtual void SetInsideSkill()
        {
            for (int i = 0; i < Skill_Inside.Count; i++)
            {
                GetSkill(Skill_Inside[i]);
            }
        }

        /// <summary>
        /// 设置局外技能
        /// </summary>
        protected virtual void SetOutSideSkill()
        {
            for (int i = 0; i < Skill_Outside.Count; i++)
            {
                GetSkill(Skill_Outside[i]);
            }
        }

        #endregion

        
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
        /// 首先检测是否拥有该技能
        /// 如果已经拥有,那么升级该技能
        /// 否则添加该技能数据块
        /// </summary>
        public virtual void GetSkill(SkillNodeData_Config skillNode)
        {
            int skillIndex = CheckHasSkill(skillNode);
            if (skillIndex !=-1)
            {
                curOwnedSkillNodes[skillIndex].LevelUp();
            }
            else
            {
                OwnedSkillData_TemporalityPoolable newSkillData = OwnedSkillData_TemporalityPoolable.Allocate();
                newSkillData.InitData(owner,skillNode);
                curOwnedSkillNodes.Add(newSkillData);
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
        protected int CheckHasSkill(SkillNodeData_Config skillNode)
        {
            int skills = -1;
            for (int i = 0; i < curOwnedSkillNodes.Count; i++)
            {
                if (curOwnedSkillNodes[i].CheckSkill(skillNode))
                {
                    skills = i;
                    break;
                }
            }

            return skills;
        }
        
        /// <summary>
        /// 检查是否满足技能的施法条件
        /// </summary>
        /// <returns></returns>
        protected bool CheckIsSatisfySkill(OwnedSkillData_TemporalityPoolable skillData)
        {
            if (CheckHasSkill(skillData.skillNodeDataConfig) != -1)
            {
                List<ISkillCondition> formula=skillData.skillNodeDataConfig.SkillCondition;
                if (formula == null || formula.Count == 0)
                    return true;

                int curSatisfyCount = 0;
                int maxSatisfyCount = formula.Count;
                for (int i = 0; i < maxSatisfyCount; i++)
                {
                    if (formula[i].CheckCondition(owner, skillData.curLevel))
                    {
                        curSatisfyCount++;
                    }
                }
                
                if (curSatisfyCount == maxSatisfyCount)
                {
                    for (int i = 0; i < maxSatisfyCount; i++)
                    {
                        formula[i].ExcuteCondition(owner, skillData.curLevel);
                    }
                    return true;
                }
            }
            return false;
        }
        
        
        /// <summary>
        /// 使用指定技能
        /// </summary>
        /// <param name="skillNode"></param>
        public virtual void UseSkill(OwnedSkillData_TemporalityPoolable skillData)
        {
            if (CheckIsSatisfySkill(skillData))
            {
                skillData.ExcuateSkillCheck();
            }
        }

        /// <summary>
        /// 流程式使用技能
        /// </summary>
        public virtual void UseSkill()
        {
            for (int i = 0; i < curOwnedSkillNodes.Count; i++)
            {
                UseSkill(curOwnedSkillNodes[i]);
            }
        }
    }
}

