using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using GameFrame.Net;
using QFramework;
using UnityEngine;

namespace GameFrame.World
{
    /// <summary>
    /// 怪物主类
    /// </summary>
    public class Monster : Biology
    {
        private MonsterDataConfig monsterDataConfig;

        public override void Init()
        {
            if (thisDataConfig is MonsterDataConfig)
            {
                try
                {
                    monsterDataConfig = thisDataConfig as MonsterDataConfig;
                }
                catch (Exception e)
                {
                    Debug.Log("转换失败");
                    return;
                }
            }
            else
            {
                Debug.LogError("MonsterDataConfig没有设置");
            }
            InitComponents();
        }

        protected override void InitConfig()
        {
            if (monsterDataConfig)
            {
                if (this.IsOwner || NetManager.Instance.isLocalGameMode)
                {
                    InitMovement();
                    InitJump();
                    InitCrouch();
                    InitDash();
                }
                InitHealthy();
                InitSkill();
            }
        }

        protected override void InitHealthy()
        {
            if (monsterDataConfig.healthyable)
            {
                healthyController = new HealthyController();
                healthyController.InitHealthyer(monsterDataConfig.healthyData);
            }
        }
        
        protected override void InitMovement()
        {
            if (monsterDataConfig.moveable)
            {
                moveController = new MoveController();
                moveController.InitMovement(this,monsterDataConfig.moveData);
                
                this.RegisterEvent<SInputEvent_Move>(moveData =>
                {
                    moveController.Move(moveData);
                }).AddToUnregisterList(this);
                
                this.RegisterEvent<SInputEvent_MouseDrag>(mouseData =>
                {
                    moveController.MouseRotate(mouseData);
                }).AddToUnregisterList(this);
                
                this.RegisterEvent<SInputEvent_Run>(moveData =>
                {
                    moveController.Running(moveData);
                });

                ActionKit.OnFixedUpdate.Register(()=>moveController.GroundCheck()).AddToUnregisterList(this);
            }
        }
        
        

        protected override void InitJump()
        {
            if (monsterDataConfig.jumpable)
            {
                moveController.InitJump(monsterDataConfig.jumpData);
                this.RegisterEvent<SInputEvent_Jump>(moveData =>
                {
                    moveController.JumpCheck();
                }).AddToUnregisterList(this);
            }
        }

        protected override void InitCrouch()
        {
            if (monsterDataConfig.crouchable)
            {
                moveController.InitCrouch(monsterDataConfig.crouchData);
                this.RegisterEvent<SInputEvent_Crouch>(moveData =>
                {
                    moveController.CrouchCheck(moveData);
                }).AddToUnregisterList(this);
            }
        }

        protected override void InitDash()
        {
            if (monsterDataConfig.dashable)
            {
                moveController.InitDash(monsterDataConfig.dashData);
                this.RegisterEvent<SInputEvent_Dash>(moveData =>
                {
                    moveController.DashCheck();
                }).AddToUnregisterList(this);
            }
        }

        protected override void InitSkill()
        {
            if (monsterDataConfig.skillTree)
            {
                skillController = new SkillController();
                skillController.Init(monsterDataConfig.skillTree,this);
            }
        }
    }
}

