using System;
using System.Collections.Generic;
using GameFrame.World;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace GameFrame.Config
{
    [Serializable]
    public struct SConditionData
    {
        [LabelText("消耗的是否是物品")]
        public bool isItem;
        
        [LabelText("会消耗的数量(也可以是等级或玩家属性值或技能等)")]
        public float willCostCount;

        [LabelText("当前等级")]
        public int thisLevel;
    }
    
    [Serializable]
    public abstract class SkillCondition : ISkillCondition
    {
        [LabelText("技能条件等级数据列表(必须包含等级为1的数据")]
        public List<SConditionData> ConditionDataList = new List<SConditionData>()
        {
            new SConditionData()
            {
                thisLevel = 1
            }
        };

        /// <summary>
        /// 首先判断条件是否满足
        /// 如果这个技能的所有条件都满足
        /// 最后才执行
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="curSkillLevel"></param>
        /// <returns></returns>
        public abstract bool CheckCondition(WorldObj owner, int curSkillLevel = 1);

        /// <summary>
        /// 执行条件
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="curSkillLevel"></param>
        public abstract void ExcuteCondition(WorldObj owner, int curSkillLevel = 1);
        
        /// <summary>
        /// 根据等级去计算伤害
        /// </summary>
        /// <param name="curLevel"></param>
        /// <returns>如果没有等级则取默认值</returns>
        protected float GetLeveledDamage(int curLevel)
        {
            float levelCost = 0;
            for (int i = 0; i < ConditionDataList.Count; i++)
            {
                SConditionData curLevelData= ConditionDataList[i];
                if (curLevel <= curLevelData.thisLevel)
                {
                    levelCost=curLevelData.willCostCount;
                    break;
                }
            }

            return levelCost;
        }
    }
}