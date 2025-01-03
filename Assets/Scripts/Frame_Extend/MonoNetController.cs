using GameFrame.World;
using QFramework;
using Unity.Netcode;
using UnityEngine;

namespace GameFrame
{
    public abstract class MonoNetController : NetworkBehaviour, IController
    {
        public abstract void DeInitData();
        
        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }
    }
}

