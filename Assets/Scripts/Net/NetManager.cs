using System;
using GameFrame;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using Unity.Jobs;
using Unity.Netcode;
using UnityEngine;

namespace GameFrame.Net
{
    /// <summary>
    /// 网络管理类
    /// </summary>
    public class NetManager : MonoSingleton<NetManager>,IJob,IController,IUnRegisterList
    {
        public bool isLogin { get; private set; }
        
        public bool isLocalGameMode { get; private set; }
        
        public bool isConnecting { get; private set; }

        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();

        private int curTickerCount=0;
        
        private float curTickerTime=0;

        private int curHeartDisconnectCount=0;

        protected float TickTimer;

        protected int shortTickTimeDeep;

        protected int normalTickTimeDeep;

        protected int longTickTimeDeep;

        protected int heartDisconnectCount;

        protected NetSpawner _netSpawner;

        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }

        public void InitComponents()
        {
            NetDataConfig netDataConfig = GetArchitecture().GetModel<ResourcesModel>().NetDataConfig;
            TickTimer = netDataConfig.TickTime;
            shortTickTimeDeep = netDataConfig.ShortTickTimeDeep;
            normalTickTimeDeep = netDataConfig.NormalTickTimeDeep;
            longTickTimeDeep = netDataConfig.LongTickTimeDeep;
            heartDisconnectCount = netDataConfig.HeartDisconnectCount;
        }

        /// <summary>
        /// 开启主机模式
        /// </summary>
        public void StartHost()
        {
            isLocalGameMode = false;
        }

        /// <summary>
        /// 开启客户端模式
        /// </summary>
        public void StartClient()
        {
            TryConnectHost();
        }

        /// <summary>
        /// 开启本地模式
        /// </summary>
        public void StartLocal()
        {
            isLocalGameMode = true;
        }

        /// <summary>
        /// 尝试链接主机
        /// </summary>
        public void TryConnectHost()
        {
            
        }
        
        /// <summary>
        /// 处理Tick逻辑
        /// </summary>
        public void Execute()
        {
            TickCheck();
        }

        /// <summary>
        /// Tick检测
        /// </summary>
        private void TickCheck()
        {
            if (isLogin&&isConnecting)
            {
                curTickerCount++;
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
            if (curTickerCount == shortTickTimeDeep)
            {
                
            }
        }
        
        /// <summary>
        /// 正常Tick
        /// </summary>
        private void NormalTickCheck()
        {
            if (curTickerCount == normalTickTimeDeep)
            {
                HeartCheck();
            }
        }
        
        /// <summary>
        /// 长Tick
        /// </summary>
        private void LongTickCheck()
        {
            if (curTickerCount == longTickTimeDeep)
            {
                curTickerCount = 0;
            }
        }

        /// <summary>
        /// 心跳检测
        /// </summary>
        private void HeartCheck()
        {
            if (curHeartDisconnectCount >= heartDisconnectCount)
            {
                isConnecting = false;
            }
        }
    }
}

