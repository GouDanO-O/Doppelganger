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
        
        protected MoveController moveController;
        
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
                    return;
                }

                InitComponents();
            }
            else
            {
                Debug.LogError("MonsterDataConfig is not set!");
            }
        }

        protected override void InitComponents()
        {
            if (biologyDataConfig.moveable)
            {
                moveController = new MoveController();
                moveController.InitMovement(this,biologyDataConfig.moveData);
            }
        }
    }
}

