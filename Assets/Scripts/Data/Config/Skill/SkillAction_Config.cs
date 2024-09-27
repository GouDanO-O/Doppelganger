using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    public class SkillAction_Config : SerializedScriptableObject
    {
        [LabelText("时间轴列表")]
        public List<SkillActionClip_Config> clipTimeLineList=new List<SkillActionClip_Config>();
    }
}

