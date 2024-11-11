using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using UnityEngine;
using GameFrame.Net;
using UnityEngine.Scripting;
using Unity.Netcode;
using QFramework;
using Unity.Netcode.Components;
using UnityEngine.Events;

namespace GameFrame.World
{
    /// <summary>
    /// 持久化的世界物体数据--生命周期内会一直存在
    /// </summary>
    public class SWorldObjData_Persistence : PersistentData
    {
        
        public override void SaveData()
        {
            
        }
    }

    /// <summary>
    /// 临时性的世界物体数据--当变形或其他操作时会被初始化
    /// </summary>
    public class SWorldObjData_Temporality : TemporalityData_Pool
    {
        public static SWorldObjData_Temporality Allocate()
        {
            return SafeObjectPool<SWorldObjData_Temporality>.Instance.Allocate();
        }
        
        public override void OnRecycled()
        {
            
        }

        public override void Recycle2Cache()
        {
            
        }
    }

    /// <summary>
    /// 世界物体基类--主要负责集中管理该物体
    /// </summary>
    public abstract class WorldObj : BasicNetController, IUnRegisterList
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
        
        public EWorldObjCollisionType CollisionType { get; set; }
        
        public Rigidbody rigidbody { get; set; }
        
        public Collider collider { get; set; }

        private void Start()
        {
            ChangePlayerSelecting(true);
        }

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
            worldObjData_Persistence.SaveData();
            worldObjData_Temporality.Recycle2Cache();
        }

        /// <summary>
        /// 初始化组件
        /// </summary>
        protected virtual void InitComponents()
        {
            worldObjData_Persistence = new SWorldObjData_Persistence();
            worldObjData_Temporality = new SWorldObjData_Temporality();
            rigidbody = GetComponent<Rigidbody>();
            collider = GetComponent<Collider>();
            
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

        #region 碰撞检测
        
        /// <summary>
        /// 开始碰撞
        /// </summary>
        /// <param name="other"></param>
        protected virtual void CollisionEnter(Collision other)
        {
            
        }
        
        /// <summary>
        /// 碰撞中
        /// </summary>
        /// <param name="other"></param>
        protected virtual void CollisionStay(Collision other)
        {
            
        }
        
        /// <summary>
        /// 碰撞结束
        /// </summary>
        /// <param name="other"></param>
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


