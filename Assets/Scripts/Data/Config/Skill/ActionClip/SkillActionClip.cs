using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameFrame.World;
using GameFrame;
using QFramework;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    [Serializable]
    public class SkillActionClip : IExecuteLogic
    {
        [LabelText("行为类型")]
        public EActionType ActionType;

        [LabelText("行为描述")]
        public string ActionDes;
        
        [HorizontalGroup("Timing")]
        [LabelText("开始时间"), LabelWidth(60), MinValue(0)]
        public float StartTime;

        [HorizontalGroup("Timing")]
        [LabelText("生命周期"), LabelWidth(60)]
        public float Duration = 1f;

        [HorizontalGroup("Timing")]
        [ShowInInspector,LabelText("结束时间"), LabelWidth(60), ReadOnly]
        public float EndTime => StartTime + Duration;
        
        [BoxGroup("行为参数")]
        [SerializeReference]
        public SkillActionClip_BasicData Parameters;

        public WorldObj ownerObj { get; set; }
        
        public void InitExecution(WorldObj owner)
        {
           this.ownerObj = owner;
        }

        public void StartExecute()
        {
            
        }

        public void EndExecute()
        {
            
        }

        public void ResetExecute()
        {
            ownerObj = null;
        }
        
        /// <summary>
        /// 触发具体的行为逻辑
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="target"></param>
        private void TriggerSkillAction(WorldObj target = null)
        {
            switch (ActionType)
            {
                case EActionType.DetailAction:
                    if (Parameters is SkillActionClip_DetailAction_Basic detailAction)
                    {
                        detailAction.InitExecution(ownerObj);
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
    }
}
