using System;
using GameFrame.Config;
using UnityEngine;
using QFramework;

namespace GameFrame.World
{
    public class PoolManager : MonoNetSingleton<PoolManager>,IController
    {
        public virtual IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }


        private void Start()
        {
            InitSafePool();
        }

        public void InitSafePool()
        {
            SafeObjectPool<SActionClipData_Temporality>.Instance.Init(50,30);
        }
        

        public GameObject LoadObjFromPool(EObjectPoolType poolType)
        {
            return null;
        }

        public void RecycleObj(EObjectPoolType poolType,GameObject targetObj)
        {
            
        }
    }
}

