using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame
{
    /// <summary>
    /// 移动数据配置
    /// </summary>
    [Serializable,LabelText("移动数据", SdfIconType.Box)]
    public struct SMoveData
    {
        [LabelText("行走速度")]
        public float walkSpeed;
        
        [LabelText("奔跑速度")]
        public float runSpeed;
        
        [LabelText("空中移动速度")]
        public float inAirMoveSpeed;

        [LabelText("最大上下转向角")]
        public Vector2 maxPitchAngle;
        
        [LabelText("检测层级")]
        public LayerMask groundLayerMask;
    }
}