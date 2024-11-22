using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QFramework;
using UnityEngine;

namespace GameFrame
{
    public class UnityMainThreadDispatcher : MonoSingleton<UnityMainThreadDispatcher>
    {
        private readonly Queue<Action> executionQueue = new Queue<Action>();

        public void Update()
        {
            lock (executionQueue)
            {
                while (executionQueue.Count > 0)
                {
                    executionQueue.Dequeue().Invoke();
                }
            }
        }

        public void Enqueue(Action action)
        {
            lock (executionQueue)
            {
                executionQueue.Enqueue(action);
            }
        }
        
        /// <summary>
        /// 开始一个异步任务并在完成后执行回调
        /// </summary>
        /// <param name="taskFunc">要执行的异步任务</param>
        /// <param name="onComplete">任务完成后的回调</param>
        public async Task StartAsyncTask(float timeDelay, Action onComplete = null)
        {
            int delayInMilliseconds = Mathf.RoundToInt(timeDelay * 1000);
            await Task.Delay(delayInMilliseconds); // 模拟延时操作
            onComplete?.Invoke();
        }
    }
}