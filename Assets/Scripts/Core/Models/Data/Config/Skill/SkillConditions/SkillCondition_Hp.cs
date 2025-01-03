using System;
using System.Collections.Generic;
using GameFrame.World;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    /// <summary>
    /// 消耗HP来执行
    /// 直接造成真实伤害
    /// </summary>
    [LabelText("消耗HP")]
    public class SkillCondition_Hp : SkillCondition
    {
        public override bool CheckCondition(WorldObj owner,int curSkillLevel = 1)
        {
            float curHpCount = owner.healthyController.curHealthy.Value;
            float curCost = GetLeveledDamage(curSkillLevel);
            return curHpCount >= curCost;
        }
        
        public override void ExcuteCondition(WorldObj owner, int curSkillLevel = 1)
        {
            float curHpCount = owner.healthyController.curHealthy.Value;
            float curCost = GetLeveledDamage(curSkillLevel);
            if (curHpCount >= curCost)
            {
                TriggerDamageData_TemporalityPoolable DamageData = TriggerDamageData_TemporalityPoolable.Allocate();
                DamageData.UpdateEnforcer(owner);
                DamageData.UpdateSufferer(owner);
                DamageData.UpdateBasicDamage(curCost,EElementType.TrueInjury);
                DamageData.HarmedSufferer();
            }
            else
            {
                Debug.Log("HP不足");
            }
        }
    }
}

