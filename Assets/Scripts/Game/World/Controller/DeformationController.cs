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
        /// <summary>
        /// 仅变化外貌,技能还是本体的技能
        /// </summary>
        /// <param name="deformationData"></param>
        void TriggerDeformationAppearance(WorldObjDataConfig deformationData);
        
        /// <summary>
        /// 完全变形,技能和外貌都以新物体为准
        /// </summary>
        /// <param name="deformationData"></param>
        void TriggerDeformationCompletely(WorldObjDataConfig deformationData);
    }

    /// <summary>
    /// 变形控制器
    /// </summary>
    public class DeformationController : IDeformation
    {
        public WorldObj owner;

        public float lastDeformationEnergy { get; set; }

        public float maxDeformationEnergy { get;protected set; }
        
        public DeformationController(WorldObj owner)
        {
            this.owner = owner;
            this.lastDeformationEnergy = 0;
        }
        
        public DeformationController(WorldObj owner,float maxDeformationEnergy)
        {
            this.owner = owner;
            this.lastDeformationEnergy = 0;
            this.maxDeformationEnergy = maxDeformationEnergy;
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
            if (CheckHasEnoughEnergy(deformationData.costDeformationEnergy.x))
            {
                
            }
        }

        public void TriggerDeformationCompletely(WorldObjDataConfig deformationData)
        {
            if (CheckHasEnoughEnergy(deformationData.costDeformationEnergy.y))
            {
                
            }
        }
    } 
}

