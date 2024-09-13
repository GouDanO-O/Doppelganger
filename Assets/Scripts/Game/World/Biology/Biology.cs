using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using UnityEngine;

namespace GameFrame.World
{
    public abstract class Biology : WorldObj
    {
        protected HealthyController healthyController;
        
        protected MoveController moveController;

        public BiologyDataConfig biologyDataConfig;

        public virtual void InitData()
        {
            
        }
    }
}

