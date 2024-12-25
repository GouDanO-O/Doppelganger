using System.Collections.Generic;
using GameFrame.Config;
using QFramework;

namespace GameFrame.World
{
    /// <summary>
    /// 技能执行器
    /// 当前逻辑如下:
    /// 初始化时,轮循当前技能的所有轴中的行为,并且记录下每个的具体行为和其生命周期
    /// 当技能管理器在轮循时间时,只要时间符合行为的生命周期时间,那么就要执行这个技能
    /// 
    /// </summary>
    public class SkillExecuter_TemporalityPoolable : TemporalityData_Pool
    {
        public SkillExecuteManager skillExecuteManager;
        
        private OwnedSkillData_TemporalityPoolable _skillDataTemporalityPoolable;

        private List<SkillExcuterData_TemporalityPoolable> curWillExecuteActions = new List<SkillExcuterData_TemporalityPoolable>();
        
        private float curExecuteTime = 0;

        public static SkillExecuter_TemporalityPoolable Allocate()
        {
            return SafeObjectPool<SkillExecuter_TemporalityPoolable>.Instance.Allocate();
        }
        
        public void InitData(SkillExecuteManager skillExecuteManager,OwnedSkillData_TemporalityPoolable skillDataTemporalityPoolable)
        {
            this._skillDataTemporalityPoolable = skillDataTemporalityPoolable;
            this.skillExecuteManager = skillExecuteManager;
            ExtuateSkillCheck();
        }

        /// <summary>
        /// 执行技能检测
        /// 将每条轴里面的行为添加到list中
        /// </summary>
        private void ExtuateSkillCheck()
        {
            if (_skillDataTemporalityPoolable.skillNodeDataConfig)
            {
                List<SkillTrack_Config> skillTracks=_skillDataTemporalityPoolable.skillNodeDataConfig.SkillTracks;
                for (int i = 0; i < skillTracks.Count; i++)
                {
                    SkillTrack_Config detailSkillTrackConfig = skillTracks[i];
                    for (int j = 0; j < detailSkillTrackConfig.ActionClips.Count; j++)
                    {
                        AddAction(detailSkillTrackConfig.ActionClips[j]);
                    }
                }
            }
        }

        /// <summary>
        /// 添加技能行为
        /// </summary>
        /// <param name="actionClip"></param>
        private void AddAction(SkillActionClip actionClip)
        {
            SkillExcuterData_TemporalityPoolable skillData = SkillExcuterData_TemporalityPoolable.Allocate();
            skillData.InitData(actionClip);
            curWillExecuteActions.Add(skillData);
        }

        /// <summary>
        /// 由技能管理器去轮循这个技能的时间
        /// </summary>
        public void TimeCheck(float deltaTime)
        {
            curExecuteTime += deltaTime;
            int curEndCount = 0;
            int curSkillCount = curWillExecuteActions.Count;
            for (int i = 0; i < curSkillCount; i++)
            {
                SkillExcuterData_TemporalityPoolable curData=curWillExecuteActions[i];
                if (curData.CheckTime(curExecuteTime))
                {
                    curData.StartExecute();
                    curEndCount++;
                }
            }

            if (curEndCount == curSkillCount)
            {
                Recycle2Cache();
            }
        }
        
        public override void DeInitData()
        {
            
        }

        public override void OnRecycled()
        {
            
        }

        public override void Recycle2Cache()
        {
            for (int i = 0; i < curWillExecuteActions.Count; i++)
            {
                curWillExecuteActions[i].Recycle2Cache();
            }
            curWillExecuteActions.Clear();
            skillExecuteManager.RemoveSkillExecuter(this);
        }
    }
}