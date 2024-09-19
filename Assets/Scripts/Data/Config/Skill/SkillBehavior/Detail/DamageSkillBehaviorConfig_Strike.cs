using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using UnityEngine;

namespace GameFrame.Skill
{
    [CreateAssetMenu(fileName = "SkillBehaviorConfig", menuName = "配置/技能/行为/直接造成伤害")]
    public class DamageSkillBehaviorConfig_Strike : SkillBehaviorConfig
    {
        public int damage;
        
        public string description;
        
        public override void ExecuteSkill(GameObject user, GameObject target, int triggerLevel)
        {
            
        }
    }
}

