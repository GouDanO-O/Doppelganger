using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame
{
    [LabelText("目标传入类型")]
    public enum EAction_TargetInputType
    {
        [LabelText("None")]
        None = 0,
        [LabelText("传入目标实体")]
        Target = 1,
        [LabelText("传入目标点")]
        Point = 2,
    }
    
    [LabelText("触发类型"), Flags]
    public enum EAction_TriggerType
    {
        [LabelText("无")]
        None = 0,
        [LabelText("初始触发")]
        StartTrigger = 1 << 1,
        [LabelText("碰撞触发单次")]
        CollisionTrigger = 1 << 2,
        [LabelText("结束触发")]
        EndTrigger = 1 << 3,
        [LabelText("碰撞触发多次")]
        CollisionTriggerMultiple = 1 << 4,
    }

    [LabelText("发射弹体形式")]
    public enum EAction_ShootProjectileType
    {
        [LabelText("直线")]
        Line,
        [LabelText("抛物线")]
        Parabola,
    }
}

