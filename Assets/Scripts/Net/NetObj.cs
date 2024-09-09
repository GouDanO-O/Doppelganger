using System.Collections;
using System.Collections.Generic;
using QFramework;
using Unity.Netcode;
using UnityEngine;

namespace  GameFrame.Net
{
    public class NetObj : NetworkBehaviour,IController
    {
        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }
    } 
}

