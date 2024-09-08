using GameFrame.UI;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame
{

    /// <summary>
    /// 全局管理器(用来注册和管理System,Model和Utility
    /// </summary>
    public class Main : Architecture<Main>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        protected override void Init()
        {
            Regiest_System();
            Regiest_Model();
            Regiest_Utility();
            Regiest_Event();

            UIRoot.Instance.OnSingletonInit();
        }


        /// <summary>
        /// 注册System
        /// </summary>
        protected void Regiest_System()
        {
            this.RegisterSystem(new ResourcesManager());
            this.RegisterSystem(new SceneLoader());
            this.RegisterSystem(new UISupervisor());
        }

        /// <summary>
        /// 注册Model
        /// </summary>
        protected void Regiest_Model()
        {
            this.RegisterModel(new ResourceData());
        }

        /// <summary>
        /// 注册Utility
        /// </summary>
        protected void Regiest_Utility()
        {
            this.RegisterUtility(new ResoucesUtility());
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        protected void Regiest_Event()
        {
            
        }

        /// <summary>
        /// 注销事件
        /// </summary>
        protected void UnRegiest_Event()
        {

        }
    }
}

