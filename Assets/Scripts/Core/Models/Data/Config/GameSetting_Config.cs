using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    [CreateAssetMenu(fileName = "GameSettingConfig",menuName ="配置/游戏设置")]
    public class GameSetting_Config : SerializedScriptableObject
    {
        [LabelText("默认字体")]
        public Font CustomFont; 
        
        [LabelText("鼠标灵敏度")]
        public float MouseSensitivity;
    }
}

