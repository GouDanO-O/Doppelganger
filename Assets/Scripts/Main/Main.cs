using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
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
            RegiestSystem();
            RegistModel();
            RegistEvent();

            UIRoot.Instance.OnSingletonInit();
        }


        /// <summary>
        /// 注册System
        /// </summary>
        protected void RegiestSystem()
        {
            
        }

        /// <summary>
        /// 注册model
        /// </summary>
        protected void RegistModel()
        {

        }

        /// <summary>
        /// 注册事件
        /// </summary>
        protected void RegistEvent()
        {
            
        }

        /// <summary>
        /// 注销事件
        /// </summary>
        protected void UnRegistEvent()
        {

        }
    }
}

