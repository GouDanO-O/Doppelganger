using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame
{
    /// <summary>
    /// 蹲伏数据配置
    /// </summary>
    [Serializable,LabelText("蹲伏数据", SdfIconType.Box)]
    public struct SCrouchData
    {
        [LabelText("蹲伏速度")]
        public float crouchSpeed;
        
        [LabelText("蹲伏高度变化率"),Range(0,1)]
        public float crouchReduceRatio;

        [LabelText("蹲伏检测层级")]
        public LayerMask crouchCheckLayerMask;
    }
}