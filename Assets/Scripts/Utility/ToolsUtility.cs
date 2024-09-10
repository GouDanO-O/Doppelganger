using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace GameFrame
{
    public class ToolsUtility : MonoSingleton<ToolsUtility>,IUtility
    {
        public bool willShowCheatWindow = false;
        
        public bool willShowLogWindow = true;
        
        private CheatUtility cheatUtility;
        
        private LogUtility logUtility;
        
        private void Awake()
        {
            Main.Interface.RegisterUtility(this);
        }

        private void Start()
        {
            if (willShowLogWindow)
            {
                logUtility = gameObject.AddComponent<LogUtility>();
            
            }

            if (willShowCheatWindow)
            {
                cheatUtility = gameObject.AddComponent<CheatUtility>();
            }
        }

        private void OnGUI()
        {
            int showCount = -1;
            if (willShowLogWindow)
            {
                showCount++;
                if (GUI.Button(new Rect(20+showCount*150,0,120,30),logUtility.isShowing ? "关闭日志系统" : "打开日志系统"))
                {
                    logUtility.CheckButtonWillShow();
                    if (logUtility.isShowing && cheatUtility)
                    {
                        if (cheatUtility.isShowing)
                        {
                            cheatUtility.CheckButtonWillShow();
                        }
                    }
                }
            }

            if (willShowCheatWindow)
            {
                showCount++;
                if (GUI.Button(new Rect(20+showCount*150,0,120,30),cheatUtility.isShowing ? "关闭作弊系统" : "打开作弊系统"))
                {
                    cheatUtility.CheckButtonWillShow();
                    if (cheatUtility.isShowing && logUtility)
                    {
                        if (logUtility.isShowing)
                        {
                            logUtility.CheckButtonWillShow();
                        }
                    }
                } 
            }

        }

        /// <summary>
        /// 异步生成UI
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level"></param>
        public void SpawnUI_Async<T>(UILevel level=UILevel.Common, Action complete = null) where T : UIPanel
        {
            StartCoroutine(IemSpawnUI_Async<T>(level, complete));
        }

        protected IEnumerator IemSpawnUI_Async<T>(UILevel level=UILevel.Common, Action complete=null) where T : UIPanel
        {
            yield return UIKit.OpenPanelAsync<T>(level);
            complete?.Invoke();
        }

        /// <summary>
        /// 将秒转换成时分秒
        /// </summary>
        /// <param name="totalSeconds"></param>
        /// <returns></returns>
        public string ConvertToTimeFormat(float totalSeconds=0)
        {
            int hours = Mathf.FloorToInt(totalSeconds / 3600);
            int minutes = Mathf.FloorToInt((totalSeconds % 3600) / 60);
            int seconds = Mathf.FloorToInt(totalSeconds % 60);

            return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }

        /// <summary>
        /// 开始一个协程
        /// </summary>
        /// <param name="routine"></param>
        /// <returns></returns>
        public void StartRoutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }

        /// <summary>
        /// 开始一个协程
        /// </summary>
        /// <param name="routine"></param>
        /// <returns></returns>
        public Coroutine GetAndStartRoutine(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }

        /// <summary>
        /// 开始一个协程
        /// </summary>
        /// <param name="routine"></param>
        /// <returns></returns>
        public void StartRoutine_Action(IEnumerator routine)
        {
            ActionKit.Coroutine(()=>routine).Start(this);
        }

        /// <summary>
        /// 开始一个协程
        /// </summary>
        /// <param name="routine"></param>
        /// <returns></returns>
        public IAction GetAndStartRoutine_Action(IEnumerator routine)
        {
            return ActionKit.Coroutine(() => routine);
        }

        // 停止协程
        public void StopRoutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }

        // 停止协程
        public void StopRoutine(IActionController coroutine)
        {
            if (coroutine != null)
            {
                coroutine.Deinit();
                coroutine.Recycle();
            }
        }

        // 停止所有协程
        public void StopAllRoutines()
        {
            StopAllCoroutines();
        }
    }
}

