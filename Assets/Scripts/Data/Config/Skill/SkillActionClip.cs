using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameFrame.World;
using QFramework;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    [Serializable]
    public class SkillActionClip
    {
        [LabelText("行为类型")]
        public EActionType ActionType;

        [LabelText("行为描述")]
        public string ActionDes;

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

    public class SActionClipData_Temporality : TemporalityData_Pool
    {
        public WorldObj owner;
        
        public bool timeDelayEnd;
        
        public bool IsRecycled { get; set; }

        public static SActionClipData_Temporality Allocate()
        {
            return SafeObjectPool<SActionClipData_Temporality>.Instance.Allocate();
        }

        public void SetOwner(WorldObj owner)
        {
            this.owner = owner;
        }

        public override void OnRecycled()
        {
            owner = null;
            timeDelayEnd = false;
        }
        
        public override void Recycle2Cache()
        {
            SafeObjectPool<SActionClipData_Temporality>.Instance.Recycle(this);
        }
    }
    
    /// <summary>
    /// 基础行为--(具体行为方式在行为中定义)
    /// </summary>
    [Serializable]
    public class SActionClip_DetailAction_Basic : SerializedScriptableObject
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

        [LabelText("行为延时的时间(释放这个技能需要的前摇时长)"),SerializeField]
        private float TimeDelayTime;
        
        public EAction_TriggerType ActionTriggerType;

        [ShowIf("@ActionTriggerType==EAction_TriggerType.LifeTimeEndTrigger"),LabelText("生命周期时间")]
        public float LifeTime;

        private SActionClipData_Temporality clipDataTemporality;
        
        /// <summary>
        /// 开始行为前,进行前置检测(分配临时数据变量)
        /// </summary>
        public virtual void ExecuteCheck(WorldObj owner)
        {
            clipDataTemporality = SActionClipData_Temporality.Allocate();
            clipDataTemporality.SetOwner(owner);
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
                clipDataTemporality.timeDelayEnd = true;
                StartExecute();
            }
        }

        IEnumerator TimeDelay()
        {
            clipDataTemporality.timeDelayEnd = false;
            yield return new WaitForSeconds(TimeDelayTime);
            EndTimeDelay();
        }

        /// <summary>
        /// 结束延时
        /// </summary>
        public virtual void EndTimeDelay()
        {
            clipDataTemporality.timeDelayEnd = true;
            StartExecute();
        }
        
        /// <summary>
        /// 开始生命周期计算
        /// </summary>
        /// <param name="thisObj"></param>
        public virtual void StartExecute()
        {
            TriggerTypeCheck();
        }
        
        /// <summary>
        /// 触发类型检测
        /// </summary>
        public virtual void TriggerTypeCheck()
        {
            if (ActionTriggerType == EAction_TriggerType.StartTrigger)
            {
                Trigger();
            }
        }

        /// <summary>
        /// 结束生命周期--不能本帧就销毁(要么延时,要么下一帧,以确保本帧逻辑执行完毕)
        /// </summary>
        public virtual void EndExecute()
        {
            ResetExecute();
        }

        /// <summary>
        /// 结束生命周期时重设变量
        /// </summary>
        public virtual void ResetExecute()
        {
            clipDataTemporality.Recycle2Cache();
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
