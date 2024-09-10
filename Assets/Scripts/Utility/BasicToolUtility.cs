using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace GameFrame
{
    public class BasicToolUtility : MonoBehaviour,IUtility
    {
        
        // Start is called before the first frame update
        protected virtual void Start()
        {
            InitUtility();
        }

        protected virtual void InitUtility()
        {
            Main.Interface.RegisterUtility(this);
        }

        protected virtual void OnGUI()
        {
            
        }
    }
}

