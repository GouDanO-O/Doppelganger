using System;
using GameFrame.World;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    [Serializable]
    public class SkillActionClip_ParticleEffectData : SkillActionClip_BasicData
    {
        [LabelText("特效预制体")]
        public GameObject ParticleEffectPrefab;

        protected override void StartExecute()
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