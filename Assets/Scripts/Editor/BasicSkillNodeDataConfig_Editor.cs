// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using GameFrame.Config;
// using UnityEditor;
// using UnityEditorInternal;
// using System.Collections.Generic;
//
// namespace GameFrame.Editors
// {
//     [CustomEditor(typeof(BasicSkillNodeDataConfig))]
//     public class BasicSkillNodeDataConfig_Editor : Editor
//     {
//         private ReorderableList prerequisiteSkillList;
//         
//         private bool foldout = false; // 控制列表折叠的开关
//
//         private void OnEnable()
//         {
//             // 获取目标对象
//             BasicSkillNodeDataConfig config = (BasicSkillNodeDataConfig)target;
//
//             // 创建 ReorderableList
//             prerequisiteSkillList = new ReorderableList(serializedObject,
//                 serializedObject.FindProperty("PrerequisiteSkills"),
//                 true, true, true, true);
//
//             // 设置列表标题
//             prerequisiteSkillList.drawHeaderCallback = (Rect rect) => {
//                 EditorGUI.LabelField(rect, "前置技能列表");
//             };
//
//             // 绘制每个列表项
//             prerequisiteSkillList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
//                 SerializedProperty element = prerequisiteSkillList.serializedProperty.GetArrayElementAtIndex(index);
//                 rect.y += 2;
//                 EditorGUI.PropertyField(
//                     new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
//                     element, GUIContent.none);
//             };
//
//             // 添加按钮回调
//             prerequisiteSkillList.onAddCallback = (ReorderableList list) => {
//                 config.PrerequisiteSkills.Add(null);
//             };
//
//             // 删除按钮回调
//             prerequisiteSkillList.onRemoveCallback = (ReorderableList list) => {
//                 if (EditorUtility.DisplayDialog("警告", "你确定要删除这个前置技能吗？", "是", "否"))
//                 {
//                     ReorderableList.defaultBehaviours.DoRemoveButton(list);
//                 }
//             };
//         }
//         
//         
//         public override void OnInspectorGUI()
//         {
//             serializedObject.Update();
//             // 获取目标对象
//             BasicSkillNodeDataConfig config = (BasicSkillNodeDataConfig)target;
//             
//             // 显示技能ID
//             config.SkillID = EditorGUILayout.IntField("技能ID", config.SkillID);
//             
//             // 显示技能名称
//             config.SkillName = EditorGUILayout.TextField("技能名称", config.SkillName);
//             
//             // 显示技能描述
//             config.SkillDescription = EditorGUILayout.TextField("技能描述", config.SkillDescription);
//             
//             // 显示技能所需等级
//             config.RequiredLevel = EditorGUILayout.IntField("所需等级", config.RequiredLevel);
//             
//             // 显示解锁该技能所需的技能点数
//             config.SkillPointsCost = EditorGUILayout.IntField("所需技能点数", config.SkillPointsCost);
//             
//             foldout = EditorGUILayout.Foldout(foldout, "前置技能列表");
//             if (foldout)
//             {
//                 prerequisiteSkillList.DoLayoutList();
//             }
//             config.CompositeSkillBehaviorConfig = (CompositeSkillBehaviorConfig)EditorGUILayout.ObjectField("技能行为配置", config.CompositeSkillBehaviorConfig, typeof(CompositeSkillBehaviorConfig), false);
//             if (GUI.changed)
//             {
//                 EditorUtility.SetDirty(config);
//             }
//             serializedObject.ApplyModifiedProperties();
//         }
//     }
// }