using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Sirenix.OdinInspector.Editor;

namespace GameFrame.Config
{
    [Serializable]
    public class SkillTrack
    {
        [HorizontalGroup("TrackHeader")]
        [LabelText("轨道名称"), LabelWidth(60)]
        public string TrackName;

        [HideInInspector]
        public SkillNodeDataConfig SkillConfigInstance;

        [LabelText("行为片段列表")]
        [ListDrawerSettings(CustomAddFunction = "AddNewActionClip", DraggableItems = false)]
        [SerializeReference]
        public List<SkillActionClip_Config> ActionClips = new List<SkillActionClip_Config>();

        public SkillTrack()
        {
        }

        public SkillTrack(SkillNodeDataConfig skillConfig)
        {
            this.SkillConfigInstance = skillConfig;
        }

        private SkillActionClip_Config AddNewActionClip()
        {
            var actionClipTypes = new List<Type>()
            {
                typeof(SActionClip_ParticleEffectData),
                typeof(SActionClip_AnimationData),
                typeof(SActionClip_AudioData)
            };

            var selector = new GenericSelector<Type>("选择行为类型", false, actionClipTypes);
            selector.SelectionConfirmed += (selection) =>
            {
                var selectedType = selection.FirstOrDefault();
                if (selectedType != null)
                {
                    var newClip = (SkillActionClip_Config)Activator.CreateInstance(selectedType);
                    newClip.StartTime = 0f;
                    newClip.Duration = 1f;
                    ActionClips.Add(newClip);
                    EditorUtility.SetDirty(SkillConfigInstance);
                }
            };
            selector.ShowInPopup();
            return null;
        }
    }
}