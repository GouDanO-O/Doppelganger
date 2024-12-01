using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using GameFrame.World;
using QFramework;
using Unity.Netcode;
using UnityEngine;

namespace GameFrame.World
{
    /// <summary>
    /// 客户端玩家控制器，用来管理玩家
    /// </summary>
    public class PlayerController : BaseController
    {
        public WorldObjDataConfig willDeformationConfig
        {
            get
            {
                if (owner == null)
                    return null;
                return owner.thisDataConfig;
            }
        }

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
            headCameraRootTransfrom = transform.Find("CameraRoot/HeadRoot");
            WorldManager.Instance.SetPlayer(this);
            base.InitData(worldObj);
        }

        protected override void InitMove()
        {
            if (thisDataConfig.Moveable)
            {
                moveController = new MoveController_Player();
                moveController.InitData(owner);
                moveController.InitPlayerController(this);

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

        protected override void InitJump()
        {
            if (thisDataConfig.Jumpable)
            {
                moveController.InitJump(thisDataConfig.JumpData);
                this.RegisterEvent<SInputEvent_Jump>(moveData => { moveController.JumpCheck(); })
                    .AddToUnregisterList(this);
            }
        }

        protected override void InitCrouch()
        {
            if (thisDataConfig.Crouchable)
            {
                moveController.InitCrouch(thisDataConfig.CrouchData);
                this.RegisterEvent<SInputEvent_Crouch>(moveData => { moveController.CrouchCheck(moveData); })
                    .AddToUnregisterList(this);
            }
        }

        protected override void InitDash()
        {
            if (thisDataConfig.Dashable)
            {
                moveController.InitDash(thisDataConfig.DashData);
                this.RegisterEvent<SInputEvent_Dash>(moveData => { moveController.DashCheck(); })
                    .AddToUnregisterList(this);
            }
        }
    }
}