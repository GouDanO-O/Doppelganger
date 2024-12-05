using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using QFramework;
using UnityEngine;

namespace GameFrame.World
{
    /// <summary>
    /// 变形控制器
    /// </summary>
    public class DeformationController : AbstractController,IDeformation
    {
        public WorldObj owner;

        public float lastDeformationEnergy { get; set; }

        public float maxDeformationEnergy { get;protected set; }

        public override void InitData(WorldObj owner)
        {
            base.InitData(owner);
        }
        
        /// <summary>
        /// 检查是否有足够的变身能量
        /// </summary>
        /// <param name="energy"></param>
        /// <returns></returns>
        public bool CheckHasEnoughEnergy(float energy)
        {
            if (lastDeformationEnergy - energy >= 0)
            {
                lastDeformationEnergy -= energy;
                return true;
            }

            return false;
        }

        public void TriggerDeformationAppearance(WorldObjDataConfig deformationData)
        {
            if (CheckHasEnoughEnergy(deformationData.CostDeformationEnergy.x))
            {
                
            }
        }

        public void TriggerDeformationCompletely(WorldObjDataConfig deformationData)
        {
            if (CheckHasEnoughEnergy(deformationData.CostDeformationEnergy.y))
            {
                
            }
        }

        public override void DeInitData()
        {
            
        }
    } 
}

