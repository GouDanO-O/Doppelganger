using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    [CreateAssetMenu(fileName = "WorldObj",menuName = "配置/世界/基础物体配置")]
    public class WorldObjDataConfig : SerializedScriptableObject
    {
        [Required(InfoMessageType.Error),LabelText("预制体")]
        public GameObject thisPrefab;
        
        [LabelText("碰撞等级")]
        public EWorldObjCollisionType CollisionType;
        
        [Space(3),LabelText("是否有技能")]
        public bool hasSkill;
        
        [ShowIf("hasSkill"),LabelText("技能树")]
        public SkillTreeConfig skillTree;
        
        [LabelText("重力"),Range(-20,0)]
        public float gravity;
        
        [Space(3),LabelText("是否具有生命")]
        public bool healthyable;
        
        [ShowIf("healthyable"),LabelText("生命数据",SdfIconType.Box)]
        public SHealthyData healthyData;

        [Header("变身所需要的能量,x=>仅外貌, y=>完全变身")]
        [LabelText("")]
        public Vector2 costDeformationEnergy;
        
        [LabelText("是否能够进行移动(不会接受输入数据,由物体本身控制移动)")]
        public bool moveable;

        [ShowIf("moveable"), LabelText("移动数据", SdfIconType.Box)]
        public SMoveData moveData;

        [Space(3)]
        [ShowIf("moveable"), LabelText("是否能够进行闪烁")]
        public bool dashable;

        [ShowIf("@moveable && dashable"), LabelText("闪烁数据", SdfIconType.Box)]
        public SDashData dashData;

        [Space(3)]
        [ShowIf("moveable"), LabelText("是否能够进行跳跃")]
        public bool jumpable;

        [ShowIf("@moveable && jumpable"), LabelText("跳跃数据", SdfIconType.Box)]
        public SJumpData jumpData;

        [Space(3)]
        [ShowIf("moveable"), LabelText("是否能够进行蹲伏")]
        public bool crouchable;

        [ShowIf("@moveable && crouchable"), LabelText("蹲伏数据", SdfIconType.Box)]
        public SCrouchData crouchData;
        
        [Space(3),LabelText("能否进行攻击")]
        public bool attackable;
        
        [ShowIf("attackable"),LabelText("攻击数据",SdfIconType.Box)]
        public SAttackData attackData;
    }
    
    /// <summary>
    /// 生命数据配置
    /// </summary>
    [Serializable]
    public struct SHealthyData
    {
        [LabelText("最大生命值")]
        public float maxHealth;
        
        [LabelText("最大护甲值")]
        public float maxArmor;
        
        [LabelText("伤害减免率"),Range(0,1)]
        public float damageReductionRatio;
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
        
        [LabelText("空中移动速度")]
        public float inAirMoveSpeed;

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
        
        [LabelText("蹲伏高度变化率"),Range(0,1)]
        public float crouchReduceRatio;
    }
    
    /// <summary>
    /// 攻击数据配置
    /// </summary>
    [Serializable]
    public struct SAttackData
    {
        [LabelText("基础伤害")]
        public float basicDamage;
        
        [LabelText("暴击率"),Range(0,100)]
        public float criticalRate;
        
        [LabelText("暴击伤害")]
        public float criticalDamage;
        
        [LabelText("攻击距离")]
        public float attackDistance;
    }
    
    /// <summary>
    /// 物体的碰撞等级
    /// 
    /// </summary>
    public enum EWorldObjCollisionType
    {
        [LabelText("正常等级--通常场景中的静态物体")]
        NormalLevel,
        [LabelText("中等等级--")]
        MidLevel,
        [LabelText("高等等级--")]
        HighLevel,
        [LabelText("特殊等级--")]
        SpecialLevel,
    }
}

