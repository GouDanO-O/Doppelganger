using System;
using GameFrame.World;
using Sirenix.OdinInspector;

namespace GameFrame.Config
{
    [Serializable]
    public abstract class SkillActionClip_BasicData : SerializedScriptableObject,ITriggerLogic_NoTarget,ITriggerLogic_HasTarget
    {
        protected WorldObj owner;

        public virtual void InitExecution(WorldObj owner)
        {
            this.owner = owner;
        }

        protected abstract void StartExecute();

        public abstract void EndExecute();

        public virtual void ResetExecute()
        {
            this.owner = null;
        }
        
        public abstract void OnTriggerStart();

        public abstract void OnTriggerEnd();

        public abstract void OnTriggerStart(WorldObj suffer);

        public abstract void OnTriggerEnd(WorldObj suffer);
    }
}