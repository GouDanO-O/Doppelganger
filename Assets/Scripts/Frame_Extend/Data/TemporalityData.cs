﻿using QFramework;
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


}