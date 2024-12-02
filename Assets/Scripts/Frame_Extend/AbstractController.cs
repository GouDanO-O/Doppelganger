using System.Collections.Generic;
using GameFrame.World;
using QFramework;
using UnityEngine;

namespace GameFrame
{
    public abstract class AbstractController : IController
    {
        public WorldObj owner;
        
        public BaseController controller;
        
        public virtual void InitData(WorldObj owner)
        {
            this.owner = owner;
            this.controller = owner.thisController;
        }

        public abstract void DeInitData();
        
        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }
    }
}

