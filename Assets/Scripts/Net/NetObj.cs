using System.Collections;
using System.Collections.Generic;
using QFramework;
using Unity.Netcode;
using UnityEngine;

namespace  GameFrame.Net
{
    public partial class NetObj : NetworkBehaviour,IController
    {
        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }
    } 
}

