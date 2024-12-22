using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEngine;
using UnityEditor;
using System.Linq;
using GameFrame.World;
using Sirenix.OdinInspector.Editor;

namespace GameFrame.Config
{

    [CreateAssetMenu(fileName = "SkillTrack",menuName = "配置/技能/技能轨道配置")]
    public class SkillTrackConfig : SerializedScriptableObject
    {
        [LabelText("轨道名称"), LabelWidth(60)]
        public string TrackName;
        
        [LabelText("行为片段列表")]
        public List<SkillActionClip> ActionClips =new List<SkillActionClip>();
    }
}