using QFramework;
using UnityEngine;

namespace GameFrame
{
    /// <summary>
    /// 持久化对象---非池
    /// </summary>
    public abstract class PersistentData
    {
        public virtual void SaveData()
        {
            
        }
    }
    
    /// <summary>
    /// 持久化对象--池
    /// 需要实现Allocate
    /// </summary>
    public abstract class PersistentData_Pool: PersistentData,IPoolable,IPoolType
    {
        public bool IsRecycled { get; set; }
        
        public abstract void OnRecycled();


        public abstract void Recycle2Cache();
    }
}

