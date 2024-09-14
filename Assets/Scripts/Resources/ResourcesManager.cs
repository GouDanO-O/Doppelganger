using GameFrame.Config;
using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

namespace GameFrame
{

    public class ResourcesManager : AbstractSystem
    {
        public UnityAction onFirstLoadComplete;
        public int loadedCount { get; private set; }

        private int maxLoadCount = 3;

        ResoucesUtility loader;

        ResourcesModel resourcesModel;

        protected override void OnInit()
        {
            loader = this.GetUtility<ResoucesUtility>();
            loader.InitLoader();
            resourcesModel=this.GetModel<ResourcesModel>();
        }

        public void InitialLoad()
        {
            if (loader != null && resourcesModel!=null)
            {
                loader.LoadScriptObjAsync<GameSettingConfig>(QAssetBundle.Configs.GameSettingConfig, (data) =>
                {
                    resourcesModel.SettingConfig = data;
                    InitialLoadCheck();
                });
                loader.LoadScriptObjAsync<NetDataConfig>(QAssetBundle.Configs.NetDataConfig, (data) =>
                {
                    resourcesModel.NetDataConfig = data;
                    InitialLoadCheck();
                });
                loader.LoadInputActionAsset(QAssetBundle.Player_inputactions.Player, (data) =>
                {
                    resourcesModel.InputActionAsset = data;
                    InitialLoadCheck();
                });
            }
        }
        
        /// <summary>
        /// 每加载一个就进行检测
        /// </summary>
        private void InitialLoadCheck()
        {
            loadedCount++;
            if (loadedCount == maxLoadCount)
            {
                onFirstLoadComplete?.Invoke();
                Debug.Log("加载完成");
            }
            Debug.Log("加载数据成功");
        }
    }
}

