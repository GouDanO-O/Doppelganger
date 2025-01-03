using GameFrame.Config;
using GameFrame.World;
using QFramework;

namespace GameFrame
{
    /// <summary>
    /// 技能执行器再进行细分
    /// 细分为每个行为的执行器
    /// </summary>
    public class SkillExcuterData_TemporalityPoolable : TemporalityData_Pool
    {
        public SkillActionClip SkillActionClip;
        
        public float willExecuteTime;
        
        public float lifeDuration;

        public float endTime;

        private WorldObj owner;
        
        private bool hasStartExecuted = false;
        
        private bool hasEndExecuted = false;

        public static SkillExcuterData_TemporalityPoolable Allocate()
        {
            return SafeObjectPool<SkillExcuterData_TemporalityPoolable>.Instance.Allocate();
        }
        
        public void InitData(SkillActionClip clipConfig,WorldObj owner)
        {
            this.SkillActionClip = clipConfig;
            this.willExecuteTime = clipConfig.StartTime;
            this.lifeDuration = clipConfig.Duration;
            this.endTime = clipConfig.EndTime;
            this.owner = owner;
        }

        /// <summary>
        /// 检查执行时机
        /// </summary>
        /// <param name="curTime"></param>
        /// <returns></returns>
        public bool CheckTime(float curTime)
        {
            return curTime >= willExecuteTime && !hasStartExecuted;
        }

        /// <summary>
        /// 检查是否执行完毕
        /// </summary>
        /// <param name="curTime"></param>
        /// <returns></returns>
        public bool CheckEndTime(float curTime)
        {
            return curTime >= endTime;
        }

        /// <summary>
        /// 开始执行
        /// </summary>
        public void StartExecute()
        {
            hasStartExecuted = true;
            ActionTypeCheck();
        }

        /// <summary>
        /// 结束执行
        /// </summary>
        public void EndExecute()
        {
            hasEndExecuted = true;
        }
        
        /// <summary>
        /// 是否结束执行
        /// </summary>
        /// <returns></returns>
        public bool HasExecute()
        {
            return hasEndExecuted;
        }
        
        /// <summary>
        /// 技能行为检测
        /// </summary>
        /// <param name="clipData"></param>
        private void ActionTypeCheck()
        {
            switch (SkillActionClip.ActionType)
            {
                case EActionType.DetailAction:
                    if (SkillActionClip.Parameters is SkillActionClip_DetailAction_Basic detailAction)
                    {
                        SkillActionClip.Parameters.InitExecution(owner);
                        EndExecute();
                    }
                    break;
                case EActionType.Animation:
                    // 实现动画触发逻辑
                    
                    break;
                case EActionType.Audio:
                    // 实现音效触发逻辑
                    break;
                case EActionType.ParticleSystem:
                    // 实现粒子特效触发逻辑
                    break;
            }
            
        }

        public override void DeInitData()
        {
            hasStartExecuted = false;
            hasEndExecuted = false;
        }

        public override void OnRecycled()
        {
            DeInitData();
        }

        public override void Recycle2Cache()
        {
            SafeObjectPool<SkillExcuterData_TemporalityPoolable>.Instance.Recycle(this);
        }
    }
}