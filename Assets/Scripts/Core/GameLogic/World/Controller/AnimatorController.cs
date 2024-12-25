using System.Collections.Generic;
using QFramework;
using UnityEngine.Events;

namespace GameFrame.World
{
    /// <summary>
    /// 动画控制器
    /// </summary>
    public class AnimatorController: AbstractController
    {
        private EAnimationType currentAnimationType = EAnimationType.Idle;
        
        public override void InitData(WorldObj owner)
        {
            base.InitData(owner);
            RegisterEvent();
        }
        
        public override void DeInitData()
        {
            UnRegisterEvent();
        }

        public void RegisterEvent()
        {
            owner.onPlayAnimationEvent += DisposeAnimations;
        }

        public void UnRegisterEvent()
        {
            owner.onPlayAnimationEvent-= DisposeAnimations;
        }

        public void DisposeAnimations(SPlayAnimationEvent eventData)
        {
            this.currentAnimationType = eventData.animationType;
        }
    }
}