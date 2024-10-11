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
using UnityEngine.Events;

namespace GameFrame.World
{
    /// <summary>
    /// 持久化的数据--生命周期内会一直存在
    /// </summary>
    public interface IData_Persistence
    {
        public void ClearData();
    }
    
    /// <summary>
    /// 临时性的数据
    /// </summary>
    public interface IData_Temporality
    {
        public void InitData();
        
        public void ClearData();
    }
    
    /// <summary>
    /// 持久化的世界物体数据--生命周期内会一直存在
    /// </summary>
    public class SWorldObjData_Persistence : IData_Persistence
    {
        public void ClearData()
        {
            
        }
    }

    /// <summary>
    /// 临时性的世界物体数据--当变形或其他操作时会被初始化
    /// </summary>
    public class SWorldObjData_Temporality: IData_Temporality
    {
        public void InitData()
        {
            
        }

        public void ClearData()
        {
            
        }
    }
    
    
    public abstract class WorldObj : NetworkBehaviour,IController,IUnRegisterList
    {
        [SerializeField] protected WorldObjDataConfig thisDataConfig;

        /// <summary>
        /// 注册的事件列表
        /// </summary>
        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();
        
        public HealthyController healthyController { get;protected set; }
        
        public SkillController skillController { get;protected set; }

        public PlayerController playerController { get;protected set; }
        
        public AIController aiController { get;protected set; }
        
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
        }

        /// <summary>
        /// 初始化组件
        /// </summary>
        protected virtual void InitComponents()
        {
            InitController();
            if (thisDataConfig)
            {
                InitMovement();
                InitHealthy();
                InitSkill();
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
                    playerController=gameObject.AddComponent<PlayerController>();
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
                    aiController=gameObject.AddComponent<AIController>();
                }

                if (playerController)
                {
                    playerController.DisableLogic();
                }
            }
        }
        
        /// <summary>
        /// 初始化移动
        /// </summary>
        protected virtual void InitMovement()
        {
            if (isPlayerSelecting && playerController)
            {
                playerController.InitData();
            }
            else if(aiController)
            {
                aiController.InitData();
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
                skillController=new SkillController();
                skillController.Init(thisDataConfig.skillTree,this);
            }
        }
        
        #endregion
        
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


