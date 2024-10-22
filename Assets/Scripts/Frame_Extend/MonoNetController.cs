using GameFrame.World;
using QFramework;
using Unity.Netcode;
using UnityEngine;

namespace GameFrame
{
    public abstract class BasicNetController : NetworkBehaviour, IController
    {
        public WorldObj owner;
        
        public virtual void InitData(WorldObj owner)
        {
            this.owner = owner;
        }
        
        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }
    }
}

