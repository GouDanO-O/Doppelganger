using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using QFramework;
using UnityEngine;
using UnityEngine.Events;

namespace GameFrame.World
{
    public class MoveController : AbstractController
    {
        protected bool grounded = false;

        protected Transform transform;

        protected Transform footRoot;

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

        public float dashDuration { get; set; }

        public float dashDistance { get; set; }

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

        protected bool isPressJump = false;

        public override void InitData(WorldObj owner)
        {
            base.InitData(owner);
            this.rigidbody = owner.rigidbody;
            this.collider = owner.collider;
            this.gravity = owner.thisDataConfig.Gravity;
            this.footRoot = owner.footRoot;
            if (collider is BoxCollider boxCollider)
            {
                this.ownerHeight = boxCollider.size.y;
            }
            else if (collider is CapsuleCollider capCollider)
            {
                this.ownerHeight = capCollider.height;
            }

            this.curOwnerHeight = ownerHeight;

            SMoveData moveData = owner.thisDataConfig.MoveData;
            this.walkSpeed = moveData.walkSpeed;
            this.runSpeed = moveData.runSpeed;
            this.inAirMoveSpeed = moveData.inAirMoveSpeed;
            this.groundLayMask = moveData.groundLayerMask;
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

            this.canDoubleJump = jumpData.canDoubleJump;
            this.doubleJumpHeight = jumpData.doubleJumpHeight;
            this.doubleJumpDeepTime = jumpData.doubleJumpDeepTime;
        }

        /// <summary>
        /// 蹲伏初始化
        /// </summary>
        /// <param name="crouchData"></param>
        public virtual void InitCrouch(SCrouchData crouchData)
        {
            this.crouchSpeed = crouchData.crouchSpeed;
            this.crouchReduceRatio = crouchData.crouchReduceRatio;
            this.crouchCheckLayerMask = crouchData.crouchCheckLayerMask;
        }

        /// <summary>
        /// 闪烁初始化
        /// </summary>
        /// <param name="dashData"></param>
        public virtual void InitDash(SDashData dashData)
        {
            this.dashDuration = dashData.dashDuration;
            this.dashDistance = dashData.dashDistance;
            this.dashCD = dashData.dashCD;
            this.lastDashTime = -dashCD;
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
                boxCollider.size = new Vector3(boxCollider.size.x, curOwnerHeight, boxCollider.size.z);
                boxCollider.center = new Vector3(boxCollider.center.x, curOwnerHeight / 2, boxCollider.center.z);
            }
            else if (collider is CapsuleCollider capsuleCollider)
            {
                capsuleCollider.height = curOwnerHeight;
                capsuleCollider.center =
                    new Vector3(capsuleCollider.center.x, curOwnerHeight / 2, capsuleCollider.center.z);
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
            bool canStand = Physics.Raycast(transform.position, transform.up, ownerHeight, crouchCheckLayerMask);
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
            if (Physics.OverlapSphere(footRoot.position, 0.3f, groundLayMask).Length > 0)
            {
                if (!grounded)
                {
                    grounded = true;
                    curJumpCount = 0;
                    curDoubleJumpDeepTime = 0;
                }
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
                    if (canDoubleJump && curJumpCount == 1 && curDoubleJumpDeepTime >= doubleJumpDeepTime)
                    {
                        Jump(doubleJumpHeight);
                        owner.DoPlayAnimations(new SAnimatorEvent() { animationType = EAnimationType.DoubleJumping });
                    }
                    else if (curJumpCount == 0)
                    {
                        Jump(jumpHeight);
                        Main.Interface.GetUtility<CoroutineUtility>().StartRoutine(DoubleJumpTimeCheck());
                        owner.DoPlayAnimations(new SAnimatorEvent() { animationType = EAnimationType.StartJumping });
                    }
                }
            }
            else
            {
                if (canDoubleJump && curJumpCount == 1 && curDoubleJumpDeepTime >= doubleJumpDeepTime)
                {
                    Jump(doubleJumpHeight);
                    owner.DoPlayAnimations(new SAnimatorEvent() { animationType = EAnimationType.DoubleJumping });
                }
                else if (curJumpCount == 0)
                {
                    Jump(jumpHeight);
                    Main.Interface.GetUtility<CoroutineUtility>().StartRoutine(DoubleJumpTimeCheck());
                    owner.DoPlayAnimations(new SAnimatorEvent() { animationType = EAnimationType.StartJumping });
                }
            }
        }

        /// <summary>
        /// 跳跃
        /// </summary>
        protected virtual void Jump(float jumpHeight)
        {
            rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, 0f, rigidbody.linearVelocity.z);
            rigidbody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * gravity), ForceMode.VelocityChange);
            curJumpCount++;
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

        public virtual void Dash()
        {
            if (isInvincibleInDashing)
            {
                Main.Interface.GetUtility<CoroutineUtility>().StartRoutine(InvincibleDashTimeCheck());
            }

            // 启动冲刺协程，控制冲刺行为
            Main.Interface.GetUtility<CoroutineUtility>().StartRoutine(DashCoroutine());

            // 播放冲刺动画
            owner.DoPlayAnimations(new SAnimatorEvent() { animationType = EAnimationType.Dashing });
        }

        // 冲刺协程，控制冲刺时间和距离，考虑障碍物
        private IEnumerator DashCoroutine()
        {
            // 每秒钟的冲刺速度
            float dashSpeed = dashDistance / dashDuration;
            
            float dashTimeElapsed = 0f;

            // 原始速度
            Vector3 originalVelocity = rigidbody.linearVelocity;
            
            while (dashTimeElapsed < dashDuration)
            {
                // 检测是否有障碍物影响路径
                if (IsObstacleInPath())
                {
                    Vector3 newDirection = Vector3.Reflect(rigidbody.linearVelocity.normalized, GetObstacleNormal());
                    rigidbody.linearVelocity = newDirection * dashSpeed;
                    break; 
                }
                else
                {
                    rigidbody.AddForce(transform.forward * dashSpeed, ForceMode.VelocityChange);
                }

                // 增加时间
                dashTimeElapsed += Time.deltaTime;

                yield return null;
            }
            
            rigidbody.linearVelocity = originalVelocity;
        }

        // 检测前方是否有障碍物
        private bool IsObstacleInPath()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, dashDistance))
            {
                return true;
            }
            return false;
        }
        
        // 获取障碍物的法线，用于反弹或调整冲刺方向
        private Vector3 GetObstacleNormal()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, dashDistance))
            {
                // 返回碰撞到的障碍物表面的法线
                return hit.normal;
            }

            return Vector3.up;
        }


        /// <summary>
        /// 无敌时间检测
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator InvincibleDashTimeCheck()
        {
            isInvincibleNow = true;
            owner.ChangeInvincibleMod(isInvincibleNow);
            yield return new WaitForSeconds(dashDuration);
            isInvincibleNow = false;
            owner.ChangeInvincibleMod(isInvincibleNow);
        }
    }
}