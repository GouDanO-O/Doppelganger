using System;
using System.Collections.Generic;
using GameFrame.Config;
using QFramework;
using UnityEngine;

namespace GameFrame.World
{
    public class Tester : MonoBehaviour
    {
        public List<SkillActionClip_BasicData> basicData;

        private void Start()
        {
            ActionKit.Repeat(-1).Delay(0.3f, () =>
            {
                TimeDelayCheck();
            }).StartGlobal();
        }

        private void TimeDelayCheck()
        {
            for (int i = 0; i < basicData.Count; i++)
            {
                SkillActionClip_BasicData data = basicData[i];
                data.InitExecution();
            }
        }
    }
}

