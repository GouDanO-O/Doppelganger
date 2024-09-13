using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using UnityEngine;
using GameFrame.Net;
using UnityEngine.Scripting;
using Unity.Netcode;
using QFramework;

namespace GameFrame.World
{
    public partial class WorldObj : NetworkBehaviour,IController
    {
        public WorldObjDataConfig thisDataConfig;
        
        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }

        public virtual void InitData()
        {
            if (thisDataConfig is WorldObjDataConfig)
            {
                InitComponents();
            }
            else
            {
                Debug.LogError("MonsterDataConfig is not set!");
            }
        }

        protected virtual void InitComponents()
        {
            
        }
    }
}


