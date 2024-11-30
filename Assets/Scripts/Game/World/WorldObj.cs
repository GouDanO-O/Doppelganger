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
    /// 世界物体基类--主要负责集中管理该物体
    /// </summary>
    public abstract class WorldObj : MonoNetController
    { 
        public WorldObjDataConfig thisDataConfig;

        public WorldObjPropertyData_Temporality worldObjPropertyDataTemporality;
        
        public BaseController thisController { get;protected set; }
        
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
                DeInitData();
            }

            InitComponents();
            isInit = true;
        }

        /// <summary>
        /// 注销
        /// </summary>
        public override void DeInitData()
        {

        }

        /// <summary>
        /// 初始化组件
        /// </summary>
        protected virtual void InitComponents()
        {
            worldObjPropertyDataTemporality=new WorldObjPropertyData_Temporality();
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
                    thisController = playerController;
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
                    thisController = aiController;
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

        #region

        public void Death(bool isDeath)
        {
            thisController.Death(isDeath);
        }

        public void BeHarmed(float harmedValue,WorldObj trigger,EElementType elementType=EElementType.None)
        {
            thisController.BeHarmed(harmedValue,trigger,elementType);
        }

        public void DoPlayAnimations(SAnimatorEvent animatorEvent)
        {
            thisController.DoPlayAnimations(animatorEvent);
        }

        #endregion

        public bool IsPlayer()
        {
            return isPlayerSelecting;
        }
    }
}


