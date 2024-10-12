using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using UnityEngine;

namespace GameFrame.World
{
    public class MoveController
    {
        protected bool grounded = false;
        
        protected Transform transfrom;
        
        protected Rigidbody rigidbody;
        
        public float ownerHeight { get; set; }
        
        public float temSpeed { get; set; }
        
        public float walkSpeed { get; set; }
        
        public float runSpeed { get; set; }

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
        
        protected float curOwnerHeight;
        
        protected int curJumpCount = 0;

        protected float curDoubleJumpDeepTime = 0;
        
        protected bool crouching = false;
        
        protected bool running = false;
        
        public virtual void InitMovement(SMoveData moveData)
        {
            this.walkSpeed = moveData.walkSpeed;
            this.runSpeed = moveData.runSpeed;
            this.inAirMoveSpeed = moveData.inAirMoveSpeed;

            this.temSpeed = GetTemSpeed();
        }
        
        /// <summary>
        /// 跳跃初始化
        /// </summary>
        /// <param name="jumpData"></param>
        public virtual void InitJump(SJumpData jumpData)
        {
            this.canDoubleJump=jumpData.canDoubleJump;
            this.doubleJumpHeight= jumpData.doubleJumpHeight;
            this.doubleJumpDeepTime = jumpData.doubleJumpDeepTime;  
        }

        /// <summary>
        /// 蹲伏初始化
        /// </summary>
        /// <param name="crouchData"></param>
        public virtual void InitCrouch(SCrouchData crouchData)
        {
            this.crouchSpeed=crouchData.crouchSpeed;
            this.crouchReduceRatio=crouchData.crouchReduceRatio;
        }

        /// <summary>
        /// 闪烁初始化
        /// </summary>
        /// <param name="dashData"></param>
        public virtual void InitDash(SDashData dashData)
        {
            this.dashSpeed = dashData.dashSpeed;
            this.dashCD = dashData.dashCD;
        }
        
        public float GetTemSpeed()
        {
            return grounded ? (crouching ? crouchSpeed : running ? runSpeed : walkSpeed) : inAirMoveSpeed;
        }
        
        /// <summary>
        /// 跳跃
        /// </summary>
        protected virtual void Jump()
        {
            if (grounded)
            {
                // 添加向上的力，实现跳跃
                rigidbody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * gravity), ForceMode.VelocityChange);
                grounded = false;
            }
        }

        /// <summary>
        /// 改变当前拥有者的身高
        /// </summary>
        protected virtual void ChangeOwnerHeight()
        {
            
        }
        
        /// <summary>
        /// 起立检测--true=>无法起立，false=>可以起立
        /// </summary>
        protected bool StandUpCheck()
        {
            return Physics.Raycast(transfrom.position, transfrom.up,ownerHeight, crouchCheckLayerMask);
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
        /// 起立
        /// </summary>
        protected void StandUp()
        {
            curOwnerHeight = ownerHeight;
            crouching = false;
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
        
        protected virtual IEnumerator DoubleJumpTimeCheck()
        {
            curDoubleJumpDeepTime = 0;
            yield return new WaitForSeconds(doubleJumpDeepTime);
            curDoubleJumpDeepTime = doubleJumpDeepTime;
        }
        
        /// <summary>
        /// 闪烁检测
        /// </summary>
        public virtual void DashCheck()
        {

        }
    }
}

