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

        protected float GetLongestTime()
        {
            float longestTime = 0;
            float tempTime = 0;
            for (int i = 0; i < ActionClips.Count; i++)
            {
                longestTime = ActionClips[i].EndTime;
                if (longestTime >= tempTime)
                {
                    tempTime = longestTime;
                }
            }

            return tempTime;
        }
        
        /// <summary>
        /// 执行当前时间轨道轴里面的行为
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="target"></param>
        public void Trigger(WorldObj owner=null)
        {
            for (int i = 0; i < ActionClips.Count; i++)
            {
                ActionClips[i].InitExecution(owner);
            }
        }
    }
}