#if UNITY_EDITOR

namespace GameFrame.Editor
{
    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities;
    using Sirenix.Utilities.Editor;
    using UnityEditor;
    using UnityEngine;
    using System.Linq;
    
    public class ConfigMenu_Editor : OdinMenuEditorWindow
    {
        [MenuItem("配置/配置面板")]
        private static void OpenWindow()
        {
            var window = GetWindow<ConfigMenu_Editor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);
            
            tree.DefaultMenuStyle.IconSize = 28.00f;
            tree.Config.DrawSearchToolbar = true;
            
            return tree;
        }
    }
}
#endif
