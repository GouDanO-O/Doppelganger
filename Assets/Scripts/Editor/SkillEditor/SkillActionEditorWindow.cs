// SkillActionEditorWindow.cs
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using GameFrame.Config;
using Sirenix.OdinInspector;

namespace GameFrame.Editor
{
    public class SkillActionEditorWindow : OdinEditorWindow
    {
        private SkillActionClip skillAction;
        private SkillEditorWindow parentWindow;

        [BoxGroup("事件信息")]
        [LabelText("行为描述")]
        public string ActionDescription;

        [BoxGroup("事件信息")]
        [LabelText("事件时间")]
        public float EventTime;

        [BoxGroup("事件参数")]
        [LabelText("参数")]
        [SerializeReference]
        public SkillActionClip_BasicData Parameters;

        [MenuItem("Tools/Skill Action Editor")]
        public static void OpenWindow(SkillActionClip skillAction, SkillEditorWindow parent)
        {
            var window = GetWindow<SkillActionEditorWindow>();
            window.skillAction = skillAction;
            window.parentWindow = parent;
            window.titleContent = new GUIContent("编辑技能事件");
            window.Show();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (skillAction != null)
            {
                ActionDescription = skillAction.ActionDes;
                EventTime = skillAction.Time;
                Parameters = skillAction.Parameters;
            }
        }

        protected override void OnGUI()
        {
            SirenixEditorGUI.BeginBox();
            SirenixEditorGUI.Title("编辑技能事件", null, TextAlignment.Left, true);
            GUILayout.Space(5);

            // 编辑行为描述
            ActionDescription = SirenixEditorFields.TextField("行为描述", ActionDescription);

            // 编辑事件时间
            EventTime = SirenixEditorFields.FloatField("事件时间", EventTime);

            // 编辑行为类型
            EActionType newActionType = (EActionType)SirenixEditorFields.EnumDropdown("行为类型", skillAction.ActionType);
            if (newActionType != skillAction.ActionType)
            {
                skillAction.ActionType = newActionType;
                Parameters = CreateDefaultParameters(newActionType);
            }

            // 动态编辑参数
            if (Parameters != null)
            {
                SirenixEditorGUI.BeginBox();
                SirenixEditorGUI.Title("参数", null, TextAlignment.Left, true);
                
                Parameters = (SkillActionClip_BasicData)SirenixEditorFields.UnityObjectField(
                    "选择技能", 
                    Parameters, 
                    typeof(SkillActionClip_BasicData), 
                    false
                );
                

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
            if (skillAction != null)
            {
                skillAction.ActionDes = ActionDescription;
                skillAction.Time = EventTime;
                skillAction.Parameters = Parameters;

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
                    return new SkillActionClip_DetailAction_Basic {  };
                case EActionType.Animation:
                    return new SkillActionClip_AnimationData { };
                case EActionType.Audio:
                    return new SkillActionClip_AudioData {  };
                case EActionType.ParticleSystem:
                    return new SkillActionClip_ParticleEffectData {  };
                default:
                    return null;
            }
        }
    }
}
