using System;
using GameFrame;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.Netcode;
using UnityEngine;

namespace GameFrame.Net
{
    /// <summary>
    /// 网络管理类
    /// </summary>
    public class NetManager : MonoSingleton<NetManager>,IJob,IController
    {
        public bool isLogin { get; private set; }
        
        public bool isConnecting { get; private set; }

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
            ResourcesModel resourcesModel = GetArchitecture().GetModel<ResourcesModel>();
            TickTimer=resourcesModel.NetDataConfig.TickTime;
            shortTickTimeDeep=resourcesModel.NetDataConfig.ShortTickTimeDeep;
            normalTickTimeDeep=resourcesModel.NetDataConfig.NormalTickTimeDeep;
            longTickTimeDeep=resourcesModel.NetDataConfig.LongTickTimeDeep;
            heartDisconnectCount=resourcesModel.NetDataConfig.HeartDisconnectCount;
        }

        public void StartHost()
        {
            
        }

        public void StartLocal()
        {
            
        }

        public void TryConnectHost()
        {
            
        }
        
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

