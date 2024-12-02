using System;
using System.Collections.Generic;
using GameFrame.Config;
using GameFrame.UI;
using UnityEngine;
using QFramework;
using Sirenix.OdinInspector;

namespace GameFrame.World
{
    [LabelText("对象池所属类型")]
    public enum EObjectPoolType
    {
                
    }
    
    public class PoolManager : MonoNetSingleton<PoolManager>
    {
        public SimpleObjectPool<HealthyStatusFollower> healthyStatusFollower_Pool;
        
        private void Awake()
        {
            InitSafePool();
        }

        public void InitSafePool()
        {
            SafeObjectPool<ActionClipData_TemporalityPoolable>.Instance.Init(50,30);
        }

        public void InitNormalPool()
        {
            InitHealthyStatusFollower_Pool();
        }

        public void InitHealthyStatusFollower_Pool()
        {
            GameObject healthyStatusFollowerObj =
                this.GetModel<ResourcesModel>().UIPrefabsDataConfig.HealthyStatusFollowPrefab;
            
            Transform statusRoot=UIRoot.Instance.Common.Find("HealthyStatusFollowerRoot");
            healthyStatusFollower_Pool =  new SimpleObjectPool<HealthyStatusFollower>(() =>
            {
                GameObject obj = healthyStatusFollowerObj.InstantiateWithParent(statusRoot);
                obj.Hide();
                HealthyStatusFollower follower=obj.GetComponent<HealthyStatusFollower>();
                follower.Init();
                return follower;
            }, (spawnedObj) =>
            {
                
            }, 30);
        }

        public void InitElementHarmedParticleObjectPool()
        {
            
        }
        
        public GameObject LoadObjFromPool(EObjectPoolType poolType)
        {
            return null;
        }

        public void RecycleObj(EObjectPoolType poolType,GameObject targetObj)
        {
            
        }
        
        private SimpleObjectPool<GameObject> SpawnDetailPool(GameObject prefab,Transform father,int count=30)
        {
            SimpleObjectPool<GameObject> pool = new SimpleObjectPool<GameObject>(() =>
            {
                GameObject obj = prefab.InstantiateWithParent(father);
                obj.Hide();
                return obj;
            }, (spawnedObj) =>
            {
                spawnedObj.Hide();
            }, count);
            return pool;    
        }
        
        private SimpleObjectPool<T> SpawnDetailPool<T>(GameObject prefab,Transform father,int count=30)
        {
            SimpleObjectPool<T> pool = new SimpleObjectPool<T>(() =>
            {
                GameObject obj = prefab.InstantiateWithParent(father);
                obj.Hide();
                return obj.GetComponent<T>();
            }, (spawnedObj) =>
            {
                
            }, count);
            return pool;    
        }
    }
}

