using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine.Events;

namespace GameFrame.World
{
    public class SkillExecuteManager : MonoNetSingleton<SkillExecuteManager>
    {
        public List<SkillExecuter_TemporalityPoolable>
            skillExecuterList = new List<SkillExecuter_TemporalityPoolable>();
        
        public UnityAction<OwnedSkillModel> onAddSkillExecuter;
        
        public UnityAction<OwnedSkillModel> onRemoveSkillExecuter;

        public void InitData()
        {
            onAddSkillExecuter += AddSkillExecuter;
            onRemoveSkillExecuter += RemoveSkillExecuter;
        }

        /// <summary>
        /// 轮循里面的技能执行块
        /// </summary>
        public void CheckSkillExecution()
        {
            if (skillExecuterList.Count > 0)
            {
                for (int i = 0; i < skillExecuterList.Count; i++)
                {
                    SkillExecuter_TemporalityPoolable skillExecuter = skillExecuterList[i];
                    skillExecuter.TimeCheck();
                }
            }
        }
        
        /// <summary>
        /// 添加技能
        /// </summary>
        /// <param name="skillModel"></param>
        private void AddSkillExecuter(OwnedSkillModel skillModel)
        {
            SkillExecuter_TemporalityPoolable skillExecuter = SkillExecuter_TemporalityPoolable.Allocate();
            skillExecuter.InitData(skillModel);
            skillExecuterList.Add(skillExecuter);
        }

        /// <summary>
        /// 移除技能数据
        /// 当技能执行完毕后就移除这个技能
        /// </summary>
        /// <param name="skillModel"></param>
        private void RemoveSkillExecuter(OwnedSkillModel skillModel)
        {
            
        }
    }
}