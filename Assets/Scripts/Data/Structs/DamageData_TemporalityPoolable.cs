using GameFrame.World;
using QFramework;

namespace GameFrame
{
    public class DamageData_TemporalityPoolable : TemporalityData_Pool
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
        /// 基础元素伤害(没有计算抗性和倍率)
        /// </summary>
        private float basicDamage;

        public static DamageData_TemporalityPoolable Allocate()
        {
           return SafeObjectPool<DamageData_TemporalityPoolable>.Instance.Allocate();
        }

        /// <summary>
        /// 更新施加者
        /// </summary>
        /// <param name="enforcer"></param>
        public void UpdateEnforcer(WorldObj enforcer)
        {
            this.enforcer = enforcer;
        }

        /// <summary>
        /// 更新伤害
        /// </summary>
        /// <param name="basicDamage"></param>
        public void UpdateBasicDamage(float basicDamage)
        {
            this.basicDamage = basicDamage;
        }

        /// <summary>
        /// 更新元素伤害
        /// </summary>
        /// <param name="curLevel"></param>
        public void UpdateElementDamage(int curLevel)
        {
            ElementDamageData_Persistent elementData =
                enforcer.worldObjPropertyDataTemporality.GetElementDamageData(elementType);

            this.basicDamage = elementData.GetElementDamage(curLevel);
        }

        /// <summary>
        /// 更新受害者
        /// </summary>
        /// <param name="sufferer"></param>
        public void UpdateSufferer(WorldObj sufferer)
        {
            this.sufferer = sufferer;
        }

        /// <summary>
        /// 更新元素类型和是否要进行叠加
        /// </summary>
        /// <param name="elementType"></param>
        public void UpdateElementType(EElementType elementType,bool willAccElementLevel=false)
        {
            this.elementType = elementType;
            this.willOverlayElementLevel = willAccElementLevel;
        }

        /// <summary>
        /// 计算元素伤害
        /// 伤害: 基础伤害*施加者该元素伤害倍率/受害者伤害抗性
        /// </summary>
        /// <returns></returns>
        public void CaculateDamage()
        {
            sufferer.BeHarmed(this);
        }

        /// <summary>
        /// 计算最终伤害
        /// </summary>
        /// <returns></returns>
        public float CaculateFinalDamage()
        {
            float suffererElementResistance = sufferer.worldObjPropertyDataTemporality.GetElementDamageReductionRatio(elementType);
            float enforcerElementRatio= enforcer.worldObjPropertyDataTemporality.GetElementDamageAddition(elementType); 
            return basicDamage*enforcerElementRatio/suffererElementResistance;
        }

        public override void DeInitData()
        {
            
        }

        public override void OnRecycled()
        {
            
        }

        public override void Recycle2Cache()
        {
            SafeObjectPool<DamageData_TemporalityPoolable>.Instance.Recycle(this);
        }
    }
}