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



    public class SActionClipData_Temporality : TemporalityData_Pool
    {
        public WorldObj owner;

        public WorldObj[] triggerTargets;
        
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
        [LabelText("行为延时的时间"),SerializeField]
        private float TimeDelayTime;

        protected SActionClipData_Temporality clipDataTemporality;
        
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
                StartExecute();
            }
        }

        IEnumerator TimeDelay()
        {
            yield return new WaitForSeconds(TimeDelayTime);
            EndTimeDelay();
        }

        /// <summary>
        /// 结束延时
        /// </summary>
        public virtual void EndTimeDelay()
        {
            
        }
        
        /// <summary>
        /// 开始执行
        /// </summary>
        /// <param name="thisObj"></param>
        public virtual void StartExecute()
        {
           
        }
        

        /// <summary>
        /// 结束执行
        /// </summary>
        public virtual void EndExecute()
        {
            ResetExecute();
        }

        /// <summary>
        /// 重设变量
        /// </summary>
        public virtual void ResetExecute()
        {
            clipDataTemporality.Recycle2Cache();
        }

        /// <summary>
        ///  触发(可以多次触发,直到生命周期结束)--所有者发出,但是无目标自触发(例如每几秒在飞行路径上生成一个毒坑)
        /// </summary>
        /// <param name="curTriggerTarget"></param>
        public virtual void Trigger(WorldObj owner)
        {
            
        }

        /// <summary>
        /// 明确目标和所有者
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="curTriggerTarget"></param>
        public virtual void Trigger(WorldObj owner, WorldObj curTriggerTarget)
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

        [LabelText("是否是全局播放")]
        public bool IsGobal;
        
        [LabelText("音量")]
        public float Volume;
        
        [LabelText("播放次数(如果为-1,就代表循环播放)")]
        public int PlayCount = 1;
    }
}
