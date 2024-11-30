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

        [ShowIf("@ActionType ==EActionType.DetailAction"), LabelText("具体行为")]
        public SkillActionClip_DetailAction_Basic DetailAction;
        
        [ShowIf("@ActionType ==EActionType.Animation"), LabelText("动画")]
        public SkillActionClip_AnimationData AnimationData;
        
        [ShowIf("@ActionType ==EActionType.Audio"), LabelText("音效")]
        public SkillActionClip_AudioData AudioData;
        
        [ShowIf("@ActionType ==EActionType.ParticleSystem"), LabelText("粒子特效")]
        public SkillActionClip_ParticleEffectData ParticleEffectData;

        public void TriggerSkillAction(WorldObj owner =null,WorldObj target=null)
        {
            switch (ActionType)
            {
                case EActionType.DetailAction:
                    DetailAction?.ExecuteCheck(owner); // 检查并执行具体行为
                    break;
                case EActionType.Animation:
                    
                    break;
                case EActionType.Audio:
                    
                    break;
                case EActionType.ParticleSystem:

                    break;
            }
        }
    }
}
