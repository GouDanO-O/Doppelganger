using QFramework;

namespace GameFrame
{
    public class ProjectileTriggerDamageData_TemporalityPoolable : TriggerDamageData_TemporalityPoolable
    {
        /// <summary>
        /// 当前飞行速度
        /// </summary>
        public float curFlySpeed;

        /// <summary>
        /// 当前飞行距离
        /// </summary>
        public float curFlyDistance;
        
        /// <summary>
        /// 最大飞行距离
        /// </summary>
        public float maxFlyDistance;

        /// <summary>
        /// 当前飞行高度
        /// </summary>
        public float curFlyHeight;

        /// <summary>
        /// 飞行最高点
        /// </summary>
        public float maxFlyHeight;
        
        public static ProjectileTriggerDamageData_TemporalityPoolable Allocate()
        {
            return SafeObjectPool<ProjectileTriggerDamageData_TemporalityPoolable>.Instance.Allocate();
        }
    }
}