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
        Performed,
        Cancel
    }

    #region Move
    public struct SInputEvent_Move
    {
        public Vector2 movement;
    }

    public struct SInputEvent_Run
    {
        
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
    }

    public struct SInputEvent_MouseLeftClick
    {
        
    }

    public struct SInputEvent_MouseRightClick
    {
        
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
                moveAction.performed += HandleMove_Performed;
                moveAction.canceled += HandleMove_Cancel;
            }
            
            if (jumpAction != null)
            {
                jumpAction.performed += HandleJump;
            }

            if (crouchAction != null)
            {
                crouchAction.performed += HandleCrouch_Performed;
                crouchAction.canceled += HandleCrouch_Cancel;
            }

            if(dashAction != null)
            {
                dashAction.performed += HandleDash;
            }

            if(runAction!= null)
            {
                runAction.performed += HandleRun_Performed;
                runAction.canceled += HandleRun_Cancel;
            }


            if(interactAction != null)
            {
                interactAction.performed += HandleInteract;
            }

            if (mapAction != null)
            {
                mapAction.performed += HandleMap;
            }

            if (cancelAction != null)
            {
                cancelAction.performed += HandleCancel;
            }

            if (packageAction != null)
            {
                packageAction.performed += HandlePackage;
            }

            if (mouseDrag != null)
            {
                mouseDrag.performed += HandleMouseDrag;
            }

            if (mouseLeftClick != null)
            {
                mouseLeftClick.performed += HandleMouseLeftClick;
            }

            if (mouseRightClick != null)
            {
                mouseRightClick.performed += HandleMouseRightClick;
            }
        }
        
        /// <summary>
        /// 注销输入回调
        /// </summary>
        private void UnregisterInputCallbacks()
        {
            if (moveAction != null)
            {
                moveAction.performed -= HandleMove_Performed;
                moveAction.canceled -= HandleMove_Cancel;
            }
            
            if (jumpAction != null)
            {
                jumpAction.performed -= HandleJump;
            }

            if (crouchAction != null)
            {
                crouchAction.performed -= HandleCrouch_Performed;
                crouchAction.canceled -= HandleCrouch_Cancel;
            }

            if(dashAction != null)
            {
                dashAction.performed -= HandleDash;
            }

            if(runAction!= null)
            {
                runAction.performed -= HandleRun_Performed;
                runAction.canceled -= HandleRun_Cancel;
            }


            if(interactAction != null)
            {
                interactAction.performed -= HandleInteract;
            }

            if (mapAction != null)
            {
                mapAction.performed -= HandleMap;
            }

            if (cancelAction != null)
            {
                cancelAction.performed -= HandleCancel;
            }

            if (packageAction != null)
            {
                packageAction.performed -= HandlePackage;
            }

            if (mouseDrag != null)
            {
                mouseDrag.performed -= HandleMouseDrag;
            }

            if (mouseLeftClick != null)
            {
                mouseLeftClick.performed -= HandleMouseLeftClick;
            }

            if (mouseRightClick != null)
            {
                mouseRightClick.performed -= HandleMouseRightClick;
            }
        }

        #region Move

        private Vector2 curMovement;
        
        public void MovementCheck()
        {
            if (curMovement != Vector2.zero)
            {
                this.SendEvent(new SInputEvent_Move { movement = curMovement });
            }
        }
        
        /// <summary>
        /// 处理输入--移动
        /// </summary>
        private void HandleMove_Performed(InputAction.CallbackContext context)
        {
            curMovement = context.ReadValue<Vector2>();
        }
        
        /// <summary>
        /// 处理输入--移动
        /// </summary>
        private void HandleMove_Cancel(InputAction.CallbackContext context)
        {
            curMovement = Vector2.zero;
        }

        /// <summary>
        /// 处理输入--跳跃
        /// </summary>
        private void HandleJump(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_Jump>();
        }
        
        /// <summary>
        /// 处理输入--下蹲--持续
        /// </summary>
        private void HandleCrouch_Performed(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_Crouch>(new SInputEvent_Crouch { crouchType = EInputType.Performed });
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
        /// 处理输入--奔跑--持续
        /// </summary>
        /// <param name="context"></param>
        private void HandleRun_Performed(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_Run>();
        }
        
        /// <summary>
        /// 处理输入--奔跑--取消
        /// </summary>
        /// <param name="context"></param>
        private void HandleRun_Cancel(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_Run>(); 
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
        /// 处理输入--鼠标移动--持续
        /// </summary>
        private void HandleMouseDrag(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_MouseDrag>(new SInputEvent_MouseDrag(){ mousePos = context.ReadValue<Vector2>()});
        }
        

        /// <summary>
        /// 处理输入--鼠标左键
        /// </summary>
        private void HandleMouseLeftClick(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_MouseLeftClick>();
        }

        /// <summary>
        /// 处理输入--鼠标右键
        /// </summary>
        private void HandleMouseRightClick(InputAction.CallbackContext context)
        {
            this.SendEvent<SInputEvent_MouseRightClick>();
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
