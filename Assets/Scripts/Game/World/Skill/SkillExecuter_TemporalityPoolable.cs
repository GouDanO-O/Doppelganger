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
        private OwnedSkillModel skillModel;

        private List<SkillActionClip> curWillExecuteActions=new List<SkillActionClip>();

        public static SkillExecuter_TemporalityPoolable Allocate()
        {
            return SafeObjectPool<SkillExecuter_TemporalityPoolable>.Instance.Allocate();
        }
        
        public void InitData(OwnedSkillModel skillModel)
        {
            this.skillModel = skillModel;

            ExtuateSkillCheck();
        }

        private void ExtuateSkillCheck()
        {
            if (skillModel.skillNodeDataConfig)
            {
                List<SkillTrackConfig> skillTracks=skillModel.skillNodeDataConfig.SkillTracks;
                for (int i = 0; i < skillTracks.Count; i++)
                {
                    SkillTrackConfig detailSkillTrackConfig = skillTracks[i];
                    for (int j = 0; j < detailSkillTrackConfig.ActionClips.Count; j++)
                    {
                        AddAction(detailSkillTrackConfig.ActionClips[j]);
                    }
                }

                StartExcute();
            }
        }

        /// <summary>
        /// 添加技能行为
        /// </summary>
        /// <param name="actionClip"></param>
        private void AddAction(SkillActionClip actionClip)
        {
            curWillExecuteActions.Add(actionClip);
        }
        
        /// <summary>
        /// 开始执行
        /// </summary>
        private void StartExcute()
        {
            for (int i = 0; i < curWillExecuteActions.Count; i++)
            {
                
            }
        }

        /// <summary>
        /// 由技能管理器去轮循这个技能的时间
        /// </summary>
        public void TimeCheck()
        {
            
        }

        /// <summary>
        /// 行为开始时间检测
        /// 必须要保证每个行为的时间轴都是同步的
        /// </summary>
        private void TimeDelayCheck()
        {
            
        }

        /// <summary>
        /// 技能行为检测
        /// </summary>
        /// <param name="clipData"></param>
        private void ActionTypeCheck(SkillActionClip clipData)
        {
            switch (clipData.ActionType)
            {
                case EActionType.DetailAction:
                    if (clipData.Parameters is SkillActionClip_DetailAction_Basic detailAction)
                    {
                        
                    }
                    break;
                case EActionType.Animation:
                    // 实现动画触发逻辑
                    break;
                case EActionType.Audio:
                    // 实现音效触发逻辑
                    break;
                case EActionType.ParticleSystem:
                    // 实现粒子特效触发逻辑
                    break;
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
            
        }
    }
}