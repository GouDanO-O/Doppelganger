using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using GameFrame.Editor;
using QFramework;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace GameFrame.Config
{
    public class EditSkillConfig_Editor
    {
        [Flags]
        public enum EDisplayState
        {
            None,
            Everything,
            Gobalation,
            Timeline
        }

        private const int ButtonCount = 7;  // 按钮数量
        
        // 用于创建新技能配置的相关变量
        private string newSkillName = "NewSkill";
        private string savePath = "Assets/Res/Data/Configs/Skill/Skills"; // 默认保存路径
        private ESkill_NodeTreeType skillType = ESkill_NodeTreeType.Live; // 技能类别

        // 创建新技能按钮
        [HorizontalGroup("MainGroup/Buttons", Width = 1f / ButtonCount)]
        [Button("创建", ButtonSizes.Large)]
        public void CreateNewSkillConfig()
        {
            // 检查保存路径是否存在
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
                Debug.Log($"路径不存在，已创建保存路径: {savePath}");
            }
            
            var newSkill = ScriptableObject.CreateInstance<SkillNodeDataConfig>();
            newSkill.SkillName = newSkillName; 
            newSkill.SkillNodeType = skillType;

            // 构建保存路径
            string assetPath = $"{savePath}/{newSkillName}.asset";

            // 检查是否已经存在相同名称的技能配置文件
            if (File.Exists(assetPath))
            {
                Debug.LogError($"技能文件 {newSkillName}.asset 已经存在，请选择不同的名称！");
                return;
            }

            // 创建技能配置文件并保存
            AssetDatabase.CreateAsset(newSkill, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh(); 
            Debug.Log($"技能 {newSkillName} 创建成功，保存到路径: {assetPath}");
            
            selectedSkillConfig = AssetDatabase.LoadAssetAtPath<SkillNodeDataConfig>(assetPath);
        }
        
        // 保存按钮
        [HorizontalGroup("MainGroup/Buttons", Width = 1f / ButtonCount)]
        [Button("保存", ButtonSizes.Large)]
        public void SaveSkillConfig()
        {
            if (selectedSkillConfig != null)
            {
                EditorUtility.SetDirty(selectedSkillConfig);
                AssetDatabase.SaveAssets();
                Debug.Log($"技能配置 {selectedSkillConfig.SkillName} 已保存.");
            }
            else
            {
                Debug.LogWarning("未选择技能配置，无法保存.");
            }
        }

        // 保存到按钮，允许选择保存路径
        [HorizontalGroup("MainGroup/Buttons", Width = 1f / ButtonCount)]
        [Button("保存到", ButtonSizes.Large)]
        public void SaveSkillConfigTo()
        {
            if (selectedSkillConfig != null)
            {
                string path = EditorUtility.SaveFilePanelInProject("保存技能配置到", selectedSkillConfig.SkillName, "asset", "请选择保存路径");
                if (!string.IsNullOrEmpty(path))
                {
                    AssetDatabase.CreateAsset(selectedSkillConfig, path);
                    AssetDatabase.SaveAssets();
                    Debug.Log($"技能配置保存到: {path}");
                }
            }
            else
            {
                Debug.LogWarning("未选择技能配置，无法保存.");
            }
        }

        // 还原修改按钮
        [HorizontalGroup("MainGroup/Buttons", Width = 1f / ButtonCount)]
        [Button("还原修改", ButtonSizes.Large)]
        public void RevertChanges()
        {
            if (selectedSkillConfig != null)
            {
                AssetDatabase.Refresh();
                Debug.Log($"技能配置 {selectedSkillConfig.SkillName} 的修改已还原.");
            }
            else
            {
                Debug.LogWarning("未选择技能配置，无法还原.");
            }
        }

        // 拷贝配置按钮
        private SkillNodeDataConfig copiedSkillConfig;
        [HorizontalGroup("MainGroup/Buttons", Width = 1f / ButtonCount)]
        [Button("拷贝配置", ButtonSizes.Large)]
        public void CopySkillConfig()
        {
            if (selectedSkillConfig != null)
            {
                copiedSkillConfig = ScriptableObject.CreateInstance<SkillNodeDataConfig>();
                EditorUtility.CopySerialized(selectedSkillConfig, copiedSkillConfig);
                Debug.Log($"技能配置 {selectedSkillConfig.SkillName} 已拷贝.");
            }
            else
            {
                Debug.LogWarning("未选择技能配置，无法拷贝.");
            }
        }

        // 粘贴配置按钮
        [HorizontalGroup("MainGroup/Buttons", Width = 1f / ButtonCount)]
        [Button("粘贴配置", ButtonSizes.Large)]
        public void PasteSkillConfig()
        {
            if (selectedSkillConfig != null && copiedSkillConfig != null)
            {
                EditorUtility.CopySerialized(copiedSkillConfig, selectedSkillConfig);
                AssetDatabase.SaveAssets();
                Debug.Log($"已粘贴配置到 {selectedSkillConfig.SkillName}");
            }
            else
            {
                Debug.LogWarning("未选择技能配置或未拷贝配置，无法粘贴.");
            }
        }

        // 拷贝配置并应用到新配置按钮
        [HorizontalGroup("MainGroup/Buttons", Width = 1f / ButtonCount)]
        [Button("拷贝并应用到新配置", ButtonSizes.Large)]
        public void CopyAndApplyToNewConfig()
        {
            if (selectedSkillConfig != null)
            {
                string newPath = EditorUtility.SaveFilePanelInProject("保存新技能配置", "NewSkillConfig", "asset", "请选择保存路径");
                if (!string.IsNullOrEmpty(newPath))
                {
                    SkillNodeDataConfig newConfig = ScriptableObject.CreateInstance<SkillNodeDataConfig>();
                    EditorUtility.CopySerialized(selectedSkillConfig, newConfig);
                    AssetDatabase.CreateAsset(newConfig, newPath);
                    AssetDatabase.SaveAssets();
                    Debug.Log($"技能配置拷贝并保存为新配置，保存到: {newPath}");
                }
            }
            else
            {
                Debug.LogWarning("未选择技能配置，无法拷贝并应用.");
            }
        }

        // 显示状态
        [HorizontalGroup("MainGroup", Width = 0.15f)]
        [LabelText("显示状态")]
        public EDisplayState displayState = EDisplayState.None;
        
        // 选择的技能配置文件
        [InlineEditor(Expanded = true)]
        [LabelText("选择技能配置")]
        public SkillNodeDataConfig selectedSkillConfig;
    }
}
