﻿using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame
{
    /// <summary>
    /// 元素伤害配置--归属玩家的不可成长属性--是对其他物体或玩家造成的伤害
    /// </summary>
    [Serializable,LabelText("元素伤害配置--归属玩家的不可成长属性--是对其他物体或玩家造成的伤害", SdfIconType.Box)]
    public class ElementDamageData_Persistent : PersistentData
    {
        [LabelText("最大元素可积累等级"),MinValue(1)]
        public int MaxElementAccLevel = 1;
        
        [LabelText("基础元素伤害")]
        public float BasicElementDamage;

        [LabelText("每次升级增加的伤害比例(不包括第一级)")]
        public List<float> ElementLevelUpAddedDamage=new List<float>();

        [LabelText("元素总持续时长")]
        public int MaxElementDuration;

        [LabelText("每次升级增加的总持续时长(不包括第一级)")]
        public List<float> ElementLevelUpAddDurtaion;
        
        [LabelText("元素造成伤害的间隔时间")]
        public float BasicElementTriggerInterval;

        [LabelText("每次升级减少造成伤害的间隔时间比例(不包括第一级)")]
        public List<float> ElementLevelUpDesriggerInterval;

        public float GetElementDamage(int level)
        {
            float finalDamage = BasicElementDamage;
            if (level >= MaxElementAccLevel)
            {
                level = MaxElementAccLevel - 1;
            }
            
            finalDamage *= ElementLevelUpAddedDamage[level];
            return finalDamage;
        }

        public float GetElementDuration(int level)
        {
            float finalDurtaion = MaxElementDuration;
            if (level >= MaxElementAccLevel)
            {
                level = MaxElementAccLevel - 1;
            }
            
            finalDurtaion *= ElementLevelUpAddDurtaion[level];
            return finalDurtaion;
        }

        public float GetElementTriggerInterval(int level)
        {
            float finalTriggerInterval = BasicElementTriggerInterval;
            if (level >= MaxElementAccLevel)
            {
                level = MaxElementAccLevel - 1;
            }
            
            finalTriggerInterval *= ElementLevelUpDesriggerInterval[level];
            return finalTriggerInterval;
        }
    }
}