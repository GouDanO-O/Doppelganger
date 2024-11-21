using QFramework;
using Unity.Netcode;
using UnityEngine;

namespace GameFrame.World
{
    public class MonoNetSingleton<T> : MonoNetController,ISingleton where T : MonoNetSingleton<T>
    {
        private static T _instance;
        
        public static T Instance
        {
            get
            {
                return MonoSingletonProperty<T>.Instance;
            }
        }


        public override void DeInitData()
        {
            
        }

        public void OnSingletonInit()
        {
            
        }
    }
}

