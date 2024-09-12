using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.World
{
    public interface IBiology_Attack
    {
        public float damage { get; set; }

        public void HarmTarget(IBiology_Healthy target);
    }
    
    public abstract class AttackController : IBiology_Attack
    {
        public float damage { get; set; }
        
        public void HarmTarget(IBiology_Healthy target)
        {
            
        }
    }
}

