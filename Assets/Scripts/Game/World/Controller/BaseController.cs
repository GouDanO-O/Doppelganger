using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using QFramework;
using Unity.Netcode;
using UnityEngine;

namespace GameFrame.World
{
    /// <summary>
    /// 基础物体管理器
    /// </summary>
    public abstract class BaseController : MonoNetController,IUnRegisterList
    {
        public EasyEvent<bool> onDeathEvent;
        
        public EasyEvent<float> onBeHarmedEvent;
        
        public WorldObjDataConfig thisDataConfig { get; protected set; }
        
        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();
        
        public HealthyController healthyController { get; protected set; }
        
        public SkillController skillController { get; set;}
        
        public AttackController attackController { get; set; }
        
        public AnimatorController animatorController { get; set; }
        
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
            InitAnimator();
        }

        /// <summary>
        /// 注销数据
        /// </summary>
        public override void DeInitData()
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
        /// 初始化动画
        /// </summary>
        protected virtual void InitAnimator()
        {
            animatorController = new AnimatorController();
            animatorController.InitData(owner);
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
        
        /// <summary>
        /// 短tick逻辑
        /// </summary>
        public virtual void ShortTickLogic()
        {
            
        }
        
        /// <summary>
        /// 正常tick逻辑
        /// </summary>
        public virtual void MainLogic()
        {
            
        }
        
        /// <summary>
        /// 长tick逻辑
        /// </summary>
        public virtual void LongTickLogic()
        {
            
        }

        public virtual void Death(bool isDeath)
        {
            onDeathEvent?.Trigger(isDeath);
        }

        public virtual void BeHarmed(float harmedValue)
        {
            onBeHarmedEvent?.Trigger(harmedValue);
        }

        public virtual void DoPlayAnimations(SAnimatorEvent animatorEvent)
        {
            animatorController.onPlayAnimationEvent.Invoke(animatorEvent);
        }
    }
}

