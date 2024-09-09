using System;
using GameFrame;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace GameFrame.Net
{
    /// <summary>
    /// 网络管理类
    /// </summary>
    public class NetManager : MonoSingleton<NetManager>,IController
    {
        private NetworkManager m_NetManager;
        
        private NetworkTransport m_NetTransport;
        
        public bool isLogin { get; private set; }

        private int curTicker=0;

        private int shortTickTimeDeep
        {
            get
            {
                return GetArchitecture().GetModel<ResourcesModel>().NetDataConfig.ShortTickTimeDeep;
            }
        }

        private int normalTickTimeDeep        
        {
            get
            {
                return GetArchitecture().GetModel<ResourcesModel>().NetDataConfig.NormalTickTimeDeep;
            }
        }
        
        private int longTickTimeDeep        
        {
            get
            {
                return GetArchitecture().GetModel<ResourcesModel>().NetDataConfig.LongTickTimeDeep;
            }
        }
        
        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }

        private void Awake()
        {
            InitComponents();   
        }

        private void InitComponents()
        {
            m_NetManager = GetComponent<NetworkManager>();
            m_NetTransport = GetComponent<NetworkTransport>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        private void FixedUpdate()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            TickCheck();
        }

        /// <summary>
        /// Tick检测
        /// </summary>
        private void TickCheck()
        {
            if (isLogin)
            {
                curTicker++;
                ShortTickCheck();
                NormalTickCheck();
                LongTickCheck();
            }
        }

        /// <summary>
        /// 短Tick
        /// </summary>
        private void ShortTickCheck()
        {
            if (curTicker == shortTickTimeDeep)
            {
                
            }
        }
        
        /// <summary>
        /// 正常Tick
        /// </summary>
        private void NormalTickCheck()
        {
            if (curTicker == normalTickTimeDeep)
            {
                
            }
        }
        
        /// <summary>
        /// 长Tick
        /// </summary>
        private void LongTickCheck()
        {
            if (curTicker == longTickTimeDeep)
            {
                curTicker = 0;
            }
        }
    }
}

