using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameFrame.Config;
using QFramework;
using Unity.Netcode.Components;
using UnityEngine;

namespace GameFrame.World
{
    public class MoveController_Player :  MoveController
    {
        protected PlayerController owner;
        
        protected Transform cameraTransform;

        protected Transform headCameraRootTransfrom;
        
        public float mouseSensitivity { get; set; }

        public Vector2 maxPitchAngle { get; set; }
        
        protected float xRotation;
        
        /// <summary>
        /// 是否处于自由相机模式
        /// </summary>
        public bool isInFreeCameraMod
        {
            get
            {
                return true;
            }
        }
        
        /// <summary>
        /// 初始化拥有者
        /// </summary>
        /// <param name="owner"></param>
        public void InitOwner(PlayerController owner)
        {
            this.rigidbody = owner.rigidbody;
            this.transfrom = owner.transform;
            this.headCameraRootTransfrom = owner.headCameraRootTransfrom;
        }

        /// <summary>
        /// 初始化移动
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="moveData"></param>
        public override void InitData(WorldObj owner)
        {
            base.InitData(owner);
            SMoveData moveData = owner.thisDataConfig.moveData;
            this.maxPitchAngle = moveData.maxPitchAngle;
            
            ResourcesModel resModel = owner.GetModel<ResourcesModel>();
            this.mouseSensitivity = resModel.SettingConfig.MouseSensitivity;
            this.cameraTransform = CameraController.Instance.virtualCamera.transform;
        }
        
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="inputEvent_Move"></param>
        public void Move(SInputEvent_Move inputEvent_Move)
        {
            temSpeed = GetTemSpeed();
            Vector3 input = new Vector3(inputEvent_Move.movement.x, 0, inputEvent_Move.movement.y);
            Vector3 movement;
            if (isInFreeCameraMod)
            { 
                movement = transfrom.right * input.x + transfrom.forward * input.z;
            }
            else
            {
                movement = cameraTransform.right * input.x + cameraTransform.forward * input.z;
            }
            
            movement.y = 0;
            movement.Normalize();
            
            Vector3 newPosition = rigidbody.position + movement * (temSpeed * Time.deltaTime);
            rigidbody.DOMove(newPosition,0.1f);
        }

        /// <summary>
        /// 奔跑中
        /// </summary>
        /// <param name="runData"></param>
        public void Running(SInputEvent_Run runData)
        {
            if (runData.runType == EInputType.Performed)
            {
                running = true;
            }
            else
            {
                running = false;
            }
        }
 
        /// <summary>
        /// 鼠标旋转
        /// </summary>
        /// <param name="inputEvent_Mouse"></param>
        public void MouseRotate(SInputEvent_MouseDrag inputEvent_Mouse)
        {
            Vector2 input = inputEvent_Mouse.mousePos;
            float mouseX = input.x * mouseSensitivity *  Time.deltaTime;
            float mouseY = input.y * mouseSensitivity; 
            
            transfrom.Rotate(Vector3.up * mouseX);
            
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, maxPitchAngle.x, maxPitchAngle.y);
            cameraTransform.DORotate(new Vector3(xRotation, 0f, 0f),0.1f);
        }
        
        /// <summary>
        /// 蹲伏检测
        /// </summary>
        public void CrouchCheck(SInputEvent_Crouch crouchData)
        {
            if (crouchData.crouchType == EInputType.Performed)
            {
                Crouching();
            }
            else
            {
                if (StandUpCheck())
                {
                    Crouching();
                }
                else
                {
                    StandUp();
                }
            }
        }
    }  
}

