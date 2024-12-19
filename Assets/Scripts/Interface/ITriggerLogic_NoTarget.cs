namespace GameFrame
{
    public interface ITriggerLogic_NoTarget
    {
        /// <summary>
        /// 无对象的触发
        /// 触发开始
        /// </summary>
        public void OnTriggerStart();

        /// <summary>
        /// 无对象的触发
        /// 触发结束
        /// </summary>
        public void OnTriggerEnd();

    }
}