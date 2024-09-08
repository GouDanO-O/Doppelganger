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

        public bool Initialized { get; set; }

        public int loadedCount { get; private set; }

        private int maxLoadCount = 1;

        ResoucesUtility loader;

        ResourceData resourceData;

        protected override void OnInit()
        {
            loader = this.GetUtility<ResoucesUtility>();
            loader.InitLoader();
            resourceData=this.GetModel<ResourceData>();
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        public void LoadData()
        {
            if (loader != null && resourceData!=null)
            {
                loader.LoadScriptObjAsync<GameSettingConfig>(QAssetBundle.Configs.GameSettingConfig, (data) =>
                {
                    resourceData.SettingConfig = data;
                    LoadCheck();
                });
            }
        }

        /// <summary>
        /// 每加载一个就进行检测
        /// </summary>
        private void LoadCheck()
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

