using System;
using System.Collections.Generic;
using GameFrame.World;
using QFramework;
using Random = UnityEngine.Random;

namespace GameFrame
{
    /// <summary>
    /// 造成的伤害块--用池子处理和回收
    /// </summary>
    public class TriggerDamageData_TemporalityPoolable : TemporalityData_Pool
    {
       /// <summary>
        /// 施加者
        /// </summary>
        public WorldObj enforcer { get; private set; }

        /// <summary>
        /// 受害者
        /// </summary>
        public WorldObj sufferer { get; private set; }

        /// <summary>
        /// 是否要进行叠加元素伤害
        /// 如果是元素自身造成的伤害,则不会进行自我叠加
        /// 如果是其他来源造成的元素伤害,则会进行叠加
        /// </summary>
        public bool willOverlayElementLevel { get; private set; }
        
        /// <summary>
        /// 收到的伤害的元素类型
        /// </summary>
        public EElementType elementType { get; private set; }
        
        /// <summary>
        /// 基础伤害(没有计算抗性和倍率)
        /// </summary>
        private float basicDamage;

        /// <summary>
        /// 从池子中进行分配
        /// </summary>
        /// <returns></returns>
        public static TriggerDamageData_TemporalityPoolable Allocate()
        {
           return SafeObjectPool<TriggerDamageData_TemporalityPoolable>.Instance.Allocate();
        }

        /// <summary>
        /// 更新施加者
        /// </summary>
        /// <param name="enforcer"></param>
        public virtual void UpdateEnforcer(WorldObj enforcer)
        {
            this.enforcer = enforcer;
        }

        /// <summary>
        /// 更新伤害
        /// </summary>
        /// <param name="basicDamage"></param>
        public virtual void UpdateBasicDamage(float basicDamage,EElementType elementType = EElementType.None)
        {
            this.basicDamage = basicDamage;
            this.elementType = elementType;
        }

        /// <summary>
        /// 更新元素伤害
        /// </summary>
        /// <param name="curLevel"></param>
        public virtual void UpdateElementDamage(int curLevel)
        {
            ElementDamageData_Persistent elementData =
                enforcer.worldObjPropertyDataTemporality.GetElementDamageData(elementType);

            this.basicDamage = elementData.GetElementDamage(curLevel);
        }

        /// <summary>
        /// 更新受害者
        /// </summary>
        /// <param name="sufferer"></param>
        public virtual void UpdateSufferer(WorldObj sufferer)
        {
            this.sufferer = sufferer;
        }

        /// <summary>
        /// 更新元素类型和是否要进行叠加
        /// </summary>
        /// <param name="elementType"></param>
        public virtual void UpdateElementType(EElementType elementType,bool willAccElementLevel=false)
        {
            this.elementType = elementType;
            this.willOverlayElementLevel = willAccElementLevel;
        }

        /// <summary>
        /// 对受害者造成伤害
        /// </summary>
        /// <returns></returns>
        public virtual void HarmedSufferer()
        {
            sufferer.BeHarmed(this);
        }

        /// <summary>
        /// 计算最终伤害
        /// </summary>
        /// <returns>根据元素类型来计算伤害值</returns>
        public virtual float CaculateFinalDamage()
        {
            if (elementType == EElementType.None)
            {
                return CaculateFinalDamage_Normal();
            }
            else if (elementType == EElementType.TrueInjury)
            {
                return CaculateFinalDamage_TrueInjury();
            }
            else
            {
                return CaculateFinalDamage_Element();
            }
        }
        
        /// <summary>
        /// 计算最终伤害--普通
        /// </summary>
        /// <returns>基础伤害(这个基础伤害是由当前层数的伤害倍率*基础伤害) * 施加者的伤害伤害倍率 / 受害者的伤害抗性</returns>
        protected virtual float CaculateFinalDamage_Normal()
        {
            float suffererElementResistance = sufferer.worldObjPropertyDataTemporality.GetDamageReductionRatio();
            float enforcerElementRatio= enforcer.worldObjPropertyDataTemporality.GetDamageAddition(); 
            return basicDamage * enforcerElementRatio / suffererElementResistance;
        }

        /// <summary>
        /// 计算最终伤害--真实伤害
        /// </summary>
        /// <returns>基础伤害就是直接伤害,不计算伤害倍率和伤害抗性</returns>
        protected virtual float CaculateFinalDamage_TrueInjury()
        {
            return basicDamage;
        }

        /// <summary>
        /// 计算最终伤害--元素类型
        /// </summary>
        /// <returns>基础伤害(这个基础伤害是由当前层数的元素伤害倍率*基础伤害) * 施加者的元素伤害倍率 / 受害者的元素抗性(可以在上面再叠加一层伤害抗性)</returns>
        protected virtual float CaculateFinalDamage_Element()
        {
            float suffererElementResistance = sufferer.worldObjPropertyDataTemporality.GetElementDamageReductionRatio(elementType);
            float enforcerElementRatio = enforcer.worldObjPropertyDataTemporality.GetElementDamageAddition(elementType); 
            return basicDamage * enforcerElementRatio / suffererElementResistance;
        }

        public override void DeInitData()
        {
            enforcer = null;
            sufferer = null;
            elementType = EElementType.None;
            basicDamage = 0;
            willOverlayElementLevel = false;
        }

        public override void OnRecycled()
        {
            DeInitData();
        }

        public override void Recycle2Cache()
        {
            SafeObjectPool<TriggerDamageData_TemporalityPoolable>.Instance.Recycle(this);
        }
    }
}