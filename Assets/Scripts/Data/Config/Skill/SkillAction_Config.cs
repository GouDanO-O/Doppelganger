using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Sirenix.OdinInspector;
using UnityEngine;
using Newtonsoft.Json;

namespace GameFrame.Config
{
    
    /// <summary>
    /// 这个行为定义了一个或多个时间轴,每个时间轴又会由多个不同时间戳的小行为组成
    /// </summary>
    public class SkillAction_Config : SerializedScriptableObject
    {
        [LabelText("行为ID(不可重复)")]
        public int ActionId;
        
        [LabelText("行为名称")]
        public string ActionName;
        
        [LabelText("时间轴列表")]
        public List<SkillActionClip_Config> clipTimeLineList=new List<SkillActionClip_Config>(); 
        
        [LabelText("目标传入类型")]
        public EAction_TargetInputType ActionInputType;
        
        [DelayedProperty,JsonIgnore,AssetsOnly]
        public GameObject ObjectAsset;
        
        [LabelText("总时间")]
        public double TotalTime;
    }
}

