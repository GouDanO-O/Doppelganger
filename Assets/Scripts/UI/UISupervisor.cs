using GameFrame;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.UI
{
    /// <summary>
    /// UI管理类
    /// </summary>
    public class UISupervisor : AbstractSystem
    {
        protected override void OnInit()
        {
            
        }
        
        /// <summary>
        /// 进入菜单
        /// </summary>
        public void EnterMenu()
        {
            Main.Interface.GetUtility<CoroutineUtility>().SpawnUI_Async<UI_MenuPanel>();
        }

        /// <summary>
        /// 进入游戏
        /// </summary>
        public void EnterGame()
        {
            
        }
    }
}

