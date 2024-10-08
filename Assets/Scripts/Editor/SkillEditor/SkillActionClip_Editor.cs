using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace GameFrame.Config
{
    public class SkillActionClip_Editor : OdinEditorWindow
    {
        private SkillActionClip_Config currentClip;

        public static void OpenWindow(SkillActionClip_Config clip)
        {
            var window = GetWindow<SkillActionClip_Editor>("编辑行为片段");
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

