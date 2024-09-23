using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace GameFrame.Config
{
    [CreateAssetMenu(fileName = "BiologyDataConfig", menuName = "配置/世界/基础生物配置")]
    public class BiologyDataConfig : WorldObjDataConfig
    {
        [LabelText("是否能够进行移动(不会接受输入数据,由物体本身控制移动)")]
        public bool moveable;
        
        [ShowIf("moveable"),LabelText("移动数据")]
        public SMoveData moveData;
        
        [ShowIf("moveable"),LabelText("是否能够进行闪烁")]
        public bool dashable;
        
        [ShowIf("dashable"),LabelText("闪烁数据")]
        public SDashData dashData;
        
        [ShowIf("moveable"),LabelText("是否能够进行跳跃")]
        public bool jumpable;
        
        [ShowIf("jumpable"),LabelText("跳跃数据")]
        public SJumpData jumpData;
        
        [ShowIf("moveable"),LabelText("是否能够进行蹲伏")]
        public bool crouchable;
        
        [ShowIf("crouchable"),LabelText("蹲伏数据")]
        public SCrouchData crouchData;
        
    }
    
    /// <summary>
    /// 移动数据配置
    /// </summary>
    [Serializable]
    public struct SMoveData
    {
        [LabelText("行走速度")]
        public float walkSpeed;
        
        [LabelText("奔跑速度")]
        public float runSpeed;

        [LabelText("最大上下转向角")]
        public Vector2 maxPitchAngle;
    }
    
    /// <summary>
    /// 冲刺数据配置
    /// </summary>
    [Serializable]
    public struct SDashData
    {
        [LabelText("冲刺速度")]
        public float dashSpeed;

        [LabelText("冲刺CD")]
        public float dashCD;
        
    }
    
    /// <summary>
    /// 跳跃数据配置
    /// </summary>
    [Serializable]
    public struct SJumpData
    {
        [LabelText("跳跃高度")]
        public float jumpHeight;
        
        [LabelText("空中移动速度")]
        public float inAirMoveSpeed;
        
        [LabelText("能否二段跳")]
        public bool canDoubleJump;

        [ShowIf("canDoubleJump"),LabelText("二段跳间隔时间")]
        public float doubleJumpDeepTime;

        [ShowIf("canDoubleJump"),LabelText("二段跳高度")]
        public float doubleJumpHeight;
    }

    /// <summary>
    /// 蹲伏数据配置
    /// </summary>
    [Serializable]
    public struct SCrouchData
    {
        [LabelText("蹲伏速度")]
        public float crouchSpeed;
        
        [LabelText("蹲伏高度变化率")]
        public float crouchReduceRatio;
    }
}

