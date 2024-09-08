using GameFrame;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.Net
{
    /// <summary>
    /// 网络管理类
    /// </summary>
    public class NetManager : MonoSingleton<NetManager>,IController
    {
        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

