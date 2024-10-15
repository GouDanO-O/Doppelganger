using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using QFramework;
using Unity.Netcode;
using UnityEngine;

namespace GameFrame.World
{
    public abstract class BasicController : IController
    {
        public WorldObj owner;

        public virtual void InitData(WorldObj owner)
        {
            this.owner = owner;
        }

        public abstract void DeInitData();
        
        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }
    }

    public abstract class BasicNetController : NetworkBehaviour, IController
    {
        public WorldObj owner;
        
        public virtual void InitData(WorldObj owner)
        {
            this.owner = owner;
        }
        
        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }
    }
    
    /// <summary>
    /// 基础物体管理器
    /// </summary>
    public abstract class BaseController : BasicNetController,IUnRegisterList
    {
        public WorldObjDataConfig thisDataConfig { get; protected set; }
        
        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();
        
        public HealthyController healthyController { get; protected set; }
        
        public SkillController skillController { get; set;}
        
        public AttackController attackController { get; set; }
        
        public override void InitData(WorldObj owner)
        {
            base.InitData(owner);
            this.thisDataConfig=owner.thisDataConfig;
            InitMovement();
            InitHealthy();
            InitSkill();
        }

        public virtual void DeInitData()
        {
            healthyController.DeInitData();
            skillController.DeInitData();
            attackController.DeInitData();
        }

        public abstract void EnableLogic();

        public abstract void DisableLogic();

        /// <summary>
        /// 初始化移动
        /// </summary>
        protected virtual void InitMovement()
        {
            InitMove();
            InitJump();
            InitCrouch();
            InitDash();
        }
        
        /// <summary>
        /// 初始化生命
        /// </summary>
        protected virtual void InitHealthy()
        {
            if (thisDataConfig.healthyable)
            {
                healthyController = new HealthyController();
                healthyController.InitData(owner);
            }
        }

        /// <summary>
        /// 初始化技能
        /// </summary>
        protected virtual void InitSkill()
        {
            if (thisDataConfig.skillTree)
            {
                
                skillController = new SkillController();
                skillController.InitData(owner);
            }
        }
        
        protected abstract void InitMove();

        protected abstract void InitJump();

        protected abstract void InitCrouch();
        
        protected abstract void InitDash();
    }
}

