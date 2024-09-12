using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFrame.Config;
using UnityEditor;

namespace GameFrame.Editors
{
    [CustomEditor(typeof(MonsterDataConfig))]
    public class MonsterDataConfig_Editor : BiologyDataConfig_Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space(10);
            MonsterDataConfig config = (MonsterDataConfig)target;
            
            config.attackable = EditorGUILayout.Toggle("能否进行攻击", config.attackable);
            if (config.attackable)
            {
         
                
                EditorGUILayout.LabelField("攻击设置", EditorStyles.boldLabel);
                
                config.attackData.attackType = (EAttackType)EditorGUILayout.EnumPopup("攻击类型", config.attackData.attackType);
                
                config.attackData.basicDamage = EditorGUILayout.FloatField("基础伤害", config.attackData.basicDamage);
                
                config.attackData.criticalRate = EditorGUILayout.FloatField("暴击率", config.attackData.criticalRate);
                
                config.attackData.criticalDamage = EditorGUILayout.FloatField("暴击伤害", config.attackData.criticalDamage);

                switch (config.attackData.attackType)
                {
                    case EAttackType.None:

                        break;
                    case EAttackType.CloseAttack:

                        break;
                    case EAttackType.RemoteAttack:
                        config.attackData.attackDistance = EditorGUILayout.FloatField("攻击距离", config.attackData.attackDistance);
                        break;
                }
            }
        }
    }
}

