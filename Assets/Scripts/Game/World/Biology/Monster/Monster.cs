using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using QFramework;
using UnityEngine;

namespace GameFrame.World
{
    /// <summary>
    /// 怪物主类
    /// </summary>
    public class Monster : Biology
    {
        private MonsterDataConfig monsterDataConfig;
        
        private void Awake()
        {
            InitData();
        }

        public override void InitData()
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

                InitComponents();
            }
            else
            {
                Debug.LogError("MonsterDataConfig is not set!");
            }
        }

        protected override void InitComponents()
        {
            if (monsterDataConfig.moveable)
            {
                
            }
        }
    }
}

