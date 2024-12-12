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
    public class SkillActionClip
    {
        [LabelText("行为类型")]
        public EActionType ActionType;

        [LabelText("行为描述")]
        public string ActionDes;

        [LabelText("行为时间")]
        public float Time;
        
        [BoxGroup("行为参数")]
        [SerializeReference]
        public SkillActionClip_BasicData Parameters;

        /// <summary>
        /// 触发具体的行为逻辑
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="target"></param>
        public void TriggerSkillAction(WorldObj owner = null, WorldObj target = null)
        {
            switch (ActionType)
            {
                case EActionType.DetailAction:
                    if (Parameters is SkillActionClip_DetailAction_Basic detailAction)
                    {
                        detailAction.ExecuteCheck(owner);
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
