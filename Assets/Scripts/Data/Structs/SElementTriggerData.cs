namespace GameFrame
{
    public struct SElementTriggerData
    {
        public int curLevel;

        public int maxLevel;
        
        private float maxDuration;
        
        private float lastInterval;

        private float intervalTime;

        private float lastDuration;

        public void SetElementTriggerTime(float duration, float intervalTime)
        {
            this.maxDuration = duration;
            this.lastDuration = duration;
            this.lastInterval = intervalTime;
            this.intervalTime = intervalTime;
        }

        public void UpdateIntervalTime(float intervalTime)
        {
            this.intervalTime = intervalTime;
        }

        public void UpdateDuration(float duration)
        {
            if (maxDuration < duration)
            {
                this.maxDuration = duration;
                this.lastDuration = duration;
            }
        }
        
        public void CaculateInterval(float deltaTime)
        {
            lastInterval -= deltaTime;
            lastDuration -= deltaTime;
            if (lastInterval <= 0)
            {
                lastInterval = intervalTime;
            }
        }
        
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
        
        public int AddLevel()
        {
            curLevel ++;
            if (curLevel >= maxLevel)
            {
                curLevel = maxLevel;
            }

            return curLevel;
        }

        public bool IsTimeOver()
        {
            return lastDuration <= 0;
        }
    }
}