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

        public float rotateSpeed { get; set; }

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

        protected Rigidbody rigidbody;
        
        public float temSpeed { get; set; }
        
        public float walkSpeed { get; set; }
        
        public float runSpeed { get; set; }

        public float rotateSpeed { get; set; }

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

        public void InitMovement(WorldObj owner,SMoveData moveData)
        {
            this.owner = owner;
            this.transfrom = owner.transform;
            this.walkSpeed = moveData.walkSpeed;
            this.runSpeed = moveData.runSpeed;
            this.rotateSpeed=moveData.rotateSpeed;
            this.maxPitchAngle = moveData.maxPitchAngle;

            this.temSpeed = this.walkSpeed;
           
            this.rigidbody = owner.rigidbody;
            this.tickTime=owner.GetModel<ResourcesModel>().NetDataConfig.TickTime;
        }

        public void CanJump(SJumpData jumpData)
        {
            this.canJump=true;
            this.canDoubleJump=jumpData.canDoubleJump;
            this.inAirMoveSpeed = jumpData.inAirMoveSpeed;
            this.doubleJumpHeight= jumpData.doubleJumpHeight;
            this.doubleJumpDeepTime = jumpData.doubleJumpDeepTime;  
        }

        public void CanCrouch(SCrouchData crouchData)
        {
            
        }

        public void CanDash(SDashData dashData)
        {
            
        }
        
        public virtual void MoveControll()
        {
            Jump();
            Crouch();
            GroundCheck();
        }
        
        public virtual void Move(SInputEvent_Move inputEvent_Move)
        {
            Vector3 input = new Vector3(inputEvent_Move.movement.x, 0, inputEvent_Move.movement.y);

            Vector3 movement = transfrom.right * input.x + transfrom.forward * input.z;

            rigidbody.DOMove(movement, temSpeed * tickTime);
        }

        public virtual void MouseRotate(SInputEvent_MouseDrag inputEvent_Mouse)
        {
            if (inputEvent_Mouse.mouseDragType == EInputType.Processing)
            {
                Vector2 input = inputEvent_Mouse.mousePos;
                float mouseX = input.x * rotateSpeed * tickTime;
                float mouseY = input.y * tickTime;
                transfrom.Rotate(Vector3.up * mouseX);

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, maxPitchAngle.x, maxPitchAngle.y);
            }
        }

        public virtual void Jump()
        {
            if (canJump)
            {
                if (grounded)
                {
                    
                }
            }
        }

        protected virtual void DoubleJump()
        {
            
        }

        public virtual void GroundCheck()
        {
            grounded = true;
        }
        
        public virtual void Crouch()
        {
            if (canCrouch)
            {
                
            }
        }

        public virtual void Dash()
        {

        }
    }  
}

