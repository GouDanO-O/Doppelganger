using UnityEngine;

namespace GameFrame
{
    /// <summary>
    /// 持久化的数据--生命周期内会一直存在
    /// </summary>
    public interface IData_Persistence
    {
        public void ClearData();
        
        public void SaveData();
    }
}

