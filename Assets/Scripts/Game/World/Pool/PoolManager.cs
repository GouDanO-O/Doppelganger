using UnityEngine;
using QFramework;

namespace GameFrame.World
{
    public class PoolManager : MonoNetSingleton<PoolManager>,IController
    {
        public virtual IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }
    }
}

