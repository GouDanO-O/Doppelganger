using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFrame.Net;
using UnityEngine.Scripting;
using Unity.Netcode;
using QFramework;

namespace GameFrame.World
{
    public partial class WorldObj : NetworkBehaviour,IController
    {
        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }
    }
}


