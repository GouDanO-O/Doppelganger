using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using QFramework;
using UnityEngine;

namespace GameFrame.World
{
    
    
    public abstract class Biology : WorldObj
    {
        protected HealthyController healthyController;
        
        private BiologyDataConfig biologyDataConfig;

        public override void InitData()
        {
            if (thisDataConfig is BiologyDataConfig)
            {
                try
                {
                    biologyDataConfig = thisDataConfig as BiologyDataConfig;
                }
                catch (Exception e)
                {
                    Debug.Log("转换失败");
                }
            }
            else
            {
                Debug.LogError("BiologyDataConfig没有设置");
            }
            InitComponents();
        }

        protected override void InitComponents()
        {
            if (biologyDataConfig)
            {
                InitMovement();
                InitJump();
                InitCrouch();
                InitDash();
            }
        }

        protected override void InitMovement()
        {
            if (biologyDataConfig.moveable)
            {
                moveController = new MoveController();
                moveController.InitMovement(this,biologyDataConfig.moveData);
            }
            
        }

        protected override void InitJump()
        {
            if (biologyDataConfig.jumpable)
            {
                moveController.CanJump(biologyDataConfig.jumpData);
            }
        }

        protected override void InitCrouch()
        {
            if (biologyDataConfig.crouchable)
            {
                moveController.CanCrouch(biologyDataConfig.crouchData);
            }
        }

        protected override void InitDash()
        {
            if (biologyDataConfig.dashable)
            {
                moveController.CanDash(biologyDataConfig.dashData);
            }
        }
    }
}

