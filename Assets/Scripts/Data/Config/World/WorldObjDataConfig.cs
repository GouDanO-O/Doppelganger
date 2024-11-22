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
        public GameObject ThisPrefab;
        
        [LabelText("碰撞等级")]
        public EWorldObjCollisionType CollisionType;
        
        [Space(3),LabelText("是否有技能")]
        public bool HasSkill;
        
        [ShowIf("HasSkill"),LabelText("技能树")]
        public SkillTreeConfig skillTree;
        
        [LabelText("重力"),Range(-20,0)]
        public float Gravity;
        
        [Space(3),LabelText("是否具有生命")]
        public bool Healthyable;
        
        [ShowIf("Healthyable")]
        public SHealthyData HealthyData;
        
        [ShowIf("Healthyable"),LabelText("元素属性配置--归属玩家的可成长数据", SdfIconType.Box),DictionaryDrawerSettings()]
        public Dictionary<EAction_Skill_ElementType,SElementPropertyData> ElementPropertyData= new Dictionary<EAction_Skill_ElementType,SElementPropertyData>();

        [Header("变身所需要的能量,x=>仅外貌, y=>完全变身")]
        [LabelText("")]
        public Vector2 CostDeformationEnergy;
        
        [LabelText("是否能够进行移动(不会接受输入数据,由物体本身控制移动)")]
        public bool Moveable;

        [ShowIf("Moveable")]
        public SMoveData MoveData;

        [Space(3)]
        [ShowIf("Moveable"), LabelText("是否能够进行闪烁")]
        public bool Dashable;

        [ShowIf("@Moveable && Dashable")]
        public SDashData DashData;

        [Space(3)]
        [ShowIf("Moveable"), LabelText("是否能够进行跳跃")]
        public bool Jumpable;

        [ShowIf("@Moveable && Jumpable")]
        public SJumpData JumpData;

        [Space(3)]
        [ShowIf("Moveable"), LabelText("是否能够进行蹲伏")]
        public bool Crouchable;

        [ShowIf("@Moveable && Crouchable")]
        public SCrouchData CrouchData;
        
        [Space(3),LabelText("能否使用武器攻击")]
        public bool WeaponAttackable;
        
        //[ShowIf("attackable"),LabelText("攻击数据",SdfIconType.Box)]
    }
    
    /// <summary>
    /// 生命数据配置
    /// </summary>
    [Serializable,LabelText("生命数据", SdfIconType.Box)]
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
    
    /// <summary>
    /// 冲刺数据配置
    /// </summary>
    [Serializable,LabelText("冲刺数据", SdfIconType.Box)]
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
    [Serializable,LabelText("跳跃数据", SdfIconType.Box)]
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

    /// <summary>
    /// 元素伤害配置--归属玩家的不可成长属性--是对其他物体或玩家造成的伤害
    /// </summary>
    [Serializable,LabelText("元素伤害配置--归属玩家的不可成长属性--是对其他物体或玩家造成的伤害", SdfIconType.Box)]
    public class ElementDamageData
    {
        [LabelText("基础元素伤害")]
        public float BasicElementDamage;
        
        [LabelText("最大元素可积累等级")]
        public int MaxElementAccLevel;

        [LabelText("每次升级增加的伤害比例(不包括第一级)")]
        public List<float> ElementLevelUpAddedDamage;

        [LabelText("元素造成伤害的间隔时间")]
        public float BasicElementTriggerInterval;

        [LabelText("每次升级减少造成伤害的间隔时间(不包括第一级)")]
        public List<float> ElementLevelUpDesriggerInterval;
        
        private int curElementLevel=-1;

        private float curElementTriggerDamage;
        
        private float curElementTriggerInterval;

        private float lastTriggerTime;
        
        public virtual void SetBasicDamage(float damage)
        {
            this.BasicElementDamage = damage;
            curElementTriggerDamage = BasicElementDamage;
            curElementTriggerInterval = BasicElementTriggerInterval;
        }

        public void AddElementLevel()
        {
            curElementLevel++;
            if (curElementLevel >= MaxElementAccLevel)
            {
                curElementLevel = MaxElementAccLevel;
            }
            
            curElementTriggerDamage = BasicElementDamage * ElementLevelUpAddedDamage[curElementLevel];
            curElementTriggerInterval = BasicElementTriggerInterval * ElementLevelUpDesriggerInterval[curElementLevel];
        }

        public float CaculateElementDamage()
        {
            return curElementTriggerDamage;
        }

        public bool CheckTriggerInterval()
        {
            if (Time.time - lastTriggerTime >= curElementTriggerInterval)
            {
                return true;
            }
            return false;
        }

        public void Reset()
        {
            
        }
    }

    /// <summary>
    /// 元素属性配置--归属玩家的可成长数据
    /// </summary>
    [Serializable]
    public struct SElementPropertyData
    {
        [LabelText("元素伤害倍率")]
        public float ElementBonusDamage;
        
        [LabelText("元素伤害抗性")]
        public float ElementResistance;
    }
    
    /// <summary>
    /// 物体的碰撞等级
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

