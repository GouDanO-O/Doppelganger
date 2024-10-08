using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sirenix.OdinInspector;
using UnityEngine;
using Newtonsoft.Json;
using Sirenix.OdinInspector.Editor;

namespace GameFrame.Config
{
    
    /// <summary>
    /// 这个行为定义了一个或多个时间轴,每个时间轴又会由多个不同时间戳的小行为组成
    /// </summary>
    [CreateAssetMenu(fileName = "Action",menuName = "配置/技能/行为")]
    public class SkillAction_Config : SerializedScriptableObject
    {
        [LabelText("行为ID(不可重复)")]
        public int ActionId;
        
        [LabelText("行为名称")]
        public string ActionName;
        
        [LabelText("行为片段列表"), ListDrawerSettings(CustomAddFunction = "CreateNewActionClip", HideRemoveButton = false)]
        public List<SkillActionClip_Config> ActionClips =new List<SkillActionClip_Config>(); 
        
        [LabelText("目标传入类型")]
        public EAction_TargetInputType ActionInputType;
        
        [DelayedProperty,JsonIgnore,AssetsOnly]
        public GameObject ObjectAsset;
        
        [LabelText("总时间")]
        public double TotalTime;
    }
}

