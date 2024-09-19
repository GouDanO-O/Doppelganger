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

            config.thisPrefab = (GameObject)EditorGUILayout.ObjectField("预制体", config.thisPrefab, typeof(GameObject), false);
            
            config.gravity = EditorGUILayout.FloatField("重力", config.gravity);
            
            EditorGUILayout.Space(15);
            config.healthyable = EditorGUILayout.Toggle("是否有生命值", config.healthyable);
            if (config.healthyable)
            {
                EditorGUILayout.LabelField("生命设置", EditorStyles.boldLabel);
                config.healthyData.maxHealth = EditorGUILayout.FloatField("最大生命值", config.healthyData.maxHealth);
                config.healthyData.maxArmor = EditorGUILayout.FloatField("最大护甲值", config.healthyData.maxArmor);
                config.healthyData.damageReductionRatio = EditorGUILayout.FloatField("伤害减免比例", config.healthyData.damageReductionRatio);
            }
            
            EditorGUILayout.Space(15);
            if (GUI.changed)
            {
                EditorUtility.SetDirty(config);
            }
            
        }
    }
}

