using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.Config
{
    [CreateAssetMenu(fileName = "GameSettingConfig",menuName ="配置/游戏设置")]
    public class GameSettingConfig : ScriptableObject
    {
        
        public int testLoadingTime;
    }
}

