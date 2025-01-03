using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.Events;

namespace GameFrame.World
{
    /// <summary>
    /// 技能执行管理器
    /// 执行流程:
    /// 玩家使用技能->读取当前玩家的这个技能的数据块->转换成大技能执行块,并添加到技能管理器中
    /// 大技能执行块里面根据这个玩家这个技能数据块中的具体数据和里面的行为轴再进行细分为小技能行为执行块
    /// 技能管理器轮循当前拥有的技能->检测大技能执行块中每个小技能行为执行块是否执行和结束->当每个小技能行为块也全都结束后,就回收当前的小技能行为块和大技能块
    /// </summary>
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
            Debug.Log("添加技能块");
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
                Debug.Log("移除技能块");
            }
        }
    }
}