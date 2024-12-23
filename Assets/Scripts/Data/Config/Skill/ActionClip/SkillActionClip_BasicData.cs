﻿using System;
using GameFrame.World;
using Sirenix.OdinInspector;

namespace GameFrame.Config
{
    [Serializable]
    public abstract class SkillActionClip_BasicData : SerializedScriptableObject,IExecuteLogic,ITriggerLogic_NoTarget,ITriggerLogic_HasTarget
    {
        public WorldObj ownerObj { get; set; }

        public abstract void InitExecution(WorldObj owner);

        public abstract void StartExecute();

        public abstract void EndExecute();

        public abstract void ResetExecute();
        
        public abstract void OnTriggerStart();

        public abstract void OnTriggerEnd();

        public abstract void OnTriggerStart(WorldObj suffer);

        public abstract void OnTriggerEnd(WorldObj suffer);
    }
}