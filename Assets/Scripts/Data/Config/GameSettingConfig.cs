using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.Config
{
    [CreateAssetMenu(fileName = "GameSettingConfig",menuName ="配置/游戏设置")]
    public class GameSettingConfig : ScriptableObject
    {
        [Header("默认字体")]
        public Font customFont; 
    }
}

