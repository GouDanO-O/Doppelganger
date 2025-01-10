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
    public abstract class WorldObj : MonoNetController ,IUnRegisterList
    { 
        public WorldObjData_Config thisDataConfig;

        public WorldObjPropertyData_Temporality worldObjPropertyDataTemporality { get;private set; }

        public UnityAction<bool> onChangePlayerControllingEvent;
        
        public UnityAction<bool> onDeathEvent;
        
        public UnityAction<TriggerDamageData_TemporalityPoolable> onBeHarmedEvent;
        
        public UnityAction<bool> onChangeInvincibleModEvent;
        
        public UnityAction<SPlayAnimationEvent> onPlayAnimationEvent;
        
        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();
        
        public HealthyController healthyController { get; protected set; }
        
        public SkillController skillController { get;protected set;}
        
        public AnimatorController animatorController { get;protected set; }
        
        public MoveController moveController { get;protected set; }
        
        protected bool isInit = false;

        public bool isControlledByPlayer { get; set; }
        
        public EWorldObjCollisionType CollisionType { get; set; }
        
        public Rigidbody rigidbody { get; set; }
        
        public Collider collider { get; set; }
        
        public Transform footRoot;

        private void Start()
        {
            InitData();
        }

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void InitData()
        {
            if (isInit)
            {
                DeInitData();
            }

            InitComponents();
            RegistEvent();
            isInit = true;
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        protected virtual void RegistEvent()
        {
            this.onChangePlayerControllingEvent += ChangePlayerControlling;
        } 
        
        /// <summary>
        /// 注销
        /// </summary>
        public override void DeInitData()
        {
            this.UnRegisterAll();
            UnRegistEvent();
            healthyController.DeInitData();
            skillController.DeInitData();
        }

        /// <summary>
        /// 注销事件
        /// </summary>
        protected virtual void UnRegistEvent()
        {
            this.onChangePlayerControllingEvent -= ChangePlayerControlling;
        }
        
        /// <summary>
        /// 初始化组件
        /// </summary>
        protected virtual void InitComponents()
        {
            worldObjPropertyDataTemporality=new WorldObjPropertyData_Temporality();
            rigidbody = GetComponent<Rigidbody>();
            collider = GetComponent<Collider>();
            footRoot = transform.Find("FootRoot");
            
            InitGeneralController();
            if (isControlledByPlayer)
            {
                InitPlayer();
            }
            else
            {
                InitAI();
            }
        }

        /// <summary>
        /// 初始化通用控制器
        /// </summary>
        protected virtual void InitGeneralController()
        {
            InitHealthy();
            InitAnimator();
        }
        
        /// <summary>
        /// 初始化生命
        /// </summary>
        protected virtual void InitHealthy()
        {
            if (thisDataConfig.Healthyable)
            {
                healthyController = new HealthyController();
                healthyController.InitData(this);
            }
        }
        
        /// <summary>
        /// 初始化动画
        /// </summary>
        protected virtual void InitAnimator()
        {
            animatorController = new AnimatorController();
            animatorController.InitData(this);
        }
        #region 初始化玩家(每次有玩家接管都会初始化一遍)

        /// <summary>
        /// 初始化玩家
        /// </summary>
        protected virtual void InitPlayer()
        {
            InitMovement_Player();
            InitSkill_Player();
        }
        
        /// <summary>
        /// 初始化移动
        /// </summary>
        protected virtual void InitMovement_Player()
        {
            InitMove_Player();
            InitJump_Player();
            InitCrouch_Player();
            InitDash_Player();
        }

        /// <summary>
        /// 初始化技能
        /// </summary>
        protected virtual void InitSkill_Player()
        {
            if (thisDataConfig.HasSkill)
            {
                skillController = new SkillController();
                skillController.InitData(this);
            }
        }
        
        protected virtual void InitMove_Player()
        {
            if (thisDataConfig.Moveable)
            {
                moveController = new MoveController_Player();
                moveController.InitData(this);

                this.RegisterEvent<SInputEvent_Move>(moveData => { moveController.Move(moveData); })
                    .AddToUnregisterList(this);

                this.RegisterEvent<SInputEvent_MouseDrag>(mouseData => { moveController.MouseRotate(mouseData); })
                    .AddToUnregisterList(this);

                this.RegisterEvent<SInputEvent_Run>(moveData => { moveController.Running(moveData); })
                    .AddToUnregisterList(this);

                ActionKit.OnUpdate.Register(() => moveController.GroundCheck())
                    .AddToUnregisterList(this);
            }
        }

        /// <summary>
        /// 初始化跳跃
        /// </summary>
        protected virtual void InitJump_Player()
        {
            if (thisDataConfig.Jumpable)
            {
                moveController.InitJump(thisDataConfig.JumpData);
                this.RegisterEvent<SInputEvent_Jump>(moveData => { moveController.JumpCheck(); })
                    .AddToUnregisterList(this);
            }
        }

        /// <summary>
        /// 初始化蹲伏
        /// </summary>
        protected virtual void InitCrouch_Player()
        {
            if (thisDataConfig.Crouchable)
            {
                moveController.InitCrouch(thisDataConfig.CrouchData);
                this.RegisterEvent<SInputEvent_Crouch>(moveData => { moveController.CrouchCheck(moveData); })
                    .AddToUnregisterList(this);
            }
        }

        /// <summary>
        /// 初始化冲刺
        /// </summary>
        protected virtual void InitDash_Player()
        {
            if (thisDataConfig.Dashable)
            {
                moveController.InitDash(thisDataConfig.DashData);
                this.RegisterEvent<SInputEvent_Dash>(moveData => { moveController.DashCheck(); })
                    .AddToUnregisterList(this);
            }
        }
        #endregion

        #region 初始化AI(非玩家控制)
        
        protected virtual void InitAI()
        {
            InitMovement_AI();
            InitSkill_AI();
        }

        protected virtual void InitMovement_AI()
        {
            InitMove_AI();
            InitJump_AI();
            InitCrouch_AI();
            InitDash_AI();
        }

        protected virtual void InitSkill_AI()
        {
            
        }
        
        protected virtual void InitMove_AI()
        {
            if (thisDataConfig.Moveable)
            {
                moveController = new MoveController_AI();
                moveController.InitData(this);
            }
        }

        /// <summary>
        /// 初始化跳跃
        /// </summary>
        protected virtual void InitJump_AI()
        {
            if (thisDataConfig.Jumpable)
            {
                moveController.InitJump(thisDataConfig.JumpData);
            }
        }

        /// <summary>
        /// 初始化蹲伏
        /// </summary>
        protected virtual void InitCrouch_AI()
        {
            if (thisDataConfig.Crouchable)
            {
                moveController.InitCrouch(thisDataConfig.CrouchData);
            }
        }

        /// <summary>
        /// 初始化冲刺
        /// </summary>
        protected virtual void InitDash_AI()
        {
            if (thisDataConfig.Dashable)
            {
                moveController.InitDash(thisDataConfig.DashData);
            }
        }

        #endregion

        
        
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

        #region 事件和传参
        
        /// <summary>
        /// 是否由玩家进行操作
        /// </summary>
        /// <param name="isPlayerSelecting"></param>
        public virtual void ChangePlayerControlling(bool isControlledByPlayer)
        {
            this.isControlledByPlayer = isControlledByPlayer;
            if (isControlledByPlayer)
            {
                PlayerControlling();
            }
            else
            {
                AIControlling();
            }
        }

        /// <summary>
        /// 死亡
        /// </summary>
        /// <param name="isDeath"></param>
        public virtual void Death(bool isDeath)
        {
            onDeathEvent.Invoke(isDeath);
        }

        /// <summary>
        /// 受到伤害
        /// </summary>
        /// <param name="damageData"></param>
        public virtual void BeHarmed(TriggerDamageData_TemporalityPoolable damageData)
        {
            onBeHarmedEvent.Invoke(damageData);
        }

        /// <summary>
        /// 播放动画
        /// </summary>
        /// <param name="animatorEvent"></param>
        public virtual void DoPlayAnimations(SPlayAnimationEvent animatorEvent)
        {
            onPlayAnimationEvent.Invoke(animatorEvent);
        }

        /// <summary>
        /// 改变无敌状态
        /// </summary>
        /// <param name="isInvincible"></param>
        public virtual void ChangeInvincibleMod(bool isInvincible)
        {
            onChangeInvincibleModEvent.Invoke(isInvincible);
        }

        #endregion

        #region Tick检测

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

        #endregion

        #region 其他

        /// <summary>
        /// 由玩家接管
        /// </summary>
        protected virtual void PlayerControlling()
        {
            
        }

        /// <summary>
        /// 由AI接管
        /// </summary>
        protected virtual void AIControlling()
        {
            
        }
        

        #endregion
    }
}


