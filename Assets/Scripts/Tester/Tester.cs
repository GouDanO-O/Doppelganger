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
            Check();
        }

        void Check()
        {
            ActionKit.Repeat(-1).Delay(0.3f, () =>
            {
                UnityEngine.Profiling.Profiler.BeginSample("TimeDelayCheck");
                TimeDelayCheck();
                UnityEngine.Profiling.Profiler.EndSample();
            }).StartGlobal();
        }

        private void TimeDelayCheck()
        {

        }
    }
}

