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

namespace GameFrame.World
{
    
    public abstract class WorldObj : NetworkBehaviour,IController,IUnRegisterList
    {
        public WorldObjDataConfig thisDataConfig;
        
        public HealthyController healthyController { get;protected set; }
        
        public Rigidbody rigidbody { get; set; }
        
        public Transform headCameraRootTransfrom { get; set; }

        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();
        
        public SkillController skillController { get;protected set; }
        
        private void Awake()
        {
            Init();
        }

        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {
            if (thisDataConfig is WorldObjDataConfig)
            {
                InitComponents();
            }
            else
            {
                Debug.LogError("WorldObjDataConfig is not set!");
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        public virtual void DeInit()
        {
            this.UnRegisterAll();
        }

        /// <summary>
        /// 初始化组件
        /// </summary>
        protected virtual void InitComponents()
        {
            rigidbody = GetComponent<Rigidbody>();
            headCameraRootTransfrom = transform.Find("CameraRoot/HeadRoot");
            InitConfig();
        }

        /// <summary>
        /// 初始化配置
        /// </summary>
        protected virtual void InitConfig()
        {
            if (thisDataConfig)
            {
                InitHealthy();
                InitSkill();
            }
        }

        /// <summary>
        /// 初始化生命
        /// </summary>
        protected virtual void InitHealthy()
        {
            if (thisDataConfig.healthyable)
            {
                healthyController = new HealthyController();
                healthyController.InitHealthyer(thisDataConfig.healthyData);
            }
        }

        /// <summary>
        /// 初始化技能
        /// </summary>
        protected virtual void InitSkill()
        {
            if (thisDataConfig.skillTree)
            {
                skillController=gameObject.AddComponent<SkillController>();
                skillController.Init(thisDataConfig.skillTree);
            }
        }
        
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
    }
}


