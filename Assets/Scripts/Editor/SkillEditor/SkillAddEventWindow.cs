using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using GameFrame.Config;
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace GameFrame.Editor
{
    public class SkillAddEventWindow : OdinEditorWindow
    {
        private SkillNodeDataConfig skill;
        private SkillEditorWindow parentWindow;

        [BoxGroup("添加事件")]
        [LabelText("选择轨道")]
        public SkillTrackConfig SelectedTrack;

        [BoxGroup("添加事件")]
        [LabelText("事件类型")]
        public EActionType SelectedEventType;

        [BoxGroup("添加事件")]
        [LabelText("事件时间")]
        public float EventTime = 0f;

        private List<SkillTrackConfig> tracks;

        [MenuItem("Tools/Skill Add Event")]
        public static void OpenWindow(SkillNodeDataConfig skill, SkillEditorWindow parent)
        {
            var window = GetWindow<SkillAddEventWindow>();
            window.skill = skill;
            window.parentWindow = parent;
            window.titleContent = new GUIContent("添加技能事件");
            window.Show();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (skill != null)
            {
                tracks = skill.SkillTracks;
                if (tracks.Count > 0)
                {
                    SelectedTrack = tracks[0];
                }
            }
        }

        protected override void OnGUI()
        {
            SirenixEditorGUI.BeginBox();
            SirenixEditorGUI.Title("添加技能事件", null, TextAlignment.Left, true);
            GUILayout.Space(5);

            // 选择轨道
            SelectedTrack = 
                (SkillTrackConfig)SirenixEditorFields.UnityObjectField(SelectedTrack, typeof(SkillTrackConfig), false);

            // 选择事件类型
            SelectedEventType = (EActionType)SirenixEditorFields.EnumDropdown("事件类型", SelectedEventType);

            // 输入事件时间
            EventTime = SirenixEditorFields.FloatField("事件时间", EventTime);

            GUILayout.Space(10);
            if (GUILayout.Button("添加"))
            {
                AddEvent();
            }

            SirenixEditorGUI.EndBox();
        }

        private void AddEvent()
        {
            if (SelectedTrack != null)
            {
                SkillActionClip newEvent = new SkillActionClip
                {
                    Time = EventTime,
                    ActionType = SelectedEventType,
                    ActionDes = GetDefaultActionDescription(SelectedEventType),
                    Parameters = CreateDefaultParameters(SelectedEventType)
                };
                SelectedTrack.ActionClips.Add(newEvent);
                EditorUtility.SetDirty(skill);
                parentWindow.Repaint();
                this.Close();
            }
            else
            {
                EditorUtility.DisplayDialog("错误", "请选择一个轨道。", "确定");
            }
        }

        private string GetDefaultActionDescription(EActionType type)
        {
            switch (type)
            {
                case EActionType.DetailAction:
                    return "执行具体行为";
                case EActionType.Animation:
                    return "播放动画";
                case EActionType.Audio:
                    return "播放音效";
                case EActionType.ParticleSystem:
                    return "播放粒子特效";
                default:
                    return "未知行为";
            }
        }

        private SkillActionClip_BasicData CreateDefaultParameters(EActionType type)
        {
            switch (type)
            {
                case EActionType.DetailAction:
                    return new SkillActionClip_DetailAction_Basic { /* 初始化具体行为参数 */ };
                case EActionType.Animation:
                    return new SkillActionClip_AnimationData {  };
                case EActionType.Audio:
                    return new SkillActionClip_AudioData { AudioClip = null, Volume = 1f };
                case EActionType.ParticleSystem:
                    return new SkillActionClip_ParticleEffectData { ParticleEffectPrefab = null};
                default:
                    return null;
            }
        }
    }
}
