namespace GameFrame
{
    /// <summary>
    /// 具体元素块中的数据
    /// 用来给元素管理器去计算每个元素块的生命周期
    /// </summary>
    public struct SElementTriggerData
    {
        public int curLevel;

        public int maxLevel;
        
        private float maxDuration;
        
        private float lastInterval;

        private float intervalTime;

        private float lastDuration;

        /// <summary>
        /// 设置元素的触发时间
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="intervalTime"></param>
        public void SetElementTriggerTime(float duration, float intervalTime)
        {
            this.maxDuration = duration;
            this.lastDuration = duration;
            this.lastInterval = intervalTime;
            this.intervalTime = intervalTime;
        }

        /// <summary>
        /// 更新间隔触发时间
        /// </summary>
        /// <param name="intervalTime"></param>
        public void UpdateIntervalTime(float intervalTime)
        {
            this.intervalTime = intervalTime;
        }

        /// <summary>
        /// 更新持续时间
        /// </summary>
        /// <param name="duration"></param>
        public void UpdateDuration(float duration)
        {
            if (maxDuration < duration)
            {
                this.maxDuration = duration;
                this.lastDuration = duration;
            }
        }
        
        /// <summary>
        /// 计算间隔
        /// </summary>
        /// <param name="deltaTime"></param>
        public void CaculateInterval(float deltaTime)
        {
            lastInterval -= deltaTime;
            lastDuration -= deltaTime;
            if (lastInterval <= 0)
            {
                lastInterval = intervalTime;
            }
        }
        
        /// <summary>
        /// 设置等级
        /// </summary>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public int SetLevel(int maxLevel)
        {
            if (this.maxLevel < maxLevel)
            {
                this.maxLevel = maxLevel;
            }

            if (maxLevel == 0)
            {
                this.curLevel = 0;
            }

            return curLevel;
        }
        
        /// <summary>
        /// 增加等级
        /// </summary>
        /// <returns></returns>
        public int AddLevel()
        {
            curLevel ++;
            if (curLevel >= maxLevel)
            {
                curLevel = maxLevel;
            }

            return curLevel;
        }

        /// <summary>
        /// 查看生命周期是否结束
        /// </summary>
        /// <returns></returns>
        public bool IsTimeOver()
        {
            return lastDuration <= 0;
        }
    }
}