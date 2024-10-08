using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using QFramework;
using UnityEngine;

namespace GameFrame.World
{
    /// <summary>
    /// 变形
    /// </summary>
    public interface IDeformation
    {
        void TriggerDeformation(WorldObjDataConfig deformationData);
    }

    /// <summary>
    /// 变形控制器
    /// </summary>
    public class DeformationController : IDeformation
    {
        public WorldObj owner;
        
        public void TriggerDeformation(WorldObjDataConfig deformationData)
        {
            
        }
    } 
}

