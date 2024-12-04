using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using GameFrame.Config;
using Sirenix.OdinInspector;

namespace GameFrame.Editor
{
    public class SkillEventEditorWindow : OdinEditorWindow
    {
        private SkillActionClip skillEvent;
        private SkillEditorWindow parentWindow;

        [BoxGroup("事件信息")]
        [LabelText("事件时间")]
        public float EventTime;

        [BoxGroup("事件信息")]
        [LabelText("事件类型")]
        public EActionType EventType;

        [BoxGroup("事件参数")]
        [LabelText("参数")]
        [InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
        public SkillActionClip_BasicData Parameters;

        [MenuItem("Tools/Skill Event Editor")]
        public static void OpenWindow(SkillActionClip skillEvent, SkillEditorWindow parent)
        {
            var window = GetWindow<SkillEventEditorWindow>();
            window.skillEvent = skillEvent;
            window.parentWindow = parent;
            window.titleContent = new GUIContent("编辑技能事件");
            window.Show();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (skillEvent != null)
            {
                EventTime = skillEvent.Time;
                EventType = skillEvent.ActionType;
                Parameters = skillEvent.Parameters;
            }
        }

        protected override void OnGUI()
        {
            SirenixEditorGUI.BeginBox();
            SirenixEditorGUI.Title("编辑技能事件", null, TextAlignment.Left, true);
            GUILayout.Space(5);

            // 编辑事件类型
            EActionType newType = (EActionType)SirenixEditorFields.EnumDropdown("事件类型", EventType);
            if (newType != EventType)
            {
                EventType = newType;
                Parameters = CreateDefaultParameters(EventType);
            }

            // 编辑事件时间
            EventTime = SirenixEditorFields.FloatField("事件时间", EventTime);

            // 动态编辑参数
            if (Parameters != null)
            {
                SirenixEditorGUI.BeginBox();
                SirenixEditorGUI.Title("参数", null, TextAlignment.Left, true);
                //SirenixEditorFields.ObjectField(ref Parameters, GUIContent.none);
                SirenixEditorGUI.EndBox();
            }

            GUILayout.Space(10);
            if (GUILayout.Button("保存"))
            {
                SaveChanges();
            }

            SirenixEditorGUI.EndBox();
        }

        private void SaveChanges()
        {
            if (skillEvent != null)
            {
                skillEvent.Time = EventTime;
                skillEvent.ActionType = EventType;

                // 如果事件类型发生变化，重新创建参数对象
                if (skillEvent.ActionType != EventType)
                {
                    skillEvent.Parameters = CreateDefaultParameters(EventType);
                }
                else
                {
                    skillEvent.Parameters = Parameters;
                }

                EditorUtility.SetDirty(parentWindow.CurrentSkill);
                parentWindow.Repaint();
                this.Close();
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
                    return new SkillActionClip_ParticleEffectData { ParticleEffectPrefab = null };
                default:
                    return null;
            }
        }
    }
}
