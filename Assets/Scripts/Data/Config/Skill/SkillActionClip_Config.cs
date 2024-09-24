using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace GameFrame.Config
{
    [LabelText("目标传入类型")]
    public enum EActionTargetInputType
    {
        [LabelText("None")]
        None = 0,
        [LabelText("传入目标实体")]
        Target = 1,
        [LabelText("传入目标点")]
        Point = 2,
    }
    
    [LabelText("触发类型"), Flags]
    public enum EActionTriggerType
    {
        [LabelText("无")]
        None = 0,
        [LabelText("初始触发")]
        StartTrigger = 1 << 1,
        [LabelText("碰撞触发单次")]
        CollisionTrigger = 1 << 2,
        [LabelText("结束触发")]
        EndTrigger = 1 << 3,
        [LabelText("碰撞触发多次")]
        CollisionTriggerMultiple = 1 << 4,
    }
    
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
        
        public EActionTargetInputType ActionInputType;
    }
}

