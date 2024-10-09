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
    public class PlayerController : NetworkBehaviour,IController,IUnRegisterList
    {
        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();
        
        public MoveController moveController { get; set;}
        
        protected Biology biology;

        public readonly BiologyDataConfig biologyDataConfig;
        
        public Rigidbody rigidbody { get; set; }
        
        public Transform headCameraRootTransfrom { get; set; }
        
        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }

        // Start is called before the first frame update
        void Start()
        {
            InitData();
        }
        
        protected void InitData()
        {
            rigidbody = GetComponent<Rigidbody>();
            headCameraRootTransfrom = transform.Find("CameraRoot/HeadRoot");
            
            if (biologyDataConfig)
            {
                InitMovement();
                InitJump();
                InitCrouch();
                InitDash();
            }
        }

        protected void InitMovement()
        {
            if (biologyDataConfig.moveable)
            {
                moveController = new MoveController();
                moveController.InitMovement(this,biologyDataConfig.moveData);
                
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
        
        protected void InitJump()
        {
            if (biologyDataConfig.jumpable)
            {
                moveController.InitJump(biologyDataConfig.jumpData);
                this.RegisterEvent<SInputEvent_Jump>(moveData =>
                {
                    moveController.JumpCheck();
                }).AddToUnregisterList(this);
            }
        }
        
        protected void InitCrouch()
        {
            if (biologyDataConfig.crouchable)
            {
                moveController.InitCrouch(biologyDataConfig.crouchData);
                this.RegisterEvent<SInputEvent_Crouch>(moveData =>
                {
                    moveController.CrouchCheck(moveData);
                }).AddToUnregisterList(this);
            }
        }
        
        protected void InitDash()
        {
            if (biologyDataConfig.dashable)
            {
                moveController.InitDash(biologyDataConfig.dashData);
                this.RegisterEvent<SInputEvent_Dash>(moveData =>
                {
                    moveController.DashCheck();
                }).AddToUnregisterList(this);
            }
        }
    }
}


