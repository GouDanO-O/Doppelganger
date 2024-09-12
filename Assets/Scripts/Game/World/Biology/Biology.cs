using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.World
{
    public abstract class Biology : WorldObj
    {
        protected HealthyController healthyController;
        
        protected MoveController moveController;

        public virtual void InitData()
        {
            
        }
    }
}

