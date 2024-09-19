using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.Config
{
    public abstract class SkillBehaviorConfig : ScriptableObject
    {
        public abstract void ExecuteSkill(GameObject user=null, GameObject target=null,int triggerLevel=0);
    } 
}

