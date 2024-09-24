using System;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace GameFrame.Config
{
    /// <summary>
    /// 技能所属节点树类别
    /// </summary>
    [LabelText("技能所属节点树类别")]
    public enum ESkillNodeTreeType
    {
        [LabelText("生存类")]
        Live,
        [LabelText("战斗类别")]
        Combat,
        [LabelText("特殊类别")]
        Special
    }

    /// <summary>
    /// 技能类型
    /// </summary>
    [LabelText("技能类型")]
    public enum ESkillSpellType
    {
        /// <summary>
        /// 主动触发
        /// </summary>
        [LabelText("主动技能")]
        Active,
        /// <summary>
        /// 被动触发
        /// </summary>
        [LabelText("被动技能")]
        Passive
    }
    
    /// <summary>
    /// 作用目标对象类型
    /// </summary>
    [LabelText("作用目标对象类型")]
    public enum ESkillEffectTargetType
    {
        /// <summary>
        /// 技能目标
        /// </summary>        
        [LabelText("技能目标")]
        Target,
        /// <summary>
        /// 自身
        /// </summary>        
        [LabelText("自身")]
        Self,
        /// <summary>
        /// 附身对象
        /// </summary>        
        [LabelText("附身对象")]
        PossessionTarget,
        /// <summary>
        /// 其他
        /// </summary>        
        [LabelText("其他")]
        Other
    }
    
    /// <summary>
    /// 技能目标阵营
    /// </summary>
    [LabelText("技能目标阵营")]
    public enum ESkillAffectTargetCampsType
    {
        /// <summary>
        /// 自身
        /// </summary>
        [LabelText("自身")]
        Self = 0,
        /// <summary>
        /// 己方
        /// </summary>
        [LabelText("己方")]
        SelfTeam = 1,
        /// <summary>
        /// 敌方
        /// </summary>
        [LabelText("敌方")]
        EnemyTeam = 2,
    }


    
    /// <summary>
    /// 目标类型
    /// </summary>
    [LabelText("目标类型")]
    public enum ESkillTargetType
    {
        [LabelText("单体检测")]
        Single = 0,
        [LabelText("多人检测")]
        Multiple = 1,
    }
    
    /// <summary>
    /// 区域场类型
    /// </summary>
    [LabelText("区域场类型")]
    public enum ESkillAffectAreaType
    {
        /// <summary>
        /// 圆形
        /// </summary>
        [LabelText("圆形")]
        Circle = 0,
        /// <summary>
        /// 矩形
        /// </summary>
        [LabelText("矩形")]
        Rect = 1,
        /// <summary>
        /// 三角形
        /// </summary>
        [LabelText("三角形")]
        Triangle = 2,
        /// <summary>
        /// 组合
        /// </summary>
        [LabelText("组合")]
        Compose = 3
    }
    
    /// <summary>
    /// 伤害类型
    /// </summary>
    [LabelText("伤害类型")]
    public enum EDamageType
    {
        /// <summary>
        /// 物理伤害
        /// </summary>
        [LabelText("物理伤害")]
        Physic = 0,
        /// <summary>
        /// 魔法伤害
        /// </summary>
        [LabelText("魔法伤害")]
        Magic = 1,
        /// <summary>
        /// 真实伤害
        /// </summary>
        [LabelText("真实伤害")]
        Real = 2,
    }
    
    /// <summary>
    /// 效果类型
    /// </summary>
    [LabelText("效果类型")]
    public enum ESkillTypeOfEffect
    {
        /// <summary>
        /// 无
        /// </summary>
        [LabelText("(添加效果)")]
        None = 0,
        /// <summary>
        /// 造成伤害
        /// </summary>
        [LabelText("造成伤害")]
        CauseDamage = 1,
        /// <summary>
        /// 治疗英雄
        /// </summary>
        [LabelText("治疗英雄")]
        CureHero = 2,
        /// <summary>
        /// 施加状态
        /// </summary>
        [LabelText("施加状态")]
        AddStatus = 3,
        /// <summary>
        /// 移除状态
        /// </summary>
        [LabelText("移除状态")]
        RemoveStatus = 4,
        /// <summary>
        /// 增减数值
        /// </summary>
        [LabelText("增减数值")]
        NumericModify = 6,
        /// <summary>
        /// 中毒
        /// </summary>
        [LabelText("中毒")]
        Poison = 7,
        /// <summary>
        /// 灼烧
        /// </summary>
        [LabelText("灼烧")]
        Burn = 8,
    }
    
    /// <summary>
    /// 行为禁制
    /// </summary>
    [Flags]
    [LabelText("行为禁制")]
    public enum EProhibitionOfConductType
    {
        /// <summary>
        /// 无
        /// </summary>
        [LabelText("（空）")]
        None = 0,
        /// <summary>
        /// 禁止移动
        /// </summary>
        [LabelText("禁止移动")]
        MoveForbid = 1 << 1,
        /// <summary>
        /// 禁止施法
        /// </summary>
        [LabelText("禁止施法")]
        SkillForbid = 1 << 2,
        /// <summary>
        /// 禁止攻击
        /// </summary>
        [LabelText("禁止攻击")]
        AttackForbid = 1 << 3,
    }
    
    /// <summary>
    /// 附加数值类型
    /// </summary>
    [LabelText("附加数值类型")]
    public enum EModifyType
    {
        /// <summary>
        /// 固定增加数值
        /// </summary>
        [LabelText("固定增加数值")]
        Add = 0,
        /// <summary>
        /// 百分比增加数值
        /// </summary>
        [LabelText("百分比增加数值")]
        PercentAdd = 1,
        /// <summary>
        /// 基础值
        /// </summary>
        [LabelText("基础值")]
        BaseValue = 2,
    }
    
    /// <summary>
    /// 触发类型
    /// </summary>
    [LabelText("触发类型")]
    public enum ETriggerType
    {
        /// <summary>
        /// 主动触发
        /// </summary>
        [LabelText("主动触发")]
        ExecuteTrigger = 1,
        /// <summary>
        /// 被动触发
        /// </summary>
        [LabelText("被动触发")]
        AutoTrigger = 2
    }
    
    /// <summary>
    /// 技能触发执行类型
    /// </summary>
    [LabelText("技能触发执行类型")]
    public enum ESkillTriggersTheExecutionType
    {
        /// <summary>
        /// 触发立即开始执行
        /// </summary>
        [LabelText("使用就立即执行")]
        BeginTrigger = 0,
        /// <summary>
        /// 碰撞到目标就执行
        /// </summary>
        [LabelText("碰撞到目标就执行")]
        CollisionTrigger = 1,
        /// <summary>
        /// 计时状态执行
        /// </summary>
        [LabelText("计时状态执行")]
        TimeStateTrigger = 2
    }
    
    /// <summary>
    /// 技能选取目标方式类型
    /// </summary>
    [LabelText("技能选取目标方式类型")]
    public enum ESkillSelectionTargetWayType
    {
        /// <summary>
        /// 手动指定
        /// </summary>
        [LabelText("手动指定")]
        PlayerSelect,
        /// <summary>
        /// 碰撞检测
        /// </summary>
        [LabelText("碰撞检测")]
        CollisionSelect,
        /// <summary>
        /// 固定区域场检测
        /// </summary>
        [LabelText("固定区域场检测")]
        AreaSelect,
        /// <summary>
        /// 条件指定
        /// </summary>
        [LabelText("条件指定")]
        ConditionSelect,
        /// <summary>
        /// 自定义
        /// </summary>
        [LabelText("自定义")]
        Custom,
    }
}

