using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace GameFrame.Config
{
    public class SkillActionClip_Config : SerializationConfig
    {
        [LabelText("行为ID(不可重复)")]
        public int ActionId;
        
        [LabelText("行为名称")]
        public string ActionName;

        [LabelText("总时间")]
        public double TotalTime;

        [DelayedProperty,JsonIgnore,AssetsOnly]
        public GameObject ObjectAsset;
        
        public EAction_TargetInputType ActionInputType;
    }
}

