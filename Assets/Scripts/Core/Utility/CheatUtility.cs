using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace GameFrame
{
    public class CheatUtility : BasicToolUtility_Mono
    {
        private CheatData_Model _cheatDataModel;

        // GUI布局滚动条
        private Vector2 scrollPosition;

        protected override void InitUtility()
        {
            Main.Interface.RegisterUtility(this);
            _cheatDataModel = new CheatData_Model();
            Main.Interface.RegisterModel(_cheatDataModel);
        }

        protected override void DrawGUI()
        {
            if(!isShowing)
                return;
            
            GUILayout.BeginArea(new Rect(10, 40, 300, 400), GUI.skin.box);
            GUILayout.Label("作弊系统", GUI.skin.label);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(300));

            Dictionary<string,CheaterData> cheaters = _cheatDataModel.GetCheaterDatas();
            foreach (var cheat in cheaters)
            {
                GUILayout.BeginHorizontal();

                // 显示作弊功能的名称和描述
                GUILayout.Label(cheat.Value.Name, GUILayout.Width(200));
                    
                // 按钮执行作弊功能
                if (GUILayout.Button("激活"))
                {
                    cheat.Value.Execute();
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
            GUILayout.EndArea();
        }
    } 
}

