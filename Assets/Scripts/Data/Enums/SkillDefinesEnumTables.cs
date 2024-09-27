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
    public enum ESkill_NodeTreeType
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
    public enum ESkill_SpellType
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
    public enum ESkill_EffectTargetType
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
    public enum ESkill_AffectTargetCampsType
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
    public enum ESkill_TargetType
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
    public enum ESkill_AffectAreaType
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
    /// 效果类型
    /// </summary>
    [LabelText("效果类型")]
    public enum ESkill_TypeOfEffect
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
    /// 技能触发执行类型
    /// </summary>
    [LabelText("技能触发执行类型")]
    public enum ESkill_TriggersTheExecutionType
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
    public enum ESkill_SelectionTargetWayType
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

