using GameFrame.World;
using QFramework;
using UnityEngine;

namespace GameFrame
{
    public abstract class AbstractController : IController
    {
        public WorldObj owner;
        
        public virtual void InitData(WorldObj owner)
        {
            this.owner = owner;
        }

        public abstract void DeInitData();
        
        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }
    }
}

