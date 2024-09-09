using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace  GameFrame
{
   public class LogUtility : MonoBehaviour, IUtility
   {
      [SerializeField]private bool willShowLogs=true;

      public GUIStyle LogStyle;
      
      private bool isShowingLogs;

      private Vector2 m_scroll;

      private string m_logs;

      private void Awake()
      {
         if (!willShowLogs)
         {
            gameObject.Hide();
         }
      }

      private void Start()
      {
         Main.Interface.RegisterUtility(this);
      }


      private void OnEnable()
      {
         isShowingLogs = true;
         Application.logMessageReceived += ShowLogs;
      }
      
         
      private void ShowLogs(string logString, string stackTrace, LogType type)
      {
           string[] splitStr=logString.Split('\n');
           string strType = "";
           switch (type)
           {
              case LogType.Error:
                 strType = "<color=red>" + type.ToString() + "</color>";
                 break;
              case   LogType.Log:
                 strType = "<color=white>" + type.ToString() + "</color>";
                 break;
              case   LogType.Warning:
                 strType = "<color=yellow>" + type.ToString() + "</color>";
                 break; 
              default:
                 
                 break;
           }
           
           strType = strType.Length == 0 ? type.ToString() : strType;//如果没有日志类型，那么就赋值一个类型
           string strLog = "[—" + strType + "—]: \n" + logString + "\n" + splitStr[0] + "\t\n" + splitStr[1] + "\t\n\t\t<——————分割线——————>\n";
           m_logs = strLog + m_logs;
           if (m_logs.Length > 1024 * 8)
           {//如果字符超出长度是会报错的，所以超出限制一下长度
              m_logs = "";
              m_logs = strLog + m_logs;
           }
      }

      private void OnGUI()
      {
         if(!willShowLogs)
            return;

         if (isShowingLogs)
         {
            m_scroll = GUILayout.BeginScrollView(m_scroll);
            if (GUILayout.Button("清空日志"))
            {
               m_logs = "";
            }
            
            GUILayout.Label(m_logs, LogStyle);
            GUILayout.EndScrollView();
         }
      }
   }
}

 



