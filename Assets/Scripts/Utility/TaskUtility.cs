using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFrame
{
    public class TaskUtility : BasicToolUtility
    {
        public override void InitUtility()
        {
            Main.Interface.RegisterUtility(this);
        }
        
        /// <summary>
        /// 开始一个异步任务并在完成后执行回调
        /// </summary>
        /// <param name="taskFunc">要执行的异步任务</param>
        /// <param name="onComplete">任务完成后的回调</param>
        public async Task StartAsyncTask(float timeDelay, Action onComplete=null)
        {
            int delayInMilliseconds = Mathf.RoundToInt(timeDelay * 1000);
            await Task.Delay(delayInMilliseconds); // 模拟延时操作
            onComplete?.Invoke();
        }


    }
}