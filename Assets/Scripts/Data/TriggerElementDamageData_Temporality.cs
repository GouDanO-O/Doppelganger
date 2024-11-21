using System;
using System.Collections.Generic;
using QFramework;

namespace GameFrame.World
{
    public class TriggerElementDamageData_Temporality : TemporalityData_Pool
    {
        public Dictionary<EAction_Skill_ElementType,int> elementLevelDic=new Dictionary<EAction_Skill_ElementType,int>();
        
        public Dictionary<EAction_Skill_ElementType,float> elementDamageDic=new Dictionary<EAction_Skill_ElementType,float>();
        
        public Dictionary<EAction_Skill_ElementType,float> lastElementDurationDic=new Dictionary<EAction_Skill_ElementType,float>();

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
        
        public override void OnRecycled()
        {
            
        }

        public override void Recycle2Cache()
        {
            
        }
    }
}