using QFramework;
using UnityEngine.Pool;

namespace GameFrame
{
    /// <summary>
    /// 临时对象数据--非池
    /// </summary>
    public abstract class TemporalityData
    {
        public abstract void DeInitData();
    }

    /// <summary>
    /// 临时对象数据--池
    /// 需要实现Allocate
    /// </summary>
    public abstract class TemporalityData_Pool :  TemporalityData, IPoolable, IPoolType
    {
        public bool IsRecycled { get; set; }

        public abstract void OnRecycled();
        
        public abstract void Recycle2Cache();
    }
}