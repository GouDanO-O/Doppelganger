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

        public WorldObjDataConfig willDeformationConfig { get; }

        public Rigidbody rigidbody { get; set; }

        public Transform headCameraRootTransfrom { get; set; }

        public MoveController_Player moveController { get; set; }

        public override void EnableLogic()
        {
            
        }

        public override void DisableLogic()
        {
            
        }

        public override void InitData(WorldObj worldObj)
        {
            rigidbody = GetComponent<Rigidbody>();
            headCameraRootTransfrom = transform.Find("CameraRoot/HeadRoot");
            base.InitData(worldObj);
        }
        
        protected override void InitMove()
        {
            if (thisDataConfig.moveable)
            {
                moveController = new MoveController_Player();
                moveController.InitData(owner);

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
            if (thisDataConfig.jumpable)
            {
                moveController.InitJump(thisDataConfig.jumpData);
                this.RegisterEvent<SInputEvent_Jump>(moveData => { moveController.JumpCheck(); })
                    .AddToUnregisterList(this);
            }
        }

        protected override void InitCrouch()
        {
            if (thisDataConfig.crouchable)
            {
                moveController.InitCrouch(thisDataConfig.crouchData);
                this.RegisterEvent<SInputEvent_Crouch>(moveData => { moveController.CrouchCheck(moveData); })
                    .AddToUnregisterList(this);
            }
        }

        protected override void InitDash()
        {
            if (thisDataConfig.dashable)
            {
                moveController.InitDash(thisDataConfig.dashData);
                this.RegisterEvent<SInputEvent_Dash>(moveData => { moveController.DashCheck(); })
                    .AddToUnregisterList(this);
            }
        }
    }
}