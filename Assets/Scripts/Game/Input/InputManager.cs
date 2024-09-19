using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameFrame
{
    public enum EInputType
    {
        Start,
        Processing,
        Cancel
    }

    #region Move
    public struct SInputEvent_Move
    {
        public Vector2 movement;
    }

    public struct SInputEvent_Run
    {
        public EInputType runType;
    }

    public struct SInputEvent_Jump
    {

    }


    public struct SInputEvent_Crouch
    {
        public EInputType crouchType;
    }

    public struct SInputEvent_Dash
    {

    }
    #endregion

    #region Interact 
    public struct SInputEvent_Interact
    {

    }

    public struct SInputEvent_Cancel
    {

    }

    public struct SInputEvent_Package
    {

    }

    public struct SInputEvent_Map
    {

    }

    #endregion

    #region Mouse
    public struct SInputEvent_MouseDrag
    {
        public Vector2 mousePos;

        public EInputType mouseDragType;
    }

    public struct SInputEvent_MouseLeftClick
    {
        public EInputType mouseClcikType;
    }

    public struct SInputEvent_MouseRightClick
    {
        public EInputType mouseClcikType;
    }
    #endregion
    public class InputManager : AbstractSystem
    {
        private InputActionAsset actionAsset;
        #region Move
        private InputActionMap PlayerMovementMap;

        private InputAction moveAction;

        private InputAction runAction;

        private InputAction jumpAction;

        private InputAction crouchAction;

        private InputAction dashAction;

        #endregion

        #region Interact
        private InputActionMap PlayerInteractMap;

        private InputAction interactAction;

        private InputAction mapAction;

        private InputAction cancelAction;

        private InputAction packageAction;

        #endregion

        #region Mouse
        private InputActionMap PlayerMouseMap;

        private InputAction mouseDrag;

        private InputAction mouseLeftClick;

        private InputAction mouseRightClick;
        #endregion

        protected override void OnInit()
        {

        }

        protected override void OnDeinit()
        {
            UnregisterInputCallbacks();
            if (actionAsset != null)
            {
                actionAsset.Disable();
            }
        }

        /// <summary>
        /// 初始化输入
        /// </summary>
        public void InitActionAsset()
        {
            this.actionAsset = this.GetModel<ResourcesModel>().InputActionAsset;

            this.PlayerMovementMap = actionAsset.FindActionMap("PlayerMovementMap");

            this.moveAction = PlayerMovementMap.FindAction("Move");
            this.jumpAction = PlayerMovementMap.FindAction("Jump");
            this.crouchAction = PlayerMovementMap.FindAction("Crouch");
            this.dashAction = PlayerMovementMap.FindAction("Dash");
            this.runAction = PlayerMovementMap.FindAction("Run");

            this.PlayerInteractMap = actionAsset.FindActionMap("PlayerInteractMap");
            this.interactAction = PlayerInteractMap.FindAction("Interact");
            this.mapAction = PlayerInteractMap.FindAction("Map");
            this.cancelAction = PlayerInteractMap.FindAction("Cancel");
            this.packageAction = PlayerInteractMap.FindAction("Package");

            this.PlayerMouseMap = actionAsset.FindActionMap("PlayerMouseMap");
            this.mouseDrag = actionAsset.FindAction("MouseDrag");
            this.mouseLeftClick = actionAsset.FindAction("MouseLeftClick");
            this.mouseRightClick = actionAsset.FindAction("MouseRightClick");
            
            CheckMoveMap(true);
            CheckInteractMap(true);
            CheckMouseMap(true);
            RegisterInputCallbacks();
        }

        public void CheckMoveMap(bool enable)
        {
            if (enable)
            {
                PlayerMovementMap.Enable();;
            }
            else
            {
                PlayerMovementMap.Disable();
            }
        }

        public void CheckInteractMap(bool enable)
        {
            if (enable)
            {
                PlayerInteractMap.Enable();;
            }
            else
            {
                PlayerInteractMap.Disable();
            }
        }

        public void CheckMouseMap(bool enable)
        {
            if (enable)
            {
                PlayerMouseMap.Enable();;
            }
            else
            {
                PlayerMouseMap.Disable();
            }
        }

        /// <summary>
        /// 注册输入回调
        /// </summary>
        private void RegisterInputCallbacks()
        {
            if (moveAction != null)
            {
                moveAction.performed += HandleMove;
            }

            if (jumpAction != null)
            {
                jumpAction.started += HandleJump;
            }

            if (crouchAction != null)
            {
                crouchAction.started += HandleCrouch_Start;
                crouchAction.performed += HandleCrouch_Processing;
                crouchAction.canceled += HandleCrouch_Cancel;
            }

            if(dashAction != null)
            {
                dashAction.started += HandleDash;
            }

            if(runAction!= null)
            {
                runAction.started += HandleRun_Start;
                runAction.performed += HandleRun_Processing;
                runAction.canceled += HandleRun_Cancel;
            }


            if(interactAction != null)
            {
                interactAction.started += HandleInteract;
            }

            if (mapAction != null)
            {
                mapAction.started += HandleMap;
            }

            if (cancelAction != null)
            {
                cancelAction.started += HandleCancel;
            }

            if (packageAction != null)
            {
                packageAction.started += HandlePackage;
            }

            if (mouseDrag != null)
            {
                mouseDrag.started += HandleMouseDrag_Start;
                mouseDrag.performed += HandleMouseDrag_Processing;
                mouseDrag.canceled += HandleMouseDrag_Cancel;
            }

            if (mouseLeftClick != null)
            {
                mouseLeftClick.started += HandleMouseLeftClick_Start;
                mouseLeftClick.performed += HandleMouseLeftClick_Processing;
                mouseLeftClick.canceled += HandleMouseLeftClick_Cancel;
            }

            if (mouseRightClick != null)
            {
                mouseRightClick.started += HandleMouseRightClick_Start;
                mouseRightClick.performed += HandleMouseRightClick_Processing;
                mouseRightClick.canceled += HandleMouseRightClick_Cancel;
            }
        }
        
        /// <summary>
        /// 注销输入回调
        /// </summary>
        private void UnregisterInputCallbacks()
        {
            if (moveAction != null)
            {
                moveAction.performed -= HandleMove;
            }

            if (jumpAction != null)
            {
                jumpAction.started -= HandleJump;
            }

            if (crouchAction != null)
            {
                crouchAction.started -= HandleCrouch_Start;
                crouchAction.performed -= HandleCrouch_Processing;
                crouchAction.canceled -= HandleCrouch_Cancel;
            }

            if (dashAction != null)
            {
                dashAction.started -= HandleDash;
            }

            if (runAction != null)
            {
                runAction.started -= HandleRun_Start;
                runAction.performed -= HandleRun_Processing;
                runAction.canceled -= HandleRun_Cancel;
            }
            
            if (interactAction != null)
            {
                interactAction.started -= HandleInteract;
            }

            if (mapAction != null)
            {
                mapAction.started -= HandleMap;
            }

            if (cancelAction != null)
            {
                cancelAction.started -= HandleCancel;
            }

            if (packageAction != null)
            {
                packageAction.started -= HandlePackage;
            }

            if (mouseDrag != null)
            {
                mouseDrag.started -= HandleMouseDrag_Start;
                mouseDrag.performed -= HandleMouseDrag_Processing;
                mouseDrag.canceled -= HandleMouseDrag_Cancel;
            }

            if (mouseLeftClick != null)
            {
                mouseLeftClick.started -= HandleMouseLeftClick_Start;
                mouseLeftClick.performed -= HandleMouseLeftClick_Processing;
                mouseLeftClick.canceled -= HandleMouseLeftClick_Cancel;
            }

            if (mouseRightClick != null)
            {
                mouseRightClick.started -= HandleMouseRightClick_Start;
                mouseRightClick.performed -= HandleMouseRightClick_Processing;
                mouseRightClick.canceled -= HandleMouseRightClick_Cancel;
            }
        }

        #region Move
        /// <summary>
        /// 处理输入--移动
        /// </summary>
        private void HandleMove(InputAction.CallbackContext context)
        {
            this.SendEvent(new SInputEvent_Move { movement = context.ReadValue<Vector2>() });
        }

        /// <summary>
        /// 处理输入--跳跃
        /// </summary>
        private void HandleJump(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_Jump>();
        }

        /// <summary>
        /// 处理输入--下蹲--开始
        /// </summary>
        private void HandleCrouch_Start(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_Crouch>(new SInputEvent_Crouch { crouchType= EInputType.Start});
        }
        /// <summary>
        /// 处理输入--下蹲--持续
        /// </summary>
        private void HandleCrouch_Processing(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_Crouch>(new SInputEvent_Crouch { crouchType = EInputType.Processing });
        }
        /// <summary>
        /// 处理输入--下蹲--结束
        /// </summary>
        private void HandleCrouch_Cancel(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_Crouch>(new SInputEvent_Crouch { crouchType = EInputType.Cancel });
        }

        /// <summary>
        /// 处理输入--Dash
        /// </summary>
        /// <param name="context"></param>
        private void HandleDash(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_Dash>();
        }

        /// <summary>
        /// 处理输入--奔跑--开始
        /// </summary>
        /// <param name="context"></param>
        private void HandleRun_Start(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_Run>(new SInputEvent_Run { runType=EInputType.Start});
        }

        /// <summary>
        /// 处理输入--奔跑--持续
        /// </summary>
        /// <param name="context"></param>
        private void HandleRun_Processing(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_Run>(new SInputEvent_Run { runType = EInputType.Processing });
        }

        /// <summary>
        /// 处理输入--奔跑--结束
        /// </summary>
        /// <param name="context"></param>
        private void HandleRun_Cancel(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_Run>(new SInputEvent_Run { runType = EInputType.Cancel });
        }
#endregion

        #region Interact
        /// <summary>
        /// 处理输入--交互
        /// </summary>
        private void HandleInteract(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_Interact>();
        }

        /// <summary>
        /// 处理输入--打开地图
        /// </summary>
        private void HandleMap(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_Map>();
        }

        /// <summary>
        /// 处理输入--取消或设置
        /// </summary>
        private void HandleCancel(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_Cancel>();
        }

        /// <summary>
        /// 处理输入--打开背包
        /// </summary>
        private void HandlePackage(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_Package>();
        }

        #endregion

        #region Mouse

        /// <summary>
        /// 是否显示鼠标在游戏内
        /// </summary>
        public void WillShowMouse()
        {

        }

        /// <summary>
        /// 是否限制鼠标在游戏内的移动(不让其超出操作界面)
        /// </summary>
        public void WillImposeMouse()
        {

        }
        /// <summary>
        /// 处理输入--鼠标移动--开始
        /// </summary>
        private void HandleMouseDrag_Start(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_MouseDrag>(new SInputEvent_MouseDrag { mousePos=context.ReadValue<Vector2>(), mouseDragType=EInputType.Start});
        }

        /// <summary>
        /// 处理输入--鼠标移动--持续
        /// </summary>
        private void HandleMouseDrag_Processing(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_MouseDrag>(new SInputEvent_MouseDrag { mousePos = context.ReadValue<Vector2>(), mouseDragType = EInputType.Processing });
        }

        /// <summary>
        /// 处理输入--鼠标移动--结束
        /// </summary>
        private void HandleMouseDrag_Cancel(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_MouseDrag>(new SInputEvent_MouseDrag { mousePos = context.ReadValue<Vector2>(), mouseDragType = EInputType.Cancel });
        }

        /// <summary>
        /// 处理输入--鼠标左键--开始
        /// </summary>
        private void HandleMouseLeftClick_Start(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_MouseLeftClick>(new SInputEvent_MouseLeftClick { mouseClcikType = EInputType.Start });
        }

        /// <summary>
        /// 处理输入--鼠标左键--持续
        /// </summary>
        private void HandleMouseLeftClick_Processing(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_MouseLeftClick>(new SInputEvent_MouseLeftClick { mouseClcikType = EInputType.Processing });
        }

        /// <summary>
        /// 处理输入--鼠标左键--结束
        /// </summary>
        private void HandleMouseLeftClick_Cancel(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_MouseLeftClick>(new SInputEvent_MouseLeftClick { mouseClcikType = EInputType.Cancel });
        }

        /// <summary>
        /// 处理输入--鼠标右键--开始
        /// </summary>
        private void HandleMouseRightClick_Start(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_MouseRightClick>(new SInputEvent_MouseRightClick { mouseClcikType = EInputType.Start });
        }

        /// <summary>
        /// 处理输入--鼠标右键--持续
        /// </summary>
        private void HandleMouseRightClick_Processing(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_MouseRightClick>(new SInputEvent_MouseRightClick { mouseClcikType = EInputType.Processing });
        }

        /// <summary>
        /// 处理输入--鼠标右键--结束
        /// </summary>
        private void HandleMouseRightClick_Cancel(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_MouseRightClick>(new SInputEvent_MouseRightClick { mouseClcikType = EInputType.Cancel });
        }

        #endregion
        /// <summary>
        /// 改变输入键值
        /// </summary>
        public void ChangeInputKey()
        {
            
        }
    }
}
