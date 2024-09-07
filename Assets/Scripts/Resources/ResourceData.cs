using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
{
    public class ResourceData : AbstractModel
    {
        public int loadedCount { get; private set; }

        private int maxLoadCount = 2;

        protected override void OnInit()
        {
            var loader=this.GetUtility<ResoucesUtility>();
            loader.InitLoader();
        }


        /// <summary>
        /// 每加载一个就进行检测
        /// </summary>
        private void LoadCheck()
        {
            loadedCount++;
            if (loadedCount == maxLoadCount)
            {
                
            }
        }

        private void LoadCheck(string name)
        {
            loadedCount++;
            if (loadedCount == maxLoadCount)
            {
                
            }
            Debug.Log("加载数据成功:" + name);
        }

    }
}

