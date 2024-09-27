using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFrame.Config;
using Sirenix.OdinInspector;
using UnityEditor;

namespace GameFrame.Editor
{
    public class OwnedSkillConfig_Editor
    {
        // 存储不同类别的技能列表
        private List<SkillNodeDataConfig> liveSkills = new List<SkillNodeDataConfig>();
        
        private List<SkillNodeDataConfig> combatSkills = new List<SkillNodeDataConfig>();
        
        private List<SkillNodeDataConfig> specialSkills = new List<SkillNodeDataConfig>();

        public OwnedSkillConfig_Editor()
        {
            // 加载技能配置
            LoadSkills();
        }

        // 加载技能配置文件
        private void LoadSkills()
        {
            liveSkills.Clear();
            combatSkills.Clear();
            specialSkills.Clear();

            // 从指定路径加载所有的技能配置文件
            var skillConfigs = AssetDatabase.FindAssets("t:SkillNodeDataConfig",
                new[] { "Assets/Res/Data/Configs/Skill/Skills" });

            foreach (var guid in skillConfigs)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                SkillNodeDataConfig skillConfig = AssetDatabase.LoadAssetAtPath<SkillNodeDataConfig>(path);

                if (skillConfig != null)
                {
                    // 按照不同的技能类别分类
                    switch (skillConfig.SkillNodeType)
                    {
                        case ESkill_NodeTreeType.Live:
                            liveSkills.Add(skillConfig);
                            break;
                        case ESkill_NodeTreeType.Combat:
                            combatSkills.Add(skillConfig);
                            break;
                        case ESkill_NodeTreeType.Special:
                            specialSkills.Add(skillConfig);
                            break;
                    }
                }
            }
        }

        // 使用 Odin Inspector 显示技能列表，按照类别分组
        [FoldoutGroup("生存类技能")]
        [ShowInInspector]
        [LabelText("生存类技能列表")]
        public List<SkillNodeDataConfig> LiveSkills => liveSkills;

        [FoldoutGroup("战斗类技能")]
        [ShowInInspector]
        [LabelText("战斗类技能列表")]
        public List<SkillNodeDataConfig> CombatSkills => combatSkills;

        [FoldoutGroup("特殊类技能")]
        [ShowInInspector]
        [LabelText("特殊类技能列表")]
        public List<SkillNodeDataConfig> SpecialSkills => specialSkills;
    }
}