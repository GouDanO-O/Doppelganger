using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFrame.World;

namespace GameFrame.Config
{
    public abstract class SkillBehaviorConfig : ScriptableObject
    {
        public abstract void ExecuteSkill(WorldObj user=null, WorldObj target=null,int triggerLevel=0);
    } 
}

