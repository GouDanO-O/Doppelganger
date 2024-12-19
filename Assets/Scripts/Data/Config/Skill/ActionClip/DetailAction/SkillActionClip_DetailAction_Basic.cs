using System;
using System.Collections;
using GameFrame.World;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    /// <summary>
    /// 基础行为--(具体行为方式在行为中定义)
    /// </summary>
    [Serializable]
    public class SkillActionClip_DetailAction_Basic : SkillActionClip_BasicData
    {
        protected ActionClipData_TemporalityPoolable clipDataTemporality;
        
        public override void InitExecution(WorldObj owner)
        {
            this.ownerObj = owner;
            clipDataTemporality = ActionClipData_TemporalityPoolable.Allocate();
            clipDataTemporality.SetOwner(owner);
            StartExecute();
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