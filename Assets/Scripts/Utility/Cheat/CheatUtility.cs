using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace GameFrame
{
    public class CheatCommand : AbstractCommand
    {
        protected string Name;

        protected Action CheatAction;

        public CheatCommand(string name, Action action)
        {
            Name = name;
            CheatAction = action;
        }
        
        protected override void OnExecute()
        {
            this.GetModel<CheatModel>().AddCheatModule(Name,CheatAction);
        }
    }
    
    public class CheatUtility : BasicToolUtility
    {
        private CheatModel cheatModel;

        // GUI布局滚动条
        private Vector2 scrollPosition;

        protected override void InitUtility()
        {
            base.InitUtility();
            cheatModel = new CheatModel();
            Main.Interface.RegisterModel(cheatModel);
        }

        protected override void DrawGUI()
        {
            if(!isShowing)
                return;
            
            GUILayout.BeginArea(new Rect(10, 40, 300, 400), GUI.skin.box);
            GUILayout.Label("作弊系统", GUI.skin.label);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(300));

            List<CheaterData> cheaters = cheatModel.GetCheaterDatas();
            foreach (var cheat in cheaters)
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

