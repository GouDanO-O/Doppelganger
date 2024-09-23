using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using GameFrame.Config;
using Sirenix.OdinInspector;

namespace GameFrame.Editor
{
    public class CreateSkillNodeDataConfig_Editor : MonoBehaviour
    {
        [Button("Create New Skill Configuration", ButtonSizes.Large)]
        public void CreateNewSkill()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Skill Config", "NewSkillConfig", "asset",
                "Please enter a file name to save the skill config to");
            if (!string.IsNullOrEmpty(path))
            {
                // 创建新的 SkillNodeDataConfig 实例
                SkillNodeDataConfig newSkillConfig = ScriptableObject.CreateInstance<SkillNodeDataConfig>();
                AssetDatabase.CreateAsset(newSkillConfig, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();

                // 选中新创建的资产，以便在窗口中显示
                Selection.activeObject = newSkillConfig;
                EditorUtility.FocusProjectWindow();

                // 获取当前打开的 ConfigMenu_Editor 窗口实例
                ConfigMenu_Editor window = EditorWindow.GetWindow<ConfigMenu_Editor>();
                if (window != null)
                {
                    window.ForceMenuTreeRebuild();
                    // 选中新创建的菜单项
                    var menuItem = window.MenuTree.MenuItems
                        .SelectMany(item => item.GetChildMenuItemsRecursive(true))
                        .FirstOrDefault(item => item.Value == newSkillConfig);
                    if (menuItem != null)
                    {
                        window.MenuTree.Selection.Clear();
                        window.MenuTree.Selection.Add(menuItem);
                    }
                }
            }
        }
    }
}

