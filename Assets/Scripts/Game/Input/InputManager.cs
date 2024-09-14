using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameFrame
{
    public struct SInputEvent_Move
    {
        public Vector2 movement;
    }

    public struct SInputEvent_Jump
    {
        
    }

    public struct SInputEvent_Crouch
    {
        
    }

    public struct SInputEvent_Interact
    {
        
    }
    
    public class InputManager : AbstractSystem
    {
        private InputActionAsset actionAsset;
        
        private InputAction moveAction;
        
        private InputAction jumpAction;
        
        private InputAction crouchAction;
        
        private InputAction interactAction;
        
        protected override void OnInit()
        {
            
        }

        protected override void OnDeinit()
        {
            base.OnDeinit();
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
            this.moveAction=actionAsset.FindAction("Move");
            this.jumpAction=actionAsset.FindAction("Jump"); 
            this.crouchAction=actionAsset.FindAction("Crouch");
            this.interactAction=actionAsset.FindAction("Interact");
        }

        /// <summary>
        /// 改变输入键值
        /// </summary>
        public void ChangeInputKey()
        { 
            
        }

        private void HandleMove(Vector2 inputMove)
        {
            this.SendEvent(new SInputEvent_Move{movement = inputMove});
        }
    }
}

