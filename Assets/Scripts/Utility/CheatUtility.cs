using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace GameFrame
{
    public class CheatModule
    {
        public string Name { get; set; } 
        public Action CheatAction { get; set; }  

        public CheatModule(string name, System.Action action)
        {
            Name = name;
            CheatAction = action;
        }

        
        public void Execute()
        {
            CheatAction?.Invoke();
        }
    }
    
    public class CheatUtility : BasicToolUtility
    {
        
        private bool isShowingCheatWindow = false;

        private List<CheatModule> cheatModules = new List<CheatModule>();

        // GUI布局滚动条
        private Vector2 scrollPosition;

        public void AddCheatModule(string name="", System.Action action=null)
        {
            cheatModules.Add(new CheatModule(name, action));
        }
        
        protected override void DrawGUI()
        {
            if(!isShowing)
                return;
            
            GUILayout.BeginArea(new Rect(10, 40, 300, 400), GUI.skin.box);
            GUILayout.Label("作弊系统", GUI.skin.label);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(300));

            foreach (var cheat in cheatModules)
            {
                GUILayout.BeginHorizontal();

                // 显示作弊功能的名称和描述
                GUILayout.Label(cheat.Name, GUILayout.Width(200));
                    
                // 按钮执行作弊功能
                if (GUILayout.Button("激活"))
                {
                    cheat.Execute();
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }
    } 
}

