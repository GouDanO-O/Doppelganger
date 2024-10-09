using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using GameFrame.Net;
using QFramework;
using UnityEngine;

namespace GameFrame.World
{
    /// <summary>
    /// 怪物主类
    /// </summary>
    public class Monster : Biology
    {
        public MonsterDataConfig monsterDataConfig { get;protected set; }
        
        public DeformationController deformationController { get;protected set; }

        public override void Init()
        {
            if (thisDataConfig is MonsterDataConfig)
            {
                try
                {
                    monsterDataConfig = thisDataConfig as MonsterDataConfig;
                }
                catch (Exception e)
                {
                    Debug.Log("转换失败");
                    return;
                }
            }
            else
            {
                Debug.LogError("MonsterDataConfig没有设置");
            }
            InitComponents();
        }

        protected override void InitConfig()
        {
            if (monsterDataConfig)
            {
                InitHealthy();
                InitSkill();
            }
        }

        protected override void InitHealthy()
        {
            if (monsterDataConfig.healthyable)
            {
                healthyController = new HealthyController();
                healthyController.InitHealthyer(monsterDataConfig.healthyData);
            }
        }

        protected override void InitSkill()
        {
            if (monsterDataConfig.skillTree)
            {
                skillController = new SkillController();
                skillController.Init(monsterDataConfig.skillTree,this);
            }
        }
    }
}

