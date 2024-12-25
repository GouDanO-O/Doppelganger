using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine.Events;

namespace GameFrame.World
{
    public class SkillExecuteManager
    {
        public List<SkillExecuter_TemporalityPoolable>
            skillExecuterList = new List<SkillExecuter_TemporalityPoolable>();

        public SkillExecuteManager()
        {
            InitData();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitData()
        {
            
        }

        /// <summary>
        /// 轮循里面的技能执行块
        /// </summary>
        public void UpdateSkillExecution(float deltaTime)
        {
            if (skillExecuterList.Count > 0)
            {
                for (int i = 0; i < skillExecuterList.Count; i++)
                {
                    SkillExecuter_TemporalityPoolable skillExecuter = skillExecuterList[i];
                    skillExecuter.TimeCheck(deltaTime);
                }
            }
        }
        
        /// <summary>
        /// 添加技能
        /// </summary>
        /// <param name="skillDataTemporalityPoolable"></param>
        public void AddSkillExecuter(OwnedSkillData_TemporalityPoolable skillDataTemporalityPoolable)
        {
            SkillExecuter_TemporalityPoolable skillExecuter = SkillExecuter_TemporalityPoolable.Allocate();
            skillExecuter.InitData(this,skillDataTemporalityPoolable);
            skillExecuterList.Add(skillExecuter);
        }

        /// <summary>
        /// 移除技能数据
        /// 当技能执行完毕后就移除这个技能
        /// </summary>
        /// <param name="skillDataTemporalityPoolable"></param>
        public void RemoveSkillExecuter(SkillExecuter_TemporalityPoolable skillExecuterTemporalityPoolable)
        {
            if (skillExecuterList.Contains(skillExecuterTemporalityPoolable))
            {
                skillExecuterList.Remove(skillExecuterTemporalityPoolable);
            }
        }
    }
}