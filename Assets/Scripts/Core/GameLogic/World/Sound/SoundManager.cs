using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace GameFrame
{
    /// <summary>
    /// 音效管理器
    /// </summary>
    public class SoundManager : MonoSingleton<SoundManager>,IController
    {
        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }
        
        
    }
}


