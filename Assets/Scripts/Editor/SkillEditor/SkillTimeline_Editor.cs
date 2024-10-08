using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Linq;
using Sirenix.Utilities.Editor;

namespace GameFrame.Config
{
    public class SkillTimeline_Editor : OdinEditorWindow
    {
        [MenuItem("Tools/Skill Timeline Editor")]
        private static void OpenWindow()
        {
            GetWindow<SkillTimeline_Editor>("Skill Timeline Editor").Show();
        }

        [Title("技能配置", bold: true)]
        [InlineEditor(Expanded = true, ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        [HideLabel]
        public SkillNodeDataConfig CurrentSkill;

        private Vector2 propertyScroll;
        private Vector2 timelineScroll;

        protected override void OnGUI()
        {
            // 检查是否有选中的技能配置
            if (CurrentSkill == null)
            {
                EditorGUILayout.HelpBox("请选择一个技能配置进行编辑。", MessageType.Info);
                CurrentSkill = EditorGUILayout.ObjectField("技能配置", CurrentSkill, typeof(SkillNodeDataConfig), false) as SkillNodeDataConfig;
                return;
            }

            // 划分编辑器窗口为上下两部分
            float windowHeight = position.height;
            float propertyHeight = windowHeight * 0.4f;
            float timelineHeight = windowHeight - propertyHeight;

            // 绘制技能属性部分
            propertyScroll = GUILayout.BeginScrollView(propertyScroll, GUILayout.Height(propertyHeight));
            DrawSkillProperties();
            GUILayout.EndScrollView();

            // 绘制分割线
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

            // 绘制 Timeline 部分
            timelineScroll = GUILayout.BeginScrollView(timelineScroll, GUILayout.Height(timelineHeight));
            DrawTimelineEditor();
            GUILayout.EndScrollView();

            // 处理事件
            HandleEvents();
        }
        
        private void DrawSkillProperties()
        {
            // 使用 Odin 的 Inspector 绘制技能的基础属性
            //this.DrawEditor(CurrentSkill);
        }

        private void DrawTimelineEditor()
        {
            // 添加标题
            GUILayout.Label("技能 Timeline 编辑", EditorStyles.boldLabel);

            // 调用您的 Timeline 绘制方法
            DrawTimeline();
        }
        
        private float timelineStartX = 200f; // 时间轴起始位置
        private float timelineWidth;
        private float timelineHeight;
        private float timeScale = 100f; // 每秒对应的像素数
        private float totalDuration = 10f; // 技能的总持续时间

        private Vector2 scrollPosition; // 用于滚动视图

        private void DrawTimeRuler(Rect timelineRect)
        {
            Handles.BeginGUI();
            float rulerY = timelineRect.y - 20;
            for (float t = 0; t <= totalDuration; t += 0.5f)
            {
                float x = timelineRect.x + (t / totalDuration) * timelineRect.width;
                Handles.color = Color.white;
                Handles.DrawLine(new Vector3(x, rulerY), new Vector3(x, rulerY + timelineRect.height + 20));

                GUI.Label(new Rect(x - 10, rulerY - 15, 40, 20), t.ToString("F1"));
            }
            Handles.EndGUI();
        }

        private void DrawTimeline()
        {
            timelineWidth = position.width - timelineStartX - 20;
            timelineHeight = CurrentSkill.SkillTracks.Count * 60f + 20f;

            scrollPosition = GUI.BeginScrollView(
                new Rect(0, 100, position.width, position.height - 100),
                scrollPosition,
                new Rect(0, 0, position.width, timelineHeight + 100));

            try
            {
                Rect timelineRect = new Rect(timelineStartX, 0, timelineWidth, timelineHeight);
                EditorGUI.DrawRect(timelineRect, Color.grey * 0.2f);

                DrawTimeRuler(timelineRect);

                float trackY = 20f;
                foreach (var track in CurrentSkill.SkillTracks)
                {
                    DrawTrack(track, trackY, timelineRect);
                    trackY += 60f;
                }
            }
            finally
            {
                GUI.EndScrollView();
            }
        }

        
        private void DrawTrack(SkillTrack track, float yPosition, Rect timelineRect)
        {
            if (track == null)
            {
                Debug.LogError("track is null");
                return;
            }

            if (timelineRect == null)
            {
                Debug.LogError("timelineRect is null");
                return;
            }

            if (track.ActionClips == null)
            {
                Debug.LogError("track.ActionClips is null");
                return;
            }
            
            Rect trackRect = new Rect(timelineStartX, yPosition, timelineWidth, 50f);
            EditorGUI.DrawRect(trackRect, Color.black * 0.5f);

            Rect trackNameRect = new Rect(10, yPosition, timelineStartX - 20, 50f);
            EditorGUI.LabelField(trackNameRect, track.TrackName);

            foreach (var clip in track.ActionClips)
            {
                DrawActionClip(clip, yPosition, timelineRect);
            }

            if (GUI.Button(new Rect(trackNameRect.xMax - 60, yPosition + 15, 50, 20), "添加"))
            {
                AddNewActionClip(track);
            }
        }

        private void DrawActionClip(SkillActionClip_Config clip, float yPosition, Rect timelineRect)
        {
            if (clip == null)
            {
                Debug.Log("clip is null");
                return;
            }
            
            if (timelineRect == null)
            {
                Debug.LogError("timelineRect is null");
                return;
            }
            
            float clipStartX = timelineRect.x + (clip.StartTime / totalDuration) * timelineRect.width;
            float clipWidth = (clip.Duration / totalDuration) * timelineRect.width;

            Rect clipRect = new Rect(clipStartX, yPosition, clipWidth, 50f);
            EditorGUI.DrawRect(clipRect, Color.green * 0.8f);

            GUI.Label(new Rect(clipRect.x + 5, clipRect.y + 5, clipRect.width - 10, 20), clip.GetType().Name);

            HandleClipEvents(clip, clipRect);
        }

        private bool isDraggingClip = false;
        private SkillActionClip_Config selectedClip;
        private SkillTrack selectedTrack;
        private Vector2 dragStartPos;
        private float clipStartTimeAtDrag;

        private void HandleClipEvents(SkillActionClip_Config clip, Rect clipRect)
        {
            Event e = Event.current;

            if (clipRect.Contains(e.mousePosition))
            {
                if (e.type == EventType.MouseDown && e.button == 0)
                {
                    isDraggingClip = true;
                    selectedClip = clip;
                    selectedTrack = GetTrackOfClip(clip);
                    dragStartPos = e.mousePosition;
                    clipStartTimeAtDrag = clip.StartTime;
                    e.Use();
                }
                else if (e.type == EventType.MouseDown && e.button == 1)
                {
                    ShowClipContextMenu(clip);
                    e.Use();
                }
            }
        }

        private SkillTrack GetTrackOfClip(SkillActionClip_Config clip)
        {
            return CurrentSkill.SkillTracks.FirstOrDefault(t => t.ActionClips.Contains(clip));
        }

        private void HandleEvents()
        {
            Event e = Event.current;

            if (isDraggingClip && selectedClip != null)
            {
                if (e.type == EventType.MouseDrag)
                {
                    Vector2 delta = e.mousePosition - dragStartPos;
                    float deltaTime = (delta.x / timelineWidth) * totalDuration;
                    selectedClip.StartTime = Mathf.Clamp(clipStartTimeAtDrag + deltaTime, 0, totalDuration - selectedClip.Duration);
                    EditorUtility.SetDirty(CurrentSkill);
                    Repaint();
                    e.Use();
                }
                else if (e.type == EventType.MouseUp)
                {
                    isDraggingClip = false;
                    selectedClip = null;
                    selectedTrack = null;
                    e.Use();
                }
            }
        }

        private void AddNewActionClip(SkillTrack track)
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
                    track.ActionClips.Add(newClip);
                    EditorUtility.SetDirty(CurrentSkill);
                    Repaint();
                }
            };
            selector.ShowInPopup();
        }

        private void ShowClipContextMenu(SkillActionClip_Config clip)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("编辑"), false, () => EditActionClip(clip));
            menu.AddItem(new GUIContent("删除"), false, () => DeleteActionClip(clip));
            menu.ShowAsContext();
        }

        private void EditActionClip(SkillActionClip_Config clip)
        {
            SkillActionClipEditorWindow.OpenWindow(clip);
        }

        private void DeleteActionClip(SkillActionClip_Config clip)
        {
            var track = GetTrackOfClip(clip);
            if (track != null)
            {
                track.ActionClips.Remove(clip);
                EditorUtility.SetDirty(CurrentSkill);
                Repaint();
            }
        }

        public class SkillActionClipEditorWindow : OdinEditorWindow
        {
            private SkillActionClip_Config currentClip;

            public static void OpenWindow(SkillActionClip_Config clip)
            {
                var window = GetWindow<SkillActionClipEditorWindow>("编辑行为片段");
                window.currentClip = clip;
            }

            [InlineEditor(Expanded = true)]
            [HideLabel]
            public SkillActionClip_Config Clip
            {
                get { return currentClip; }
                set { currentClip = value; }
            }
        }

        
    }
}

