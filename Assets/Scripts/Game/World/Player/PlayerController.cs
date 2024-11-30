using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using GameFrame.World;
using QFramework;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace GameFrame.World
{
    /// <summary>
    /// 客户端玩家控制器，用来管理玩家
    /// </summary>
    public class PlayerController : BaseController
    {
        public WorldObj worldObj;

        public WorldObjDataConfig willDeformationConfig { get; }

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
            base.InitData(worldObj);
        }

        public override void DeInitData()
        {
            base.DeInitData();
        }

        protected override void InitMove()
        {
            if (thisDataConfig.moveable)
            {
                moveController = new MoveController_Player();
                moveController.InitData(owner);
                moveController.InitPlayerController(this);

                this.RegisterEvent<SInputEvent_Move>(moveData => { moveController.Move(moveData); })
                    .AddToUnregisterList(this);

                this.RegisterEvent<SInputEvent_MouseDrag>(mouseData => { moveController.MouseRotate(mouseData); })
                    .AddToUnregisterList(this);

                this.RegisterEvent<SInputEvent_Run>(moveData => { moveController.Running(moveData); });

                ActionKit.OnFixedUpdate.Register(() => moveController.GroundCheck()).AddToUnregisterList(this);
            }
        }
        
        void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                float radius = 0.5f; // 与您的 SphereCast 半径一致
                float detectionDistance = 1f; // 与您的 SphereCast 距离一致
                Vector3 origin = transform.position+Vector3.up*0.1f; // SphereCast 的起始位置
                Vector3 direction = Vector3.down; // SphereCast 的方向

                RaycastHit hit;

                // 设置 Gizmos 的颜色
                Gizmos.color = Color.red;

                // 执行 SphereCast，用于获取命中点
                if (Physics.SphereCast(origin, radius, direction, out hit, detectionDistance, 1<<13))
                {
                    // 绘制从起点到命中点的线
                    Gizmos.DrawLine(origin, hit.point);

                    // 在起点绘制一个球体
                    Gizmos.DrawWireSphere(origin, radius);

                    // 在命中点绘制一个球体
                    Gizmos.DrawWireSphere(hit.point, radius);

                    // 可选：在命中点绘制法线方向
                    Gizmos.color = Color.green;
                    Gizmos.DrawRay(hit.point, hit.normal * 0.5f);
                }
                else
                {
                    // // 如果未命中，绘制从起点到最大距离的线
                    // Vector3 endPoint = origin + direction * detectionDistance;
                    // Gizmos.DrawLine(origin, endPoint);
                    //
                    // // 在起点绘制一个球体
                    // Gizmos.DrawWireSphere(origin, radius);
                    //
                    // // 在最大距离处绘制一个球体
                    // Gizmos.DrawWireSphere(endPoint, radius);
                }
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                skillController.UseSkill();
            }
        }
    }
}