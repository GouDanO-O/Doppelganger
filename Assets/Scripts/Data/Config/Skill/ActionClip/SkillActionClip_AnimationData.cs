using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{

    [Serializable]
    public class SkillActionClip_AnimationData : SkillActionClip_BasicData
    {
        [LabelText("动画Clip")]
        public AnimationClip AnimationClip;
    }
}