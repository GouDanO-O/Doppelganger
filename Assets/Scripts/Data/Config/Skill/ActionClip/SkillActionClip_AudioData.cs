using System;
using GameFrame.World;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    [Serializable]
    public class SkillActionClip_AudioData : SkillActionClip_BasicData
    {
        [LabelText("音频Clip")]
        public AudioClip AudioClip;

        [LabelText("是否是全局播放")]
        public bool IsGobal;
        
        [LabelText("音量")]
        public float Volume;
        
        [LabelText("播放次数(如果为-1,就代表循环播放)")]
        public int PlayCount = 1;
        
        public override void InitExecution()
        {
            
        }

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