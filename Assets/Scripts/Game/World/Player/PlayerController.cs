using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using GameFrame.World;
using QFramework;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace GameFrame.Word
{
    /// <summary>
    /// 客户端玩家控制器，用来管理玩家
    /// </summary>
    public class PlayerController : BaseController
    {
        public WorldObj worldObj;

        public readonly WorldObjDataConfig worldObjDataConfig;

        public readonly WorldObjDataConfig willDeformationConfig;

        public Rigidbody rigidbody { get; set; }

        public Transform headCameraRootTransfrom { get; set; }

        public MoveController_Player moveController { get; set; }

        public override void EnableLogic()
        {
            
        }

        public override void DisableLogic()
        {
            
        }

        public override void InitData()
        {
            rigidbody = GetComponent<Rigidbody>();
            headCameraRootTransfrom = transform.Find("CameraRoot/HeadRoot");
            InitMovement();
        }
        
        protected override void InitMovement()
        {
            InitMove();
            InitJump();
            InitCrouch();
            InitDash();
        }


        protected override void InitMove()
        {
            if (worldObjDataConfig.moveable)
            {
                moveController = new MoveController_Player();
                moveController.InitMovement(worldObjDataConfig.moveData);

                this.RegisterEvent<SInputEvent_Move>(moveData => { moveController.Move(moveData); })
                    .AddToUnregisterList(this);

                this.RegisterEvent<SInputEvent_MouseDrag>(mouseData => { moveController.MouseRotate(mouseData); })
                    .AddToUnregisterList(this);

                this.RegisterEvent<SInputEvent_Run>(moveData => { moveController.Running(moveData); });

                ActionKit.OnFixedUpdate.Register(() => moveController.GroundCheck()).AddToUnregisterList(this);
            }
        }

        protected override void InitJump()
        {
            if (worldObjDataConfig.jumpable)
            {
                moveController.InitJump(worldObjDataConfig.jumpData);
                this.RegisterEvent<SInputEvent_Jump>(moveData => { moveController.JumpCheck(); })
                    .AddToUnregisterList(this);
            }
        }

        protected override void InitCrouch()
        {
            if (worldObjDataConfig.crouchable)
            {
                moveController.InitCrouch(worldObjDataConfig.crouchData);
                this.RegisterEvent<SInputEvent_Crouch>(moveData => { moveController.CrouchCheck(moveData); })
                    .AddToUnregisterList(this);
            }
        }

        protected override void InitDash()
        {
            if (worldObjDataConfig.dashable)
            {
                moveController.InitDash(worldObjDataConfig.dashData);
                this.RegisterEvent<SInputEvent_Dash>(moveData => { moveController.DashCheck(); })
                    .AddToUnregisterList(this);
            }
        }
    }
}