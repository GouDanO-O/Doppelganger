using System;
using System.Collections;
using GameFrame.World;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    /// <summary>
    /// 基础行为--(具体行为方式在行为中定义)
    /// </summary>
    [Serializable]
    public class SkillActionClip_DetailAction_Basic : SkillActionClip_BasicData
    {
        [LabelText("行为延时的时间"),SerializeField]
        private float TimeDelayTime;

        protected ActionClipData_TemporalityPoolable clipDataTemporality;
        
        /// <summary>
        /// 开始行为前,进行前置检测(分配临时数据变量)
        /// </summary>
        public virtual void ExecuteCheck(WorldObj owner)
        {
            clipDataTemporality = ActionClipData_TemporalityPoolable.Allocate();
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
        ///  触发(可以多次触发,直到生命周期结束)
        /// --所有者发出,但是无目标自触发(例如每几秒在飞行路径上生成一个毒坑)
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
}