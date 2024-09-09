using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace GameFrame
{
    public class CheatUtility : MonoBehaviour,IUtility
    {
        private void Start()
        {   
            Main.Interface.RegisterUtility(this);
        }
    } 
}

