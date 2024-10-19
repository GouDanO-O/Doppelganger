using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using UnityEngine;
using GameFrame.Net;
using GameFrame.Word;
using UnityEngine.Scripting;
using Unity.Netcode;
using QFramework;
using Unity.Netcode.Components;
using Unity.VisualScripting;
using UnityEngine.Events;

namespace GameFrame.World
{
    /// <summary>
    /// 持久化的数据--生命周期内会一直存在
    /// </summary>
    public interface IData_Persistence
    {
        public void ClearData();
        
        public void SaveData();
    }
    
    /// <summary>
    /// 临时性的数据
    /// </summary>
    public interface IData_Temporality
    {
        public void ClearData();
    }
    
    /// <summary>
    /// 持久化的世界物体数据--生命周期内会一直存在
    /// </summary>
    public class SWorldObjData_Persistence : IData_Persistence
    {
        public void ClearData()
        {
            SaveData();
        }

        public void SaveData()
        {
            
        }
    }

    /// <summary>
    /// 临时性的世界物体数据--当变形或其他操作时会被初始化
    /// </summary>
    public class SWorldObjData_Temporality: IData_Temporality
    {
        public void ClearData()
        {
            
        }
    }


    public abstract class WorldObj : NetworkBehaviour,IController, IUnRegisterList
    { 
        public WorldObjDataConfig thisDataConfig;

        public SWorldObjData_Persistence worldObjData_Persistence;

        public SWorldObjData_Temporality worldObjData_Temporality;

        /// <summary>
        /// 注册的事件列表
        /// </summary>
        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();

        public PlayerController playerController { get; protected set; }

        public AIController aiController { get; protected set; }

        protected bool isInit = false;

        public bool isPlayerSelecting { get; set; }

        /// <summary>
        /// 是否由玩家进行操作
        /// </summary>
        /// <param name="isPlayerSelecting"></param>
        public void ChangePlayerSelecting(bool isPlayerSelecting)
        {
            this.isPlayerSelecting = isPlayerSelecting;
            this.Init();
        }

        #region Init

        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {
            if (isInit)
            {
                DeInit();
            }

            InitComponents();
            isInit = true;
        }

        /// <summary>
        /// 注销
        /// </summary>
        public virtual void DeInit()
        {
            this.UnRegisterAll();
            worldObjData_Persistence.ClearData();
            worldObjData_Temporality.ClearData();
        }

        /// <summary>
        /// 初始化组件
        /// </summary>
        protected virtual void InitComponents()
        {
            worldObjData_Persistence = new SWorldObjData_Persistence();
            worldObjData_Temporality = new SWorldObjData_Temporality();
            
            if (thisDataConfig)
            {
                InitController();
            }
        }

        /// <summary>
        /// 初始化控制器
        /// </summary>
        protected virtual void InitController()
        {
            if (isPlayerSelecting)
            {
                if (playerController)
                {
                    playerController.EnableLogic();
                }
                else
                {
                    playerController = gameObject.AddComponent<PlayerController>();
                    playerController.InitData(this);
                }

                if (aiController)
                {
                    aiController.DisableLogic();
                }
            }
            else
            {
                if (aiController)
                {
                    aiController.EnableLogic();
                }
                else
                {
                    aiController = gameObject.AddComponent<AIController>();
                    aiController.InitData(this);
                }

                if (playerController)
                {
                    playerController.DisableLogic();
                }
            }
        }

        #endregion

        #region Player

        /// <summary>
        /// 短tick逻辑--Player
        /// </summary>
        public virtual void ShortTickLogic_Player()
        {
            
        }
        
        /// <summary>
        /// 正常tick逻辑--Player
        /// </summary>
        public virtual void MainLogic_Player()
        {
            
        }
        
        /// <summary>
        /// 长tick逻辑--Player
        /// </summary>
        public virtual void LongTickLogic_Player()
        {
            
        }

        #endregion

        #region AI
        
        /// <summary>
        /// 短tick逻辑--AI
        /// </summary>
        public virtual void ShortTickLogic_AI()
        {
            
        }


        
        /// <summary>
        /// 正常tick逻辑--AI
        /// </summary>
        public virtual void MainLogic_AI()
        {
            
        }


        
        /// <summary>
        /// 长tick逻辑--AI
        /// </summary>
        public virtual void LongTickLogic_AI()
        {
            
        }
        
        #endregion

        /// <summary>
        /// 获取控制器
        /// </summary>
        /// <returns></returns>
        public virtual BaseController  GetController()
        {
            if (isPlayerSelecting)
            {
                return playerController;
            }

            return aiController;
        }        
        #region CollisionCheck
        protected virtual void CollisionEnter(Collision other)
        {
            
        }
        
        protected virtual void CollisionStay(Collision other)
        {
            
        }
        
        protected virtual void CollisionExit(Collision other)
        {
            
        }
        
        protected void OnCollisionEnter(Collision other)
        {
            CollisionEnter(other);
        }

        protected void OnCollisionStay(Collision other)
        {
            CollisionStay(other);
        }
        
        protected void OnCollisionExit(Collision other)
        {
            CollisionExit(other);
        }
        
        #endregion
    }
}


