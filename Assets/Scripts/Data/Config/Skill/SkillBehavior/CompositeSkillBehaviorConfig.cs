using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.Config
{
    [CreateAssetMenu(fileName = "CompositeSkillBehaviorConfig", menuName = "配置/技能/行为树")]
    public class CompositeSkillBehaviorConfig : SkillBehaviorConfig
    {
        public List<SkillBehaviorConfig> SkillBehaviors;
        
        public override void ExecuteSkill(GameObject user, GameObject target, int triggerLevel)
        {
            foreach (var skillBehavior  in SkillBehaviors)
            {
                if (skillBehavior != null)
                {
                    skillBehavior.ExecuteSkill(user, target);
                }
            }
        }
    }
}

