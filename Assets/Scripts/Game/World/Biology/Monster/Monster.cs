using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
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
        
        public override void InitData()
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

        protected override void InitComponents()
        {
            base.InitComponents();
            if (monsterDataConfig)
            {
                InitMovement();
                InitJump();
                InitCrouch();
                InitDash();
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
                });
                this.RegisterEvent<SInputEvent_MouseDrag>(mouseData =>
                {
                    moveController.MouseRotate(mouseData);
                });
            }
        }

        protected override void InitJump()
        {
            if (monsterDataConfig.jumpable)
            {
                moveController.CanJump(monsterDataConfig.jumpData);
                this.RegisterEvent<SInputEvent_Jump>(moveData =>
                {
                    moveController.Jump();
                });
            }
        }

        protected override void InitCrouch()
        {
            if (monsterDataConfig.crouchable)
            {
                moveController.CanCrouch(monsterDataConfig.crouchData);
                this.RegisterEvent<SInputEvent_Crouch>(moveData =>
                {
                    moveController.Crouch();
                });
            }
        }

        protected override void InitDash()
        {
            if (monsterDataConfig.dashable)
            {
                moveController.CanDash(monsterDataConfig.dashData);
                this.RegisterEvent<SInputEvent_Dash>(moveData =>
                {
                    moveController.Dash();
                });
            }
        }
    }
}

