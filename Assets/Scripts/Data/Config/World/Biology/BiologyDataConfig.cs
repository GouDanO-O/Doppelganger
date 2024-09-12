using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameFrame.Config
{
    [CreateAssetMenu(fileName = "BiologyDataConfig", menuName = "配置/世界/基础生物配置")]
    public class BiologyDataConfig : WorldObjDataConfig
    {
        public bool dashable;
        
        public SDashData dashData;
        
        public bool jumpable;
        
        public SJumpData jumpData;
        
        public bool crouchable;
        
        public SCrouchData crouchData;
    }
    
    /// <summary>
    /// 冲刺数据配置
    /// </summary>
    [Serializable]
    public struct SDashData
    {
        public float dashSpeed;

        public float dashCD;
        
    }
    
    /// <summary>
    /// 跳跃数据配置
    /// </summary>
    [Serializable]
    public struct SJumpData
    {
        public float jumpHeight;
        
        public bool canDoubleJump;
    }

    /// <summary>
    /// 蹲伏数据配置
    /// </summary>
    [Serializable]
    public struct SCrouchData
    {
        public float crouchSpeed;

        public float crouchReduceRatio;
    }
}

