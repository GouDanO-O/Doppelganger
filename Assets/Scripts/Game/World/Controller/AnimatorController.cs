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
    
    public delegate void PlayAnimationEvent(SAnimatorEvent data);
    
    public class AnimatorController: AbstractController
    {
        private EAnimationType currentAnimationType = EAnimationType.Idle;

        public PlayAnimationEvent onPlayAnimationEvent;
        
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
            onPlayAnimationEvent += DisposeAnimations;
        }

        public void UnRegisterEvent()
        {
            onPlayAnimationEvent-= DisposeAnimations;
        }

        public void DisposeAnimations(SAnimatorEvent eventData)
        {
            this.currentAnimationType = eventData.animationType;
        }
    }
}