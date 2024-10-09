using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using GameFrame.Config;
using GameFrame.Word;
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
        
        public float inAirMoveSpeed { get; set; } 

        public float mouseSensitivity { get; set; }

        public Vector2 maxPitchAngle { get; set; }

        public void Move(SInputEvent_Move inputEvent_Move);
    }
    
    public interface IBiology_Jump
    {
        public float gravity { get; set; }
        
        public float jumpHeight { get; set; }
        

        
        public bool canDoubleJump { get; set; }
        
        public float doubleJumpDeepTime { get; set; }
        
        public float doubleJumpHeight { get; set; }

        public void Jump();

        public void GroundCheck();
    }
    
    public interface IBiology_Crouch
    {
        public float crouchSpeed { get; set; }

        public void CrouchCheck(SInputEvent_Crouch crouchData);
    }

    public interface IBiology_Dash
    {
        public float dashSpeed { get; set; }
        
        public float dashCD { get; set; }

        public void DashCheck();
    }
    
    public class MoveController :IBiology_Move,IBiology_Jump,IBiology_Crouch,IBiology_Dash
    {
        protected PlayerController owner;
        
        protected bool grounded = false;

        protected Transform transfrom;
        
        protected Transform cameraTransform;

        protected Transform headCameraRootTransfrom;
        
        protected Rigidbody rigidbody;
        
        public float ownerHeight { get; set; }
        
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
        
        public float dashSpeed { get; set; }
        
        public float dashCD { get; set; }
        
        public float crouchReduceRatio { get; set; }
        
        public LayerMask crouchCheckLayerMask { get; set; }
        
        public float tickTime { get; set; }
        
        protected float curOwnerHeight;

        protected float xRotation;
        
        protected int curJumpCount = 0;

        protected float curDoubleJumpDeepTime = 0;
        
        protected bool crouching = false;
        
        protected bool running = false;

        /// <summary>
        /// 是否处于自由相机模式
        /// </summary>
        public bool isInFreeCameraMod
        {
            get { return true;}}

        /// <summary>
        /// 初始化移动
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="moveData"></param>
        public void InitMovement(PlayerController owner,SMoveData moveData)
        {
            this.owner = owner;
            this.transfrom = owner.transform;
            this.headCameraRootTransfrom = owner.headCameraRootTransfrom;
            this.walkSpeed = moveData.walkSpeed;
            this.runSpeed = moveData.runSpeed;
            this.inAirMoveSpeed = moveData.inAirMoveSpeed;
            
            this.maxPitchAngle = moveData.maxPitchAngle;

            this.temSpeed = this.walkSpeed;
            this.cameraTransform = CameraController.Instance.virtualCamera.transform;
            
            this.rigidbody = owner.rigidbody;
            ResourcesModel resModel = owner.GetModel<ResourcesModel>();
            this.mouseSensitivity = resModel.SettingConfig.MouseSensitivity;
            this.tickTime = resModel.NetDataConfig.TickTime;
        }

        /// <summary>
        /// 跳跃初始化
        /// </summary>
        /// <param name="jumpData"></param>
        public void InitJump(SJumpData jumpData)
        {
            this.canDoubleJump=jumpData.canDoubleJump;
            this.doubleJumpHeight= jumpData.doubleJumpHeight;
            this.doubleJumpDeepTime = jumpData.doubleJumpDeepTime;  
        }

        /// <summary>
        /// 蹲伏初始化
        /// </summary>
        /// <param name="crouchData"></param>
        public void InitCrouch(SCrouchData crouchData)
        {
            this.crouchSpeed=crouchData.crouchSpeed;
            this.crouchReduceRatio=crouchData.crouchReduceRatio;
        }

        /// <summary>
        /// 闪烁初始化
        /// </summary>
        /// <param name="dashData"></param>
        public void InitDash(SDashData dashData)
        {
            this.dashSpeed = dashData.dashSpeed;
            this.dashCD = dashData.dashCD;
        }
        
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="inputEvent_Move"></param>
        public virtual void Move(SInputEvent_Move inputEvent_Move)
        {
            temSpeed = grounded ? (crouching ? crouchSpeed : running ? runSpeed : walkSpeed) : inAirMoveSpeed;
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
        public virtual void Running(SInputEvent_Run runData)
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
        public virtual void MouseRotate(SInputEvent_MouseDrag inputEvent_Mouse)
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
        /// 跳跃检测
        /// </summary>
        public virtual void JumpCheck()
        {
            if (crouching)
            {
                if (!StandUpCheck())
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
            else
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
            if (grounded)
            {
                // 添加向上的力，实现跳跃
                rigidbody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * gravity), ForceMode.VelocityChange);
                grounded = false;
            }
        }

        /// <summary>
        /// 着地检测
        /// </summary>
        public virtual void GroundCheck()
        {
            RaycastHit hit;
            if (Physics.SphereCast(transfrom.position, 0.5f, Vector3.down, out hit, 1f))
            {
                grounded = true;
                curJumpCount = 0;
                curDoubleJumpDeepTime = 0;
            }
            else
            {
                grounded = false;
            }
        }
        
        /// <summary>
        /// 蹲伏
        /// </summary>
        public virtual void CrouchCheck(SInputEvent_Crouch crouchData)
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

        /// <summary>
        /// 蹲伏中
        /// </summary>
        protected void Crouching()
        {
            if (grounded)
            {
                temSpeed = crouchSpeed;
                curOwnerHeight = ownerHeight * crouchReduceRatio;
                crouching = true;
            }
        }

        /// <summary>
        /// 起立检测--true=>无法起立，false=>可以起立
        /// </summary>
        protected bool StandUpCheck()
        {
            return Physics.Raycast(transfrom.position, transfrom.up,ownerHeight, crouchCheckLayerMask);
        }

        /// <summary>
        /// 起立
        /// </summary>
        protected void StandUp()
        {
            curOwnerHeight = ownerHeight;
            crouching = false;
        }
        
        /// <summary>
        /// 闪烁检测
        /// </summary>
        public virtual void DashCheck()
        {

        }
    }  
}

