using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using GameFrame.Config;

namespace GameFrame.Editor
{
    public class ConfigMenu_Editor : OdinMenuEditorWindow
    {
        [MenuItem("配置/总面板")]
        private static void OpenWindow()
        {
            GetWindow<ConfigMenu_Editor>().Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree(supportsMultiSelect: true);
            tree.Selection.SupportsMultiSelect = false;

            // 添加创建新技能的选项到 BasicSkills
            tree.Add("Skills/BasicSkills/Create New Skill", new CreateSkillNodeDataConfig_Editor());

            // 查找并添加所有的 SkillNodeDataConfig 实例到 BasicSkills
            var skillConfigs = AssetDatabase.FindAssets("t:SkillNodeDataConfig");
            foreach (var guid in skillConfigs)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                SkillNodeDataConfig skillConfig = AssetDatabase.LoadAssetAtPath<SkillNodeDataConfig>(path);
                tree.Add("Skills/BasicSkills/" + skillConfig.name, skillConfig);
            }

            return tree;
        }
    }
}

