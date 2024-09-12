using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFrame.Config;
using UnityEditor;

namespace GameFrame.Editors
{
    [CustomEditor(typeof(BasicSkillNodeDataConfig))]
    public class BasicSkillNodeDataConfig_Editor : Editor
    {
        private bool showPrerequisiteSkills = true;

        public override void OnInspectorGUI()
        {
            // 获取目标对象
            BasicSkillNodeDataConfig config = (BasicSkillNodeDataConfig)target;

            // 显示技能ID
            config.SkillID = EditorGUILayout.IntField("技能ID", config.SkillID);

            // 显示技能名称
            config.SkillName = EditorGUILayout.TextField("技能名称", config.SkillName);

            // 显示技能描述
            config.SkillDescription = EditorGUILayout.TextField("技能描述", config.SkillDescription);

            // 显示技能所需等级
            config.RequiredLevel = EditorGUILayout.IntField("所需等级", config.RequiredLevel);

            // 显示解锁该技能所需的技能点数
            config.SkillPointsCost = EditorGUILayout.IntField("所需技能点数", config.SkillPointsCost);

            // 可折叠的前置技能列表
            showPrerequisiteSkills = EditorGUILayout.Foldout(showPrerequisiteSkills, "前置技能列表");
            if (showPrerequisiteSkills)
            {
                EditorGUI.indentLevel++;
                if (config.PrerequisiteSkills == null)
                {
                    config.PrerequisiteSkills = new List<BasicSkillNodeDataConfig>();
                }

                // 显示前置技能列表的长度
                int prerequisiteCount = EditorGUILayout.IntField("前置技能数量", config.PrerequisiteSkills.Count);

                // 更新前置技能列表的长度
                if (prerequisiteCount != config.PrerequisiteSkills.Count)
                {
                    while (prerequisiteCount > config.PrerequisiteSkills.Count)
                        config.PrerequisiteSkills.Add(null);

                    while (prerequisiteCount < config.PrerequisiteSkills.Count)
                        config.PrerequisiteSkills.RemoveAt(config.PrerequisiteSkills.Count - 1);
                }

                // 显示并编辑每个前置技能
                for (int i = 0; i < config.PrerequisiteSkills.Count; i++)
                {
                    config.PrerequisiteSkills[i] = (BasicSkillNodeDataConfig)EditorGUILayout.ObjectField(
                        $"前置技能 {i + 1}",
                        config.PrerequisiteSkills[i],
                        typeof(BasicSkillNodeDataConfig),
                        false);
                }

                EditorGUI.indentLevel--;
            }

            // 保存更改
            if (GUI.changed)
            {
                EditorUtility.SetDirty(config);
            }
        }
    }
}