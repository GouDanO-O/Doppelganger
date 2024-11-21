using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using QFramework;
using UnityEngine;

namespace GameFrame.World
{
    public class MoveController : AbstractController
    {
        protected bool grounded = false;
        
        protected Transform transfrom;
        
        protected Collider collider;
        
        protected Rigidbody rigidbody;
        
        public float ownerHeight { get; set; }
        
        public float temSpeed { get; set; }
        
        public float walkSpeed { get; set; }
        
        public float runSpeed { get; set; }

        public float gravity { get; set; }
        
        public LayerMask groundLayMask { get; set; }
        
        public float jumpHeight { get; set; }
        
        public float inAirMoveSpeed { get; set; }
         
        public bool canDoubleJump { get; set; }
        
        public float doubleJumpDeepTime { get; set; }
        
        public float doubleJumpHeight { get; set; }

        public float crouchSpeed { get; set; }
        
        public float dashSpeed { get; set; }
        
        public float dashCD { get; set; }
        
        public bool isInvincibleInDashing { get; set; }
        
        public bool isInvincibleNow { get; set; }
        
        public float crouchReduceRatio { get; set; }
        
        public LayerMask crouchCheckLayerMask { get; set; }
        
        protected float lastDashTime;
        
        protected float curOwnerHeight;
        
        protected int curJumpCount = 0;

        protected float curDoubleJumpDeepTime = 0;
        
        protected bool crouching = false;
        
        protected bool running = false;
        
        public override void InitData(WorldObj owner)
        {
            base.InitData(owner);
            this.rigidbody = owner.rigidbody;
            this.collider= owner.collider;
            this.gravity = owner.thisDataConfig.gravity;
            
            if (collider is BoxCollider boxCollider)
            {
                this.ownerHeight = boxCollider.size.y;
            }
            else if (collider is CapsuleCollider capCollider)
            {
                this.ownerHeight = capCollider.height;
            }
            this.curOwnerHeight=ownerHeight;
            
            SMoveData moveData = owner.thisDataConfig.moveData;
            this.walkSpeed = moveData.walkSpeed;
            this.runSpeed = moveData.runSpeed;
            this.inAirMoveSpeed = moveData.inAirMoveSpeed;
            this.groundLayMask=moveData.groundLayerMask;
            this.temSpeed = GetTemSpeed();
        }
        
        public override void DeInitData()
        {
            
        }
        
        /// <summary>
        /// 跳跃初始化
        /// </summary>
        /// <param name="jumpData"></param>
        public virtual void InitJump(SJumpData jumpData)
        {
            this.jumpHeight = jumpData.jumpHeight;
            
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
            this.crouchCheckLayerMask = crouchData.crouchCheckLayerMask;
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
        
        /// <summary>
        /// 获取当前的速度
        /// </summary>
        /// <returns></returns>
        public float GetTemSpeed()
        {
            return grounded ? (crouching ? crouchSpeed : running ? runSpeed : walkSpeed) : inAirMoveSpeed;
        }

        /// <summary>
        /// 改变当前拥有者的身高
        /// </summary>
        protected virtual void ChangeOwnerHeight()
        {
            if (collider is BoxCollider boxCollider)
            {
                boxCollider.size=new Vector3(boxCollider.size.x,curOwnerHeight,boxCollider.size.z);
                boxCollider.center=new Vector3(boxCollider.center.x,curOwnerHeight/2,boxCollider.center.z);
            }
            else if (collider is CapsuleCollider capsuleCollider)
            {
                capsuleCollider.height = curOwnerHeight;
                capsuleCollider.center=new Vector3(capsuleCollider.center.x,curOwnerHeight/2,capsuleCollider.center.z);
            }

            if (crouching)
            {
                owner.DoPlayAnimations(new SAnimatorEvent() { animationType = EAnimationType.Crouching });
            }
            else
            {
                owner.DoPlayAnimations(new SAnimatorEvent() { animationType = EAnimationType.StandUp });
            }
        }
        
        /// <summary>
        /// 起立检测--true=>无法起立，false=>可以起立
        /// </summary>
        protected bool StandUpCheck()
        {
            bool canStand = Physics.Raycast(transfrom.position, transfrom.up, ownerHeight, crouchCheckLayerMask);
            return canStand;
        }
        
        /// <summary>
        /// 蹲伏中
        /// </summary>
        protected void Crouching()
        {
            if (grounded)
            {
                crouching = true;
                curOwnerHeight = ownerHeight * crouchReduceRatio;
                ChangeOwnerHeight();
            }
        }
        
        /// <summary>
        /// 起立
        /// </summary>
        protected void StandUp()
        {
            curOwnerHeight = ownerHeight;
            crouching = false;
            ChangeOwnerHeight();
        }
        
        /// <summary>
        /// 着地检测
        /// </summary>
        public virtual void GroundCheck()
        {
            if (Physics.OverlapSphere(transfrom.position, 0.5f, groundLayMask).Length>0)
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
                            owner.DoPlayAnimations(new SAnimatorEvent() { animationType = EAnimationType.StartJumping });
                        }
                        else
                        {
                            Jump();
                            curJumpCount++;
                            Main.Interface.GetUtility<CoroutineUtility>().StartRoutine(DoubleJumpTimeCheck());
                            owner.DoPlayAnimations(new SAnimatorEvent() { animationType = EAnimationType.DoubleJumping });
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
                         owner.DoPlayAnimations(new SAnimatorEvent() { animationType = EAnimationType.StartJumping });
                    }
                    else
                    {
                        Jump();
                        curJumpCount++;
                        Main.Interface.GetUtility<CoroutineUtility>().StartRoutine(DoubleJumpTimeCheck());
                        owner.DoPlayAnimations(new SAnimatorEvent() { animationType = EAnimationType.DoubleJumping });
                    }
                }
            }
        }
        
        /// <summary>
        /// 跳跃
        /// </summary>
        protected virtual void Jump()
        {
            rigidbody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * gravity), ForceMode.VelocityChange);
            
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
            if (Time.time - lastDashTime >= dashCD)
            {
                lastDashTime = Time.time;
                Dash();
            }
        }

        /// <summary>
        /// 闪烁
        /// </summary>
        public virtual void Dash()
        {
            if (isInvincibleInDashing)
            {
                Main.Interface.GetUtility<CoroutineUtility>().StartRoutine(InvincibleDashTimeCheck());
            }
            rigidbody.AddForce(transfrom.forward*dashSpeed, ForceMode.Impulse);
            owner.DoPlayAnimations(new SAnimatorEvent() { animationType = EAnimationType.Dashing });
        }

        /// <summary>
        /// 无敌时间检测
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator InvincibleDashTimeCheck()
        {
            isInvincibleNow = true;
            yield return new WaitForSeconds(dashCD);
            isInvincibleNow = false;
        }
    }
}

