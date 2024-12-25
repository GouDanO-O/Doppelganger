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

        private int maxLoadCount = 4;

        ResoucesUtility loader;

        ResourcesData_Model _resourcesDataModel;

        protected override void OnInit()
        {
            loader = this.GetUtility<ResoucesUtility>();
            loader.InitLoader();
            _resourcesDataModel=this.GetModel<ResourcesData_Model>();
        }

        public void InitialLoad()
        {
            if (loader != null && _resourcesDataModel!=null)
            {
                loader.LoadScriptObjAsync<GameSetting_Config>(QAssetBundle.Configs.GameSettingConfig, (data) =>
                {
                    _resourcesDataModel.SettingConfig = data;
                    InitialLoadCheck();
                });
                loader.LoadScriptObjAsync<NetData_Config>(QAssetBundle.Configs.NetDataConfig, (data) =>
                {
                    _resourcesDataModel.NetDataConfig = data;
                    InitialLoadCheck();
                });
                loader.LoadInputActionAsset(QAssetBundle.Player_inputactions.Player, (data) =>
                {
                    _resourcesDataModel.InputActionAsset = data;
                    InitialLoadCheck();
                });
                loader.LoadScriptObjAsync<UIPrefabsData_Config>(QAssetBundle.Configs.UIPrefabsDataConfig, (data) =>
                {
                    _resourcesDataModel.UIPrefabsDataConfig = data;
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

