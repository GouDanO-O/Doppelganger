using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameFrame.Config;
using QFramework;
using Unity.Netcode.Components;
using UnityEngine;

namespace GameFrame.World
{
    public interface IBiology_Move
    {
        public float temSpeed { get; set; }
        
        public float walkSpeed { get; set; }
        
        public float runSpeed { get; set; }

        public float mouseSensitivity { get; set; }

        public Vector2 maxPitchAngle { get; set; }

        public void Move(SInputEvent_Move inputEvent_Move);
    }
    
    public interface IBiology_Jump
    {
        public float gravity { get; set; }
        
        public float jumpHeight { get; set; }
        
        public float inAirMoveSpeed { get; set; }
        
        public bool canDoubleJump { get; set; }
        
        public float doubleJumpDeepTime { get; set; }
        
        public float doubleJumpHeight { get; set; }

        public void Jump();

        public void GroundCheck();
    }
    
    public interface IBiology_Crouch
    {
        public float crouchSpeed { get; set; }

        public void Crouch();
    }
    
    public class MoveController :IBiology_Move,IBiology_Jump,IBiology_Crouch
    {
        protected WorldObj owner;
        
        protected bool canJump = false;
        
        protected bool canCrouch = false;
        
        protected bool grounded = false;

        protected Transform transfrom;
        
        protected Transform cameraTransform;

        protected Transform headCameraRootTransfrom;
        
        protected Rigidbody rigidbody;
        
        public float temSpeed { get; set; }
        
        public float walkSpeed { get; set; }
        
        public float runSpeed { get; set; }

        public float mouseSensitivity { get; set; }

        public Vector2 maxPitchAngle { get; set; }

        public float gravity { get; set; }
        
        public float jumpHeight { get; set; }
        
        public float inAirMoveSpeed { get; set; }
         
        public bool canDoubleJump { get; set; }
        
        public float doubleJumpDeepTime { get; set; }
        
        public float doubleJumpHeight { get; set; }

        public float crouchSpeed { get; set; }

        public float tickTime { get; set; }

        protected float xRotation;
        
        protected int curJumpCount = 0;

        protected float curDoubleJumpDeepTime = 0;

        /// <summary>
        /// 初始化移动
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="moveData"></param>
        public void InitMovement(WorldObj owner,SMoveData moveData)
        {
            this.owner = owner;
            this.transfrom = owner.transform;
            this.headCameraRootTransfrom = owner.headCameraRootTransfrom;
            this.walkSpeed = moveData.walkSpeed;
            this.runSpeed = moveData.runSpeed;
     
            this.maxPitchAngle = moveData.maxPitchAngle;

            this.temSpeed = this.walkSpeed;
            this.cameraTransform=Camera.main.transform;
            this.rigidbody = owner.rigidbody;
            ResourcesModel resModel = owner.GetModel<ResourcesModel>();
            this.mouseSensitivity = resModel.SettingConfig.MouseSensitivity;
            this.tickTime = resModel.NetDataConfig.TickTime;
        }

        /// <summary>
        /// 跳跃初始化
        /// </summary>
        /// <param name="jumpData"></param>
        public void CanJump(SJumpData jumpData)
        {
            this.canJump=true;
            this.canDoubleJump=jumpData.canDoubleJump;
            this.inAirMoveSpeed = jumpData.inAirMoveSpeed;
            this.doubleJumpHeight= jumpData.doubleJumpHeight;
            this.doubleJumpDeepTime = jumpData.doubleJumpDeepTime;  
        }

        /// <summary>
        /// 蹲伏初始化
        /// </summary>
        /// <param name="crouchData"></param>
        public void CanCrouch(SCrouchData crouchData)
        {
            
        }

        /// <summary>
        /// 闪烁初始化
        /// </summary>
        /// <param name="dashData"></param>
        public void CanDash(SDashData dashData)
        {
            
        }
        
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="inputEvent_Move"></param>
        public virtual void Move(SInputEvent_Move inputEvent_Move)
        {
            Vector3 input = new Vector3(inputEvent_Move.movement.x, 0, inputEvent_Move.movement.y);
            Vector3 movement = transfrom.right * input.x + transfrom.forward * input.z;
            movement.y = 0;
            
            Vector3 newPosition = rigidbody.position + movement * (temSpeed * tickTime);
            rigidbody.MovePosition(newPosition);
        }

        public virtual void MouseRotate(SInputEvent_MouseDrag inputEvent_Mouse)
        {
            Vector2 input = inputEvent_Mouse.mousePos;
            float mouseX = input.x * mouseSensitivity * tickTime;
            float mouseY = input.y * mouseSensitivity; 
            rigidbody.DORotate(Vector3.up * mouseX,0.5f);
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, maxPitchAngle.x, maxPitchAngle.y);
            headCameraRootTransfrom.DOLocalRotate(xRotation*Vector3.right, 0.5f);
        }

        /// <summary>
        /// 跳跃检测
        /// </summary>
        public virtual void JumpCheck()
        {
            if (canJump)
            {
                if (curJumpCount<2)
                {
                    if (canDoubleJump && curJumpCount==1 && curDoubleJumpDeepTime>=doubleJumpDeepTime)
                    {
                        Jump();
                    }
                    else
                    {
                        Jump();
                        curJumpCount++;
                        Main.Interface.GetUtility<CoroutineUtility>().StartRoutine(DoubleJumpTimeCheck());
                    }
                }
            }
        }

        IEnumerator DoubleJumpTimeCheck()
        {
            curDoubleJumpDeepTime = 0;
            yield return new WaitForSeconds(doubleJumpDeepTime);
            curDoubleJumpDeepTime = doubleJumpDeepTime;
        }
        
        /// <summary>
        /// 跳跃
        /// </summary>
        public virtual void Jump()
        {
            
        }

        /// <summary>
        /// 着地检测
        /// </summary>
        public virtual void GroundCheck()
        {
            grounded = true;
            curJumpCount = 0;
            curDoubleJumpDeepTime = 0;
        }
        
        /// <summary>
        /// 蹲伏
        /// </summary>
        public virtual void Crouch()
        {
            if (canCrouch)
            {
                
            }
        }

        /// <summary>
        /// 闪烁
        /// </summary>
        public virtual void Dash()
        {

        }
    }  
}

