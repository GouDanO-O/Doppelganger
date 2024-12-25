using GameFrame.World;

namespace GameFrame
{
    public interface IExecuteLogic
    {
        public WorldObj ownerObj { get; set; }

        /// <summary>
        /// 开始前的检测
        /// 一般用来进行赋值
        /// </summary>
        /// <param name="owner"></param>
        public void InitExecution(WorldObj owner);
        
        /// <summary>
        /// 开始执行的行为
        /// 适合用来执行一些行为开始前就做的事情
        /// </summary>
        public void StartExecute();
        
        /// <summary>
        /// 结束执行行为
        /// 适合用来在行为结束时触发的事件
        /// </summary>
        public void EndExecute();

        /// <summary>
        /// 重设行为
        /// 一般在回收这个行为时做临时变量的去初始化
        /// </summary>
        public void ResetExecute();
    }
}