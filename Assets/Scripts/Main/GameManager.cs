using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FrameWork
{
    public enum EGameState : byte
    {
        None,
        StartLoading,
        Loading,
        EndLoading,
        Menu,
        Gaming,
    }

    public class GameManager : MonoSingleton<GameManager>,IController
    {
        protected SceneLoader sceneLoader;

        protected EGameState curGameState=EGameState.None;

        private ResourcesManager resourcesManager;

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
        }

        private void Start()
        {
            LoadData();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            resourcesManager.LoadData();
        }

        private void LoadComplete()
        {
            EnterMenu();
        }

        /// <summary>
        /// 进入菜单
        /// </summary>
        protected void EnterMenu()
        {
            sceneLoader.LoadSceneAsync(ESceneName.Menu);
        }

        /// <summary>
        /// 开始游戏
        /// </summary>
        public void StartGame()
        {
            sceneLoader.LoadSceneAsync(ESceneName.GameScene);
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
                curGameState = EGameState.Menu;
            }
            else if(sceneName == ESceneName.GameScene)
            {
                curGameState = EGameState.Gaming;
            }

            Debug.Log("加载场景结束");
        }


        /// <summary>
        /// 是否在游戏中
        /// </summary>
        /// <returns></returns>
        public bool IsGaming()
        {
            return curGameState == EGameState.Gaming;
        }
    }
}

