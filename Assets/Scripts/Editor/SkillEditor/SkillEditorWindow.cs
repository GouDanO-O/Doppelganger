using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using GameFrame.Config;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace GameFrame.Editor
{
    public class SkillEditorWindow : OdinEditorWindow
    {
        // 添加菜单项以便通过Unity菜单打开窗口
        [MenuItem("Tools/Skill Editor")]
        private static void OpenWindow()
        {
            GetWindow<SkillEditorWindow>("技能编辑器").Show();
        }

        [BoxGroup("编辑技能")]
        [LabelText("当前技能")]
        [AssetList(Path = "Assets/Skills")] // 可选：过滤到特定文件夹
        public SkillNodeDataConfig CurrentSkill;

        // 用于绘制时间轴的参数
        private Vector2 scrollPosition;
        private float zoomLevel = 1f; // 缩放级别
        private SkillActionClip copiedActionClip = null; // 用于复制粘贴

        protected override void OnGUI()
        {
            SirenixEditorGUI.Title("技能编辑器", null, TextAlignment.Left, true);
            GUILayout.Space(10);

            // 选择技能
            CurrentSkill = (SkillNodeDataConfig)SirenixEditorFields.UnityObjectField(
                "选择技能", 
                CurrentSkill, 
                typeof(SkillNodeDataConfig), 
                false
            );

            if (CurrentSkill != null)
            {
                GUILayout.Space(10);
                DrawTimeline();
            }
            else
            {
                SirenixEditorGUI.InfoMessageBox("请先选择一个技能对象进行编辑。");
            }
        }

        private void DrawTimeline()
        {
            SirenixEditorGUI.BeginBox();
            SirenixEditorGUI.Title("时间轴", null, TextAlignment.Left, true);
            GUILayout.Space(5);

            // 添加滚动视图
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(250));
            // 定义时间轴的宽度，基于技能CD和缩放级别
            float timelineWidth = CurrentSkill.SkillCooldown * 100f * zoomLevel;
            Rect timelineRect = GUILayoutUtility.GetRect(timelineWidth, 200, GUILayout.ExpandWidth(false));
            DrawTimelineArea(timelineRect);
            GUILayout.EndScrollView();

            // 添加事件按钮
            GUILayout.Space(10);
            if (GUILayout.Button("添加事件"))
            {
                AddNewEvent();
            }

            // 添加技能预览按钮
            GUILayout.Space(10);
            if (GUILayout.Button("预览技能"))
            {
                PreviewSkill();
            }

            SirenixEditorGUI.EndBox();
        }

        private void AddNewEvent()
        {
            // 默认添加到第一个轨道，或者提示用户选择轨道
            if (CurrentSkill.SkillTracks.Count == 0)
            {
                EditorUtility.DisplayDialog("提示", "当前技能没有轨道，请先添加轨道。", "确定");
                return;
            }

            // 弹出添加事件窗口，让用户选择轨道和事件类型
            SkillAddEventWindow.OpenWindow(CurrentSkill, this);
        }

        private void DrawTimelineArea(Rect rect)
        {
            // 开始绘制时间轴
            Handles.BeginGUI();

            // 绘制背景
            EditorGUI.DrawRect(rect, new Color(0.2f, 0.2f, 0.2f));

            // 处理鼠标滚轮缩放
            Event evt = Event.current;
            if (rect.Contains(evt.mousePosition))
            {
                if (evt.type == EventType.ScrollWheel)
                {
                    zoomLevel -= evt.delta.y * 0.1f;
                    zoomLevel = Mathf.Clamp(zoomLevel, 0.5f, 2f);
                    evt.Use();
                }
            }

            // 绘制分割线和刻度
            int divisions = Mathf.CeilToInt(10 * zoomLevel);
            float step = rect.width / divisions;
            for (int i = 0; i <= divisions; i++)
            {
                float x = rect.x + i * step;
                EditorGUI.DrawRect(new Rect(x, rect.y, 1, rect.height), Color.gray);
                GUI.Label(new Rect(x, rect.y + rect.height - 20, 50, 20), $"{(CurrentSkill.SkillCooldown / divisions) * i:F1}s");
            }

            // 绘制轨道和事件节点
            float trackHeight = 30f;
            float trackSpacing = 10f;
            for (int i = 0; i < CurrentSkill.SkillTracks.Count; i++)
            {
                SkillTrackConfig track = CurrentSkill.SkillTracks[i];
                float y = rect.y + i * (trackHeight + trackSpacing);

                // 绘制轨道背景
                EditorGUI.DrawRect(new Rect(rect.x, y, rect.width, trackHeight), new Color(0.3f, 0.3f, 0.3f));

                // 绘制轨道名称
                GUI.Label(new Rect(rect.x + 5, y + 5, 100, 20), track.TrackName);

                // 绘制事件节点
                foreach (var skillAction in track.ActionClips)
                {
                    DrawSkillActionClip(skillAction, rect, i, trackHeight);
                }
            }

            Handles.EndGUI();
        }

        private void DrawSkillActionClip(SkillActionClip skillAction, Rect timelineRect, int trackIndex, float trackHeight)
        {
            float pixelsPerSecond = timelineRect.width / CurrentSkill.SkillCooldown;
            float x = timelineRect.x + skillAction.Time * pixelsPerSecond;

            float nodeWidth = 10f;
            float nodeHeight = 20f;
            float y = timelineRect.y + trackIndex * (trackHeight + 10f) + (trackHeight - nodeHeight) / 2;

            Rect nodeRect = new Rect(x - nodeWidth / 2, y, nodeWidth, nodeHeight);

            // 选择颜色根据事件类型
            Color nodeColor = GetColorByActionType(skillAction.ActionType);
            EditorGUI.DrawRect(nodeRect, nodeColor);

            // 绘制边框
            GUI.Box(nodeRect, GUIContent.none, GUI.skin.box);

            // 显示事件类型缩写
            GUI.Label(nodeRect, skillAction.ActionType.ToString().Substring(0, 1));

            // 处理鼠标事件
            HandleSkillActionClipInteractions(skillAction, nodeRect, trackIndex, timelineRect);
        }

        private Color GetColorByActionType(EActionType actionType)
        {
            switch (actionType)
            {
                case EActionType.DetailAction:
                    return Color.blue;
                case EActionType.Animation:
                    return Color.green;
                case EActionType.Audio:
                    return Color.yellow;
                case EActionType.ParticleSystem:
                    return Color.red;
                default:
                    return Color.white;
            }
        }

        private void HandleSkillActionClipInteractions(SkillActionClip skillAction, Rect nodeRect, int trackIndex, Rect timelineRect)
        {
            Event evt = Event.current;
            if (nodeRect.Contains(evt.mousePosition))
            {
                if (evt.type == EventType.ContextClick)
                {
                    GenericMenu menu = new GenericMenu();
                    menu.AddItem(new GUIContent("编辑事件"), false, () => EditSkillActionClip(skillAction));
                    menu.AddItem(new GUIContent("删除事件"), false, () => DeleteSkillActionClip(skillAction));
                    menu.AddItem(new GUIContent("复制事件"), false, () => CopySkillActionClip(skillAction));
                    menu.AddItem(new GUIContent("粘贴事件"), false, () => PasteSkillActionClip(trackIndex));
                    menu.ShowAsContext();
                    evt.Use();
                }
                else if (evt.type == EventType.MouseDown && evt.button == 0)
                {
                    if (evt.clickCount == 2)
                    {
                        // 双击编辑事件
                        EditSkillActionClip(skillAction);
                        evt.Use();
                    }
                    else
                    {
                        // 开始拖动
                        StartDraggingSkillActionClip(skillAction, nodeRect);
                        evt.Use();
                    }
                }
            }

            // 处理拖动
            if (isDragging && draggedSkillAction != null)
            {
                if (evt.type == EventType.MouseDrag && evt.button == 0)
                {
                    float newX = evt.mousePosition.x - dragOffsetX;
                    float timelineWidth = timelineRect.width;
                    float newTime = (newX - timelineRect.x) / (timelineWidth / CurrentSkill.SkillCooldown);
                    newTime = Mathf.Clamp(newTime, 0f, CurrentSkill.SkillCooldown);
                    draggedSkillAction.Time = newTime;
                    EditorUtility.SetDirty(CurrentSkill);
                    Repaint();
                }
                else if (evt.type == EventType.MouseUp && evt.button == 0)
                {
                    isDragging = false;
                    draggedSkillAction = null;
                    EditorGUIUtility.hotControl = 0;
                    evt.Use();
                }
            }
        }

        private bool isDragging = false;
        private SkillActionClip draggedSkillAction = null;
        private float dragOffsetX = 0f;

        private void StartDraggingSkillActionClip(SkillActionClip skillAction, Rect nodeRect)
        {
            isDragging = true;
            draggedSkillAction = skillAction;
            dragOffsetX = Event.current.mousePosition.x - nodeRect.x;
            EditorGUIUtility.hotControl = GUIUtility.GetControlID(FocusType.Passive);
        }

        private void EditSkillActionClip(SkillActionClip skillAction)
        {
            SkillActionEditorWindow.OpenWindow(skillAction, this);
        }

        private void DeleteSkillActionClip(SkillActionClip skillAction)
        {
            foreach (var track in CurrentSkill.SkillTracks)
            {
                if (track.ActionClips.Contains(skillAction))
                {
                    track.ActionClips.Remove(skillAction);
                    EditorUtility.SetDirty(CurrentSkill);
                    Repaint();
                    break;
                }
            }
        }

        private void CopySkillActionClip(SkillActionClip skillAction)
        {
            // 深拷贝事件
            copiedActionClip = JsonUtility.FromJson<SkillActionClip>(JsonUtility.ToJson(skillAction));
        }

        private void PasteSkillActionClip(int trackIndex)
        {
            if (copiedActionClip == null)
            {
                EditorUtility.DisplayDialog("提示", "没有复制的事件可以粘贴。", "确定");
                return;
            }

            SkillActionClip newActionClip = JsonUtility.FromJson<SkillActionClip>(JsonUtility.ToJson(copiedActionClip));
            CurrentSkill.SkillTracks[trackIndex].ActionClips.Add(newActionClip);
            EditorUtility.SetDirty(CurrentSkill);
            Repaint();
        }

        private GameObject previewObject;

        private void PreviewSkill()
        {
            if (previewObject != null)
            {
                DestroyImmediate(previewObject);
            }

            // 创建一个简单的预览对象
            previewObject = new GameObject("SkillPreview");
            // 根据需要添加组件，例如动画控制器、音效播放器等
            // 这里只是一个示例，添加一个SpriteRenderer显示预览
            SpriteRenderer renderer = previewObject.AddComponent<SpriteRenderer>();
            renderer.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/YourSpritePath.png"); // 替换为你的Sprite路径

            // 触发技能事件
            CurrentSkill.TriggerSkill(owner: null, target: null);
        }
    }
}
