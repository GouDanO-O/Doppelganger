using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.World
{
    public interface IBiology_Attack
    {
        public float damage { get; set; }

        public void HarmTarget(HealthyController target);
    }
    
    public abstract class AttackController : IBiology_Attack
    {
        public float damage { get; set; }

        public virtual void InitAttacker()
        {
            
        }
        
        public virtual void HarmTarget(HealthyController target)
        {
            target.Beharmed(damage);
        }
    }
}

