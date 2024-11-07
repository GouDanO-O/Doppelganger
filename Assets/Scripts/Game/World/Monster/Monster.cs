using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using GameFrame.Net;
using QFramework;
using UnityEngine;

namespace GameFrame.World
{
    /// <summary>
    /// 怪物主类
    /// </summary>
    public class Monster : WorldObj
    {
        public DeformationController deformationController { get;protected set; }
 
        public override void Init()
        {
            base.Init();
            
        }
    }
}

