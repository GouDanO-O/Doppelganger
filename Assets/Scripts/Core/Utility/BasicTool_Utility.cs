using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace GameFrame
{
    public abstract class BasicTool_Utility : IUtility
    {
        public abstract void InitUtility();

        public virtual void DeInitUtility()
        {
            
        }
    }
}


