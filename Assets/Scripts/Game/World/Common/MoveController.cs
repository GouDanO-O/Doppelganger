using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using UnityEngine;

namespace GameFrame.World
{
    public interface IBiology_Move
    {
        public float temSpeed { get; set; }
        
        public float walkSpeed { get; set; }
        
        public float runSpeed { get; set; }

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

        public void InitMovement(WorldObj owner,SMoveData moveData)
        {
            this.owner = owner;
            this.transfrom = owner.transform;
            this.walkSpeed = moveData.walkSpeed;
            this.runSpeed = moveData.runSpeed;
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
            Debug.Log(inputEvent_Move.movement);
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
    }  
}

