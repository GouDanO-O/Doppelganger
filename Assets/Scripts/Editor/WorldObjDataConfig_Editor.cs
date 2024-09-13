using UnityEngine;
using UnityEditor;
using GameFrame.Config;

namespace GameFrame.Editors
{
    [CustomEditor(typeof(WorldObjDataConfig))]
    public class WorldObjDataConfig_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            WorldObjDataConfig config = (WorldObjDataConfig)target;

            config.gravity = EditorGUILayout.FloatField("重力", config.gravity);
            
            EditorGUILayout.Space(15);
            if (GUI.changed)
            {
                EditorUtility.SetDirty(config);
            }
            
        }
    }
}

