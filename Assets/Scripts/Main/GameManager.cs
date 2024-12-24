using System;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using GameFrame.Multilingual;
using GameFrame.Net;
using GameFrame.UI;
using GameFrame.World;
using UnityEngine;
using UnityEngine.Events;

namespace GameFrame
{
    /// <summary>
    /// 游戏全局管理
    /// </summary>
    public class GameManager : MonoSingleton<GameManager>,IController
    {
        protected SceneLoader sceneLoader;

        protected EGameState curGameState=EGameState.None;

        private ResourcesManager resourcesManager;

        private InputManager inputManager;
        
        private WorldManager worldManager;
        
        protected Font customFont;

        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }

        private void Awake()
        {
            InitComponent();
        }

        /// <summary>
        /// 初始化管理类和组件
        /// </summary>
        protected void InitComponent()
        {
            sceneLoader = GetArchitecture().GetSystem<SceneLoader>();
            sceneLoader.OnSceneLoadStart += LoadSceneStart;
            sceneLoader.OnSceneLoading += LoadingScene;
            sceneLoader.OnSceneLoadComplete += LoadSceneComplete;

            resourcesManager = GetArchitecture().GetSystem<ResourcesManager>();
            resourcesManager.onFirstLoadComplete += LoadComplete;

            inputManager = GetArchitecture().GetSystem<InputManager>();
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            LoadData();
        }

        private void OnDestroy()
        {
            UnRegistEvent();
        }
        
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            resourcesManager.InitialLoad();
        }

        /// <summary>
        /// 初始数据加载完成
        /// </summary>
        private void LoadComplete()
        {
            customFont = GetArchitecture().GetModel<ResourcesModel>().SettingConfig.CustomFont;
            NetManager.Instance.InitComponents();
            GetArchitecture().GetSystem<MultilingualManager>().InitLanguage();
            GetArchitecture().GetSystem<InputManager>().InitActionAsset();
            EnterMenu();
        }

        private void FixedUpdate()
        {
            if (IsGaming())
            {
                inputManager.MovementCheck();
                WorldManager.Instance.UpdateWorldLogic();
            }
        }

        /// <summary>
        /// 进入菜单
        /// </summary>
        protected void EnterMenu()
        { 
            sceneLoader.onLoadScene.Invoke(ESceneName.Menu);
        }

        /// <summary>
        /// 开始游戏
        /// </summary>
        public void StartGame()
        {
            sceneLoader.onLoadScene.Invoke(ESceneName.TestScene);
            PoolManager.Instance.InitNormalPool();
        }

        /// <summary>
        /// 注销事件
        /// </summary>
        protected void UnRegistEvent()
        {
            sceneLoader.OnSceneLoadStart -= LoadSceneStart;
            sceneLoader.OnSceneLoading -= LoadingScene;
            sceneLoader.OnSceneLoadComplete -= LoadSceneComplete;
            resourcesManager.onFirstLoadComplete -= LoadComplete;
        }

        /// <summary>
        /// 开始加载场景
        /// </summary>
        protected void LoadSceneStart()
        {
            curGameState = EGameState.StartLoading;
            Debug.Log("开始加载场景");
        }

        /// <summary>
        /// 加载中
        /// </summary>
        /// <param name="progress"></param>
        protected void LoadingScene(float progress)
        {
            curGameState = EGameState.Loading;
            Debug.Log("加载场景中"+progress);
        }

        /// <summary>
        /// 加载场景结束
        /// </summary>
        protected void LoadSceneComplete(ESceneName sceneName)
        {
            curGameState = EGameState.EndLoading;
            if (sceneName == ESceneName.Menu)
            {
                GetArchitecture().GetSystem<UISupervisor>().EnterMenu();
            }
            else if(sceneName == ESceneName.GameScene)
            {
                curGameState = EGameState.Playing;
                GetArchitecture().GetSystem<UISupervisor>().EnterGame();
            }
            Debug.Log("加载场景结束");
        }


        /// <summary>
        /// 是否在游戏中
        /// </summary>
        /// <returns></returns>
        public bool IsGaming()
        {
            return curGameState == EGameState.Playing;
        }

        private void OnGUI()
        {
            // 设置自定义字体
            if (customFont != null)
            {
                GUI.skin.font = customFont;
            }
            else
            {
                GUI.skin.font = default;
            }
        }
    }
}

