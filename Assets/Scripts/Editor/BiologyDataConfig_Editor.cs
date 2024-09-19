using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GameFrame.Config;

namespace GameFrame.Editors
{
    [CustomEditor(typeof(BiologyDataConfig))]
    public class BiologyDataConfig_Editor : WorldObjDataConfig_Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            EditorGUILayout.Space(15);
            BiologyDataConfig config = (BiologyDataConfig)target;
            
            config.moveable = EditorGUILayout.Toggle("是否可移动", config.moveable);
            if (config.moveable)
            {
                EditorGUILayout.LabelField("移动设置", EditorStyles.boldLabel);
                config.moveData.walkSpeed = EditorGUILayout.FloatField("行走速度", config.moveData.walkSpeed);
                config.moveData.runSpeed = EditorGUILayout.FloatField("奔跑速度", config.moveData.runSpeed);
                config.moveData.maxPitchAngle = EditorGUILayout.Vector2Field("上下Pitch角", config.moveData.maxPitchAngle);
            }

            EditorGUILayout.Space(15);

            
            config.dashable = EditorGUILayout.Toggle("是否可冲刺", config.dashable);
            if (config.dashable)
            {
                EditorGUILayout.LabelField("冲刺设置", EditorStyles.boldLabel);
                config.dashData.dashSpeed = EditorGUILayout.FloatField("冲刺速度", config.dashData.dashSpeed);
                config.dashData.dashCD = EditorGUILayout.FloatField("冲刺冷却时间", config.dashData.dashCD);
            }
            
            EditorGUILayout.Space(15);
            config.jumpable = EditorGUILayout.Toggle("是否可跳跃", config.jumpable);
            if (config.jumpable)
            {
                EditorGUILayout.LabelField("跳跃设置", EditorStyles.boldLabel);
                config.jumpData.jumpHeight = EditorGUILayout.FloatField("跳跃高度", config.jumpData.jumpHeight);
                config.jumpData.canDoubleJump = EditorGUILayout.Toggle("是否可二段跳", config.jumpData.canDoubleJump);
            }
            
            
            EditorGUILayout.Space(15);
            // 处理 crouchable 控制相关的数据展示
            config.crouchable = EditorGUILayout.Toggle("是否可蹲下", config.crouchable);
            if (config.crouchable)
            {
                EditorGUILayout.LabelField("蹲下设置", EditorStyles.boldLabel);
                config.crouchData.crouchSpeed = EditorGUILayout.FloatField("蹲下速度", config.crouchData.crouchSpeed);
                config.crouchData.crouchReduceRatio = EditorGUILayout.FloatField("身高缩减比例", config.crouchData.crouchReduceRatio);
            }
            
            EditorGUILayout.Space(15);
            config.skillTree = (SkillTree)EditorGUILayout.ObjectField("技能树配置", config.skillTree, typeof(SkillTree), false);
            // 保存更改
            if (GUI.changed)
            {
                EditorUtility.SetDirty(config);
            }
        }
    }
}


