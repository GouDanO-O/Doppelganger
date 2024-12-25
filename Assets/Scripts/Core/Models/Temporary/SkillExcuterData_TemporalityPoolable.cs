using GameFrame.Config;
using QFramework;

namespace GameFrame
{
public class SkillExcuterData_TemporalityPoolable : TemporalityData_Pool
    {
        public SkillActionClip SkillActionClip;
        
        public float willExecuteTime;
        
        public float lifeDuration;

        public float endTime;
        
        private bool hasExecuted = false;

        public static SkillExcuterData_TemporalityPoolable Allocate()
        {
            return SafeObjectPool<SkillExcuterData_TemporalityPoolable>.Instance.Allocate();
        }
        
        public void InitData(SkillActionClip clipConfig)
        {
            this.SkillActionClip=clipConfig;
            this.willExecuteTime = clipConfig.StartTime;
            this.lifeDuration = clipConfig.Duration;
            this.endTime = clipConfig.EndTime;
        }

        /// <summary>
        /// 检查执行时机
        /// </summary>
        /// <param name="curTime"></param>
        /// <returns></returns>
        public bool CheckTime(float curTime)
        {
            return curTime >= willExecuteTime && !hasExecuted;
        }

        /// <summary>
        /// 检查是否执行
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
            hasExecuted = true;
            ActionTypeCheck();
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
                        SkillActionClip.Parameters.InitExecution();
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