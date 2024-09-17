using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using UnityEngine;
using GameFrame.Net;
using UnityEngine.Scripting;
using Unity.Netcode;
using QFramework;
using Unity.Netcode.Components;

namespace GameFrame.World
{
    
    public partial class WorldObj : NetworkBehaviour,IController
    {
        public WorldObjDataConfig thisDataConfig;
        
        public MoveController moveController { get; set;}
        
        public Rigidbody rigidbody { get; set; }

        private void Awake()
        {
            InitData();
        }

        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }

        public virtual void InitData()
        {
            if (thisDataConfig is WorldObjDataConfig)
            {
                InitComponents();
            }
            else
            {
                Debug.LogError("WorldObjDataConfig is not set!");
            }
        }

        protected virtual void InitComponents()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        protected virtual void InitMovement()
        {

        }

        protected virtual void InitJump()
        {
            
        }

        protected virtual void InitCrouch()
        {
            
        }

        protected virtual void InitDash()
        {
            
        }


        protected virtual void CollisionEnter(Collision other)
        {
            
        }
        
        protected virtual void CollisionStay(Collision other)
        {
            
        }
        
        protected virtual void CollisionExit(Collision other)
        {
            
        }
        
        private void OnCollisionEnter(Collision other)
        {
            CollisionEnter(other);
        }

        private void OnCollisionStay(Collision other)
        {
            CollisionStay(other);
        }
        
        private void OnCollisionExit(Collision other)
        {
            CollisionExit(other);
        }


    }
}


