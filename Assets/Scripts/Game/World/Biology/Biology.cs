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
        public BiologyDataConfig biologyDataConfig { get; protected set; }
        
        public override void Init()
        {
            if (thisDataConfig is BiologyDataConfig && !biologyDataConfig)
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

        protected override void InitConfig()
        {
            if (biologyDataConfig)
            {
                InitHealthy();
                InitSkill();
            }
        }
        
        protected override void InitHealthy()
        {
            if (biologyDataConfig.healthyable)
            {
                healthyController = new HealthyController();
                healthyController.InitHealthyer(biologyDataConfig.healthyData);
            }
        }
        
        protected override void InitSkill()
        {
            if (biologyDataConfig.skillTree)
            {
                skillController = new SkillController();
                skillController.Init(biologyDataConfig.skillTree,this);
            }
        }
    }
}

