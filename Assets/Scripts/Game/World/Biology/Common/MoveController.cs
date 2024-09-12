using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.World
{
    public interface IBiology_Move
    {
        public float temSpeed { get; set; }
        
        public float walkSpeed { get; set; }
        
        public float sprintSpeed { get; set; }

        public void Move();
    }
    
    public interface IBiology_Jump
    {
        public float gravity { get; set; }
        
        public float jumpHeight { get; set; }
        
        public float inAirMoveSpeed { get; set; }

        public void Jump();

        public void GroundCheck();
    }
    
    public interface IBiology_Crouch
    {
        public float crouchSpeed { get; set; }

        public void Crouch();
    }
    
    public abstract class MoveController :IBiology_Move,IBiology_Jump,IBiology_Crouch
    {
        public float temSpeed { get; set; }
        
        public float walkSpeed { get; set; }
        
        public float sprintSpeed { get; set; }
        
        public float gravity { get; set; }
        
        public float jumpHeight { get; set; }
        
        public float inAirMoveSpeed { get; set; }
        
        public float crouchSpeed { get; set; }

        public void InitMovement()
        {
            
        }
        
        protected virtual void MoveControll()
        {
            
        }
        
        public virtual void Move()
        {
            
        }
        
        public virtual void Jump()
        {
           
        }

        public virtual void GroundCheck()
        {
            
        }
        
        public virtual void Crouch()
        {
            
        }
    }  
}

