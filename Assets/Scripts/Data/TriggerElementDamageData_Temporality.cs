using System;
using System.Collections.Generic;
using QFramework;

namespace GameFrame.World
{
    public class TriggerElementDamageData_Temporality : TemporalityData
    {
        public Dictionary<EAction_Skill_ElementType,int> elementLevelDic=new Dictionary<EAction_Skill_ElementType,int>();
        
        public Dictionary<EAction_Skill_ElementType,float> elementDamageDic=new Dictionary<EAction_Skill_ElementType,float>();
        
        public Dictionary<EAction_Skill_ElementType,float> lastElementDurationDic=new Dictionary<EAction_Skill_ElementType,float>();

        private bool isElementDisappear = false;
        
        public void AddElement(EAction_Skill_ElementType element)
        {
            if (elementLevelDic.ContainsKey(element))
            {
                elementLevelDic[element]++;
            }
            else
            {
                elementLevelDic.Add(element, 1);
            }
        }

        /// <summary>
        /// 所有的元素是否都计算完毕
        /// </summary>
        /// <returns></returns>
        public bool IsElementDisappear()
        {
            if (lastElementDurationDic.Count == 0)
            {
                isElementDisappear = true;
            }
            else
            {
                isElementDisappear = false;
            }
            
            return isElementDisappear;
        }

        /// <summary>
        /// 主动更新当前会造成的元素伤害
        /// </summary>
        public void UpdateElementDuration()
        {
            if (elementLevelDic.Count > 0)
            {
                
            }
        }
        
        public override void DeInitData()
        {
            elementLevelDic.Clear();
            elementDamageDic.Clear();
            lastElementDurationDic.Clear();
        }
        
    }
}