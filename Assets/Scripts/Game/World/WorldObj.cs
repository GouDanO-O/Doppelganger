using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFrame.Net;
using UnityEngine.Scripting;

namespace GameFrame.World
{
    public interface IWorldObj_Move
    {
        public float temSpeed { get; set; }
        
        public float walkSpeed { get; set; }
        
        public float sprintSpeed { get; set; }

        public void Move();
    }
    
    [RequiredInterface(typeof(IWorldObj_Move))]
    public interface IWorldObj_Jump
    {
        public float gravity { get; set; }
        
        public float jumpHeight { get; set; }
        
        public float inAirMoveSpeed { get; set; }

        public void Jump();

        public void GroundCheck();
    }
    
    [RequiredInterface(typeof(IWorldObj_Move))]
    public interface IWorldObj_Crouch
    {
        public float crouchSpeed { get; set; }

        public void Crouch();
    }

    public interface IWorldObj_Healthy
    {
        public bool isDeath { get; set; }
        
        public float curHealthy { get; set; }
        
        public float maxHealthy { get; set; }

        public void Beharmed(float damage)
        {
            if(this.isDeath)
                return;
            this.curHealthy -= damage;
            if (curHealthy <= 0)
            {
                Death();
            }
        }

        public void Becuring(float cureValue)
        {
            this.curHealthy += cureValue;
            if (curHealthy >= this.maxHealthy)
            {
                this.curHealthy = this.maxHealthy;
            }
        }

        public void Death()
        {
            isDeath = true;
        }
    }

    [RequiredInterface(typeof(IWorldObj_Healthy))]
    public interface IWorldObj_Attack
    {
        public float damage { get; set; }

        public void HarmTarget(IWorldObj_Healthy target)
        {
            target.Beharmed(damage);
        }
    }
    
    public partial class WorldObj : NetObj
    {
        
    }
}


