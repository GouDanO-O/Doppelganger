using GameFrame.World;
using QFramework;
using Unity.Netcode;
using UnityEngine;

namespace GameFrame
{
    public abstract class MonoNetController : NetworkBehaviour, IController
    {
        [HideInInspector]public WorldObj owner;
        
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

