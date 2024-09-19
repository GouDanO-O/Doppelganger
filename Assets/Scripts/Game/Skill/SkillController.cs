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
        public void TriggerSkill(GameObject owner=null,GameObject target=null)
        {
            if (IsSkillReady())
            {
                this.skillNodeDataConfig.CompositeSkillBehaviorConfig.ExecuteSkill(owner,target,curLevel);
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
            skillNodeDataConfig.LevelUp(curLevel);
        }   
        
        /// <summary>
        /// 升级
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
    
    public class SkillController : MonoBehaviour,IController
    {
        protected List<SkillNodeDataConfig> Skill_Inside = new List<SkillNodeDataConfig>();
        
        protected List<SkillNodeDataConfig> Skill_Outside = new List<SkillNodeDataConfig>();

        /// <summary>
        /// 可变变量--当发生改变时会自动调用订阅的事件
        /// </summary>
        public BindableProperty<int> curLevel { get; protected set; } = new BindableProperty<int>();
        
        public BindableProperty<int> curOwnedSkillPoint { get;protected set; } = new BindableProperty<int>();
        
        public BindableList<SOwnedSkill> curOwnedSkillNodes { get;protected set; } = new BindableList<SOwnedSkill>();
        
        
        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="skillTree"></param>
        public void Init(SkillTree skillTree)
        {
            Skill_Inside.AddRange(skillTree.Skill_Inside);
            Skill_Outside.AddRange(skillTree.Skill_Outside);

            SetOutSideSkill();
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
                    SetSkill(Skill_Outside[i]);
                }
            }
        }

        /// <summary>
        /// 升级
        /// </summary>
        protected void LevelUp()
        {
            
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
            for (int i = 0; i < curOwnedSkillNodes.Count; i++)
            {
                if (curOwnedSkillNodes[i].CheckSkill(skillNode))
                {
                    curOwnedSkillNodes[i].LevelUp();
                }
                else
                {
                    curOwnedSkillNodes.Add(new SOwnedSkill(skillNode));
                }
            }
        }
        
        /// <summary>
        /// 设置技能
        /// </summary>
        /// <param name="skillNode"></param>
        protected void SetSkill(SkillNodeDataConfig skillNode)
        {
            if (skillNode.CompositeSkillBehaviorConfig)
            {
                skillNode.CompositeSkillBehaviorConfig.ExecuteSkill(this.gameObject,null,0);
            }
        }
    }
}

