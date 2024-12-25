using GameFrame.World;

namespace GameFrame
{
    public interface ITriggerLogic_HasTarget
    {
        
        /// <summary>
        /// 有受害对象的触发
        /// 触发开始
        /// </summary>
        /// <param name="suffer"></param>
        public void OnTriggerStart(WorldObj suffer);
        
        /// <summary>
        /// 有受害对象的触发
        /// 触发结束
        /// </summary>
        /// <param name="suffer"></param>
        public void OnTriggerEnd(WorldObj suffer);
    }
}