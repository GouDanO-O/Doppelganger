using System.Collections.Generic;
using GameFrame.Config;
using QFramework;

namespace GameFrame
{
    /// <summary>
    /// 技能执行器
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