using System.Collections;
using System.Collections.Generic;
using GameFrame.World;
using UnityEngine;

namespace GameFrame.World
{
    /// <summary>
    /// 移动系统
    /// </summary>
    public partial class Monster : IWorldObj_Move,IWorldObj_Jump,IWorldObj_Crouch
    {
        public float temSpeed { get; set; }
        
        public float walkSpeed { get; set; }
        
        public float sprintSpeed { get; set; }
        
        public void Move()
        {
            throw new System.NotImplementedException();
        }

        public float gravity { get; set; }
        
        public float jumpHeight { get; set; }
        
        public float inAirMoveSpeed { get; set; }
        
        public void Jump()
        {
           
        }

        public void GroundCheck()
        {
            
        }

        public float crouchSpeed { get; set; }
        public void Crouch()
        {
            
        }
    }
}


