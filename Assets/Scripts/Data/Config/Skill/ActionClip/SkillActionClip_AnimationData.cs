using System;
using GameFrame.World;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{

    [Serializable]
    public class SkillActionClip_AnimationData : SkillActionClip_BasicData
    {
        [LabelText("动画Clip")]
        public AnimationClip AnimationClip;

        public override void InitExecution(WorldObj owner)
        {
            
        }

        public override void StartExecute()
        {
            
        }

        public override void OnTriggerStart()
        {
            
        }

        public override void OnTriggerStart(WorldObj suffer)
        {
            
        }

        public override void OnTriggerEnd()
        {
            
        }

        public override void OnTriggerEnd(WorldObj suffer)
        {
            
        }

        public override void EndExecute()
        {
            
        }

        public override void ResetExecute()
        {
            
        }
    }
}