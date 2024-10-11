using System.Collections;
using System.Collections.Generic;
using QFramework;
using Unity.Netcode;
using UnityEngine;

namespace GameFrame.World
{
    /// <summary>
    /// 基础物体管理器
    /// </summary>
    public abstract class BaseController : NetworkBehaviour,IController,IUnRegisterList
    {
        public WorldObj WorldObj { get; set; }
        
        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();
        
        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }

        public abstract void EnableLogic();

        public abstract void DisableLogic();
        
        public abstract void InitData();

        protected abstract void InitMovement();

        protected abstract void InitMove();

        protected abstract void InitJump();

        protected abstract void InitCrouch();
        
        protected abstract void InitDash();
    }
}

