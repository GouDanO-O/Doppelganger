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
        
        /// <summary>
        /// 注册数据
        /// </summary>
        /// <param name="owner"></param>
        public override void InitData(WorldObj owner)
        {
            base.InitData(owner);
            this.thisDataConfig=owner.thisDataConfig;
            InitMovement();
            InitHealthy();
            InitSkill();
        }

        /// <summary>
        /// 注销数据
        /// </summary>
        public virtual void DeInitData()
        {
            this.UnRegisterAll();
            healthyController.DeInitData();
            skillController.DeInitData();
            attackController.DeInitData();
        }

        /// <summary>
        /// 开始逻辑循环
        /// </summary>
        public abstract void EnableLogic();
        
        /// <summary>
        /// 取消逻辑循环
        /// </summary>
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
        
        /// <summary>
        /// 初始化移动
        /// </summary>
        protected abstract void InitMove();

        /// <summary>
        /// 初始化跳跃
        /// </summary>
        protected abstract void InitJump();

        /// <summary>
        /// 初始化蹲伏
        /// </summary>
        protected abstract void InitCrouch();
        
        /// <summary>
        /// 初始化冲刺
        /// </summary>
        protected abstract void InitDash();
    }
}

