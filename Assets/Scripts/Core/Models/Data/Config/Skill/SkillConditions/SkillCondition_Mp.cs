﻿using GameFrame.World;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    [LabelText("消耗MP")]
    public class SkillCondition_Mp : SkillCondition
    {
        public override bool CheckCondition(WorldObj owner,int curSkillLevel = 1)
        {
            float curHpCount = owner.healthyController.curHealthy.Value;
            float curCost = GetLeveledDamage(curSkillLevel);
            return curHpCount >= curCost;
        }
        
        public override void ExcuteCondition(WorldObj owner, int curSkillLevel = 1)
        {

        }
    }
}