using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameFrame.Config;
using Sirenix.OdinInspector.Demos.RPGEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace GameFrame.Editor
{
    public class SkillConfigTable_Editor : OdinMenuEditorWindow
    {
        [MenuItem("工具/技能编辑器")]
        private static void Open()
        {
            var window = GetWindow<SkillConfigTable_Editor>();
            window.titleContent.text = "技能编辑器";
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1200, 800);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            
            tree.Add("已创建的技能",new OwnedSkillConfig_Editor());
            
            tree.Add("编辑技能",new EditSkillConfig_Editor());
            return tree;
        }
    }
}

