using System.Collections;
using System.Collections.Generic;
using GameFrame.World;
using UnityEngine;

namespace GameFrame.World
{
    public interface IBiology_Healthy
    {
        public bool isDeath { get; set; }
        
        public float curHealthy { get; set; }
        
        public float maxHealthy { get; set; }

        public void Beharmed(float damage);

        public void Becuring(float cureValue);

        public void Death();
    }
    
    public abstract class HealthyController : IBiology_Healthy
    {
        public bool isDeath { get; set; }
        public float curHealthy { get; set; }
        public float maxHealthy { get; set; }

        public virtual void InitHealthyer()
        {
            
        }
        
        public virtual void Beharmed(float damage)
        {
            if(this.isDeath)
                return;
            this.curHealthy -= damage;
            if (curHealthy <= 0)
            {
                Death();
            }
        }

        public virtual void Becuring(float cureValue)
        {
            this.curHealthy += cureValue;
            if (curHealthy >= this.maxHealthy)
            {
                this.curHealthy = this.maxHealthy;
            }
        }

        public virtual void Death()
        {
            isDeath = true;
        }
    }
}

