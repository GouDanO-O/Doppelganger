using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace GameFrame
{
    public class ToolsUtilityManager : MonoBehaviour,IController
    {
        public bool willShowCheatWindow = false;
        
        public bool willShowLogWindow = true;
        
        private bool canShowGUI = false;
        
        private CheatUtility cheatUtility;
        
        private LogUtility logUtility;
        
        private CoroutineUtility coroutineUtility;
        
        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }

        private void Awake()
        {
            InitData();
        }

        protected void InitData()
        {
            if (willShowLogWindow)
            {
                logUtility = gameObject.AddComponent<LogUtility>();
                canShowGUI = true;
            }
            
            if (willShowCheatWindow)
            {
                cheatUtility = gameObject.AddComponent<CheatUtility>();
                canShowGUI = true;
            }
            
            coroutineUtility = gameObject.AddComponent<CoroutineUtility>();
        }

        private void OnGUI()
        {
            DrawGUI();
        }

        protected void DrawGUI()
        {
            if(!canShowGUI)
                return;            
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
        
    }
}

