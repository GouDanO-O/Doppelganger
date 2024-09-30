using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    //首先,每个技能是由基础的行为Action组成,每个行为都会是独立的,且有各自的逻辑行为
    //例如释放一个植物大战僵尸里面僵王博士的(火球)就可以拆解为以下行为:
    //首先,开始释放一个球,且附加上火属性,这个球是以当前玩家正前方进行释放,且释放过程中玩家无法进行移动和攻击,会有n秒的释放前摇
    //然后,球释放完毕,开始进行滚动,当碰撞到其他目标时,会对其造成伤害
    //最后,经过n秒或碰撞到n个目标又或者撞到墙壁又或者碰到其他属性相克的物体时,就会被摧毁
    //摧毁时会进行爆炸,对aXb范围内的敌人造成伤害和属性伤害
    //这个技能只是一个例子,在这个技能中,可以在行为中进行穿插或叠加,例如修改下附加的属性,就是另一个技能
    //再或者不会滚动,而是以玩家选取的范围为目标点,进行抛物线飞射
    //从而不同的行为,可以构成许多不同的技能
    
    
    [CreateAssetMenu(fileName = "BasicSkill",menuName = "配置/技能/基础技能配置")]
    public class SkillNodeDataConfig : SerializedScriptableObject
    {
        [LabelText("技能ID")]
        public int SkillID;
        
        [LabelText("技能名称")]
        public string SkillName;
        
        [LabelText("技能描述")]
        public string SkillDescription;
        
        [LabelText("技能所属节点类型")]
        public ESkill_NodeTreeType SkillNodeType;
        
        [LabelText("技能触发类型\n主动--会添加到主动技能列表中(当玩家互动且满足使用条件时会释放)\n被动--当满足使用条件时会自动释放",true)]
        public ETriggerType SkillSpellType;
        
        [LabelText("主动技能释放方式"),ShowIf("SkillSpellType",ETriggerType.ExecuteTrigger)]
        public ESkill_TriggerConditionType SkillTriggerConditionType;
        
        [LabelText("技能触发条件")]
        public string SkillTriggerConditionFormula;
        
        [LabelText("所需等级(-1表示无等级限制)")]
        public int RequiredLevel;
        
        [LabelText("所需技能点数")]
        public int SkillPointsCost;
        
        [LabelText("前置技能列表")]
        public List<SkillNodeDataConfig> PrerequisiteSkills;
        
        [LabelText("技能行为列表")]
        public List<SkillAction_Config> SkillActions;
        
        [LabelText("CD")]
        public float SkillCooldown;

        [LabelText("最大可升级等级")]
        public int MaxLevel;

        [LabelText("每个升级的等级配置"),ShowIf("@this.MaxLevel>0")]
        public SkillNodeDataConfig[] LevelUpSkills;
    }
}

