using System.Collections.Generic;
using QFramework;
using UnityEngine.Events;

namespace GameFrame.World
{
    public enum EAnimationType
    {
        Idle,
        StartWalking,
        Walking,
        EndWalking,
        StartRunning,
        Running,
        EndRunning,
        Crouching,
        StandUp,
        StartJumping,
        DoubleJumping,
        Falling,
        Landing,
        Hitted,
        Death,
        Dashing
    }
    
    public struct SAnimatorEvent
    {
        public EAnimationType animationType;

        public float blendValue;
    }
    
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
            controller.onPlayAnimationEvent += DisposeAnimations;
        }

        public void UnRegisterEvent()
        {
            controller.onPlayAnimationEvent-= DisposeAnimations;
        }

        public void DisposeAnimations(SAnimatorEvent eventData)
        {
            this.currentAnimationType = eventData.animationType;
        }
    }
}