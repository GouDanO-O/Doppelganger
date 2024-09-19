using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using QFramework;
using UnityEngine;

namespace GameFrame.World
{

    
    public class SkillController : MonoBehaviour,IController
    {
        protected List<BasicSkillNodeDataConfig> Skill_Inside = new List<BasicSkillNodeDataConfig>();
        
        protected List<BasicSkillNodeDataConfig> Skill_Outside = new List<BasicSkillNodeDataConfig>();

        /// <summary>
        /// 可变变量--当发生改变时会自动调用订阅的事件
        /// </summary>
        public BindableProperty<int> curLevel { get; protected set; } = new BindableProperty<int>();
        
        public BindableProperty<int> curOwnedSkillPoint { get;protected set; } = new BindableProperty<int>();
        
        public BindableList<BasicSkillNodeDataConfig> curOwnedSkillNodes { get;protected set; } = new BindableList<BasicSkillNodeDataConfig>();
        
        
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
        /// 设置技能
        /// </summary>
        /// <param name="skillNode"></param>
        protected void SetSkill(BasicSkillNodeDataConfig skillNode)
        {
            if (skillNode.CompositeSkillBehaviorConfig)
            {
                skillNode.CompositeSkillBehaviorConfig.ExecuteSkill(this.gameObject,null);
            }
        }


    }
}

