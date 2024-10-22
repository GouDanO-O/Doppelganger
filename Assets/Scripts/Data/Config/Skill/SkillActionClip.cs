using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameFrame.World;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    [Serializable]
    public class SkillActionClip
    {
        [LabelText("行为类型")]
        public EActionType ActionType;

        [ShowIf("@ActionType ==EActionType.DetailAction"), LabelText("具体行为")]
        public SActionClip_DetailAction_Basic DetailAction;
        
        [ShowIf("@ActionType ==EActionType.Animation"), LabelText("动画")]
        public SActionClip_AnimationData AnimationData;
        
        [ShowIf("@ActionType ==EActionType.Audio"), LabelText("音效")]
        public SActionClip_AudioData AudioData;
        
        [ShowIf("@ActionType ==EActionType.ParticleSystem"), LabelText("粒子特效")]
        public SActionClip_ParticleEffectData ParticleEffectData;

        public void TriggerSkillAction(WorldObj owner =null,WorldObj target=null)
        {
            switch (ActionType)
            {
                case EActionType.DetailAction:
                    DetailAction?.ExecuteCheck(owner); // 检查并执行具体行为
                    break;
                case EActionType.Animation:

                    break;
                case EActionType.Audio:

                    break;
                case EActionType.ParticleSystem:

                    break;
            }
        }
    }

    [Serializable]
    public class SActionClip_BasicData
    {
        [HorizontalGroup("Timing")]
        [LabelText("开始时间"), LabelWidth(60), MinValue(0)]
        public float StartTime;

        [HorizontalGroup("Timing")]
        [LabelText("持续时间"), LabelWidth(60)]
        public float Duration = 1f;

        [HorizontalGroup("Timing")]
        [LabelText("结束时间"), LabelWidth(60), ReadOnly]
        public float EndTime => StartTime + Duration;
    }

    [LabelText("对象池所属类型")]
    public enum EObjectPoolType
    {
                
    }
    
    /// <summary>
    /// 基础行为--(具体行为方式在行为中定义)
    /// </summary>
    [Serializable]
    public class SActionClip_DetailAction_Basic : SerializedScriptableObject
    {
        [LabelText("是否从对象池中进行加载")]
        public bool isLoadFromPool;
        
        [ShowIf("isLoadFromPool")]
        public EObjectPoolType ObjectPoolType;
        
        [ShowIf("@isLoadFromPool ==false"),LabelText("物体")]
        public GameObject ObjectPrefab;
        
        [HorizontalGroup("Timing")]
        [LabelText("开始时间"), LabelWidth(60), MinValue(0)]
        public float StartTime;

        [HorizontalGroup("Timing")]
        [LabelText("持续时间"), LabelWidth(60)]
        public float Duration = 1f;

        [HorizontalGroup("Timing")]
        [LabelText("结束时间"), LabelWidth(60), ReadOnly]
        public float EndTime => StartTime + Duration;

        [LabelText("行为延时的时间"),SerializeField]
        private float TimeDelayTime;
        
        public EAction_TriggerType ActionTriggerType;

        [ShowIf("@ActionTriggerType==EAction_TriggerType.LifeTimeEndTrigger"),LabelText("生命周期时间"),SerializeField]
        private float LifeTime;
        
        protected WorldObj owner;
        
        protected Transform transform;

        private bool timeDelayEnd;
        
        private float lastLifeTime;
        
        private bool isLifeTimeCheck;

        /// <summary>
        /// 用来标记是否能够进行生命周期内的行为循环
        /// </summary>
        private bool canUpdateExecute;

        /// <summary>
        /// 开始行为前,进行前置检测
        /// </summary>
        public virtual void ExecuteCheck(WorldObj owner)
        {
            this.owner = owner;
            this.transform = owner.transform;
            CheckDelayTime();
        }

        /// <summary>
        /// 计算延时
        /// </summary>
        public virtual void CheckDelayTime()
        {
            if (TimeDelayTime > 0)
            {
                Main.Interface.GetUtility<CoroutineUtility>().StartRoutine(TimeDelay());
            }
            else
            {
                timeDelayEnd = true;
                StartExecute();
            }
        }

        IEnumerator TimeDelay()
        {
            timeDelayEnd = false;
            yield return new WaitForSeconds(TimeDelayTime);
            EndTimeDelay();
        }

        /// <summary>
        /// 结束延时
        /// </summary>
        public virtual void EndTimeDelay()
        {
            timeDelayEnd = true;
            StartExecute();
        }

        /// <summary>
        /// 重设行为
        /// </summary>
        public virtual void ResetExecution()
        {
            
        }
        
        /// <summary>
        /// 开始生命周期计算
        /// </summary>
        /// <param name="thisObj"></param>
        public virtual void StartExecute()
        {
            TriggerTypeCheck();
            canUpdateExecute = true;
        }

        /// <summary>
        /// 生命周期更新
        /// </summary>
        public virtual void UpdateExecute()
        {
            if(!timeDelayEnd && !canUpdateExecute)
                return;

            if (isLifeTimeCheck)
            {
                lastLifeTime -= Time.deltaTime;
                if (lastLifeTime <= 0)
                {
                    EndExecute();
                    return;
                }
            }
        }

        /// <summary>
        /// 结束生命周期--不能本帧就销毁(要么延时,要么下一帧,以确保本帧逻辑执行完毕)
        /// </summary>
        public virtual void EndExecute()
        {
            canUpdateExecute = false;
        }

        /// <summary>
        /// 触发(可以多次触发,直到生命周期结束)--一般作用于无目标自触发类型(例如每几秒在飞行路径上生成一个毒坑)
        /// </summary>
        public virtual void Trigger()
        {
            
        }

        /// <summary>
        /// 对世界中的物体触发
        /// </summary>
        /// <param name="curTriggerTarget"></param>
        public virtual void Trigger(WorldObj curTriggerTarget)
        {
            
        }
        
        /// <summary>
        /// 触发类型检测
        /// </summary>
        public virtual void TriggerTypeCheck()
        {
            isLifeTimeCheck = false;
            if (ActionTriggerType == EAction_TriggerType.StartTrigger)
            {
                Trigger();
            }
            else if (ActionTriggerType == EAction_TriggerType.LifeTimeEndTrigger)
            {
                lastLifeTime = LifeTime;
                isLifeTimeCheck = true;
            }
        }
    }
    
    
    [Serializable]
    public class SActionClip_ParticleEffectData : SActionClip_BasicData
    {
        [LabelText("特效预制体")]
        public GameObject ParticleEffectPrefab;
    }

    [Serializable]
    public class SActionClip_AnimationData : SActionClip_BasicData
    {
        [LabelText("动画Clip")]
        public AnimationClip AnimationClip;
    }

    [Serializable]
    public class SActionClip_AudioData : SActionClip_BasicData
    {
        [LabelText("音频Clip")]
        public AudioClip AudioClip;
    }
}
