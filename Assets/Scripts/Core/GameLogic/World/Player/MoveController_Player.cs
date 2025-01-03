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
        protected Transform cameraTransform;
        
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
        /// 初始化移动
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="moveData"></param>
        public override void InitData(WorldObj owner)
        {
            base.InitData(owner);
            
            SMoveData moveData = owner.thisDataConfig.MoveData;
            this.maxPitchAngle = moveData.maxPitchAngle;
            
            ResourcesData_Model resDataModel = owner.GetModel<ResourcesData_Model>();
            this.mouseSensitivity = resDataModel.SettingConfig.MouseSensitivity;
            this.cameraTransform = CameraController.Instance.virtualCamera.transform;
        }
        
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="inputEvent_Move"></param>
        public override void Move(SInputEvent_Move inputEvent_Move)
        {
            temSpeed = GetTemSpeed();
            Vector3 input = new Vector3(inputEvent_Move.movement.x, 0, inputEvent_Move.movement.y);
            Vector3 movement;

            if (isInFreeCameraMod)
            { 
                movement = transform.right * input.x + transform.forward * input.z;
            }
            else
            {
                movement = cameraTransform.right * input.x + cameraTransform.forward * input.z;
            }

            movement.y = 0;
            movement.Normalize();

            Vector3 newPosition = rigidbody.position + movement * (temSpeed * Time.fixedDeltaTime);
            rigidbody.MovePosition(newPosition);
        }

        /// <summary>
        /// 奔跑中
        /// </summary>
        /// <param name="runData"></param>
        public override void Running(SInputEvent_Run runData)
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
        public override void MouseRotate(SInputEvent_MouseDrag inputEvent_Mouse)
        {
            Vector2 input = inputEvent_Mouse.mousePos;
            float mouseX = input.x * mouseSensitivity *  Time.deltaTime;
            float mouseY = input.y * mouseSensitivity * Time.deltaTime; 
            
            Quaternion deltaRotation = Quaternion.Euler(0f, mouseX, 0f);
            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
            
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, maxPitchAngle.x, maxPitchAngle.y);
            cameraTransform.localEulerAngles = new Vector3(xRotation, 0f, 0f);
        }
        
        /// <summary>
        /// 蹲伏检测
        /// </summary>
        public override void CrouchCheck(SInputEvent_Crouch crouchData)
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

