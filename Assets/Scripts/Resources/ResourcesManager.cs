using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

namespace FrameWork
{
    public interface IResoucesSystem : ISystem
    {

    }

    public class ResourcesManager : AbstractSystem,IResoucesSystem
    {
        public UnityAction onFirstLoadComplete;

        public bool Initialized { get; set; }

        protected override void OnInit()
        {
            
        }
    }
}

