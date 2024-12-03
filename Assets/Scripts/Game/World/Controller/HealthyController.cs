using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using GameFrame.World;
using QFramework;
using UnityEngine;
using GameFrame.UI;

namespace GameFrame.World
{
    public interface IBiology_Healthy
    {
        public BindableProperty<bool> isDeath { get; set; }
        
        public BindableProperty<float> curHealthy { get; set; }
        
        public BindableProperty<float> maxHealthy { get; set; }
        
        public BindableProperty<float> curArmor { get; set; }
        
        public BindableProperty<float> maxArmor { get; set; }

        public void SufferHarmed(TriggerDamageData_TemporalityPoolable damageData);

        public void Becuring(float cureValue);

        public void Death();
    }
    
    public class HealthyController : AbstractController,IBiology_Healthy,IUnRegisterList
    { 
        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();
        
        public HealthyStatusFollower healthyStatusFollower;
        
        public TriggerElementDamageData_TemporalityPoolable triggerElementDamageData_TemporalityPoolable;
        
        public BindableProperty<bool> isDeath { get; set; }= new BindableProperty<bool>(false);
        
        public BindableProperty<float> curHealthy { get; set; } = new BindableProperty<float>();
        
        public BindableProperty<float> maxHealthy { get; set; } = new BindableProperty<float>();
        
        public BindableProperty<float> curArmor { get; set; } = new BindableProperty<float>();
        
        public BindableProperty<float> maxArmor { get; set; } = new BindableProperty<float>();
        
        private bool isInvisible = false;
        
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="healthyData"></param>
        public override void InitData(WorldObj worldObj)
        {
            base.InitData(worldObj);
            
            healthyStatusFollower = PoolManager.Instance.healthyStatusFollower_Pool.Allocate();
            healthyStatusFollower.Show();
            
            isDeath.Register((data) =>
            {
                this.owner.Death(data);
                this.healthyStatusFollower.Death(data);
            }).AddToUnregisterList(this);
            
            curHealthy.Register((data) =>
            {
                this.healthyStatusFollower.ChangeHealthy(data,maxHealthy.Value);
            }).AddToUnregisterList(this);
            
            maxHealthy.Register((data) =>
            {
                this.healthyStatusFollower.ChangeHealthy(curHealthy.Value,data);
            }).AddToUnregisterList(this);
            
            curArmor.Register((data) =>
            {
                this.healthyStatusFollower.ChangeArmor(data,maxArmor.Value);
            }).AddToUnregisterList(this);
            
            maxArmor.Register((data) =>
            {
                this.healthyStatusFollower.ChangeArmor(curArmor.Value,data);
            }).AddToUnregisterList(this);

            SHealthyData healthyData = worldObj.thisDataConfig.HealthyData;
            this.curHealthy.Value = healthyData.maxHealth;
            this.maxHealthy.Value = healthyData.maxHealth;    
            this.curArmor.Value = healthyData.maxArmor;
            this.maxArmor.Value = healthyData.maxArmor;

            healthyStatusFollower.InitFollowerStatus(worldObj,healthyData);
            controller.onBeHarmedEvent += SufferHarmed;
            controller.onChangeInvincibleModEvent += ChangeInvincible;
        }

        public void ChangeInvincible(bool isInvincible)
        {
            this.isInvisible = isInvisible;
        }
        
        /// <summary>
        /// 受到伤害
        /// </summary>
        /// <param name="elementType"></param>
        public void SufferHarmed(TriggerDamageData_TemporalityPoolable damageData)
        {
            if(this.isDeath.Value || this.isInvisible)
                return;

            if (damageData.elementType != EElementType.None && damageData.willOverlayElementLevel)
            {
                SufferElement(damageData.elementType,damageData.enforcer);
            }

            ReduceHealthy(damageData.CaculateFinalDamage());
            RecycleDamageData();
        }

        /// <summary>
        /// 减少生命状态
        /// </summary>
        /// <param name="damage"></param>
        public void ReduceHealthy(float damage)
        {
            float deepValue = curArmor.Value - damage;
            if (deepValue >= 0)
            {
                curArmor.Value -= deepValue;
                if (curArmor.Value < 0)
                {
                    curArmor.SetValueWithoutEvent(0);
                }
            }
            else
            {
                curHealthy.Value += damage;
            }
            
            if (curHealthy.Value <= 0)
            {
                Death();
            }
        }

        /// <summary>
        /// 遭受元素伤害
        /// </summary>
        /// <param name="elementType"></param>
        public void SufferElement(EElementType elementType,WorldObj enforcer)
        {
            if (triggerElementDamageData_TemporalityPoolable == null || triggerElementDamageData_TemporalityPoolable.IsRecycled)
            {
                triggerElementDamageData_TemporalityPoolable = TriggerElementDamageData_TemporalityPoolable.Allocate();
            }
            triggerElementDamageData_TemporalityPoolable.UpdateSuffererAndEnforcer(enforcer,owner,elementType);
        }

        /// <summary>
        /// 治疗
        /// </summary>
        /// <param name="cureValue"></param>
        public void Becuring(float cureValue)
        {
            this.curHealthy.Value += cureValue;
            if (curHealthy.Value >= this.maxHealthy.Value)
            {
                this.curHealthy.Value = this.maxHealthy.Value;
            }
        }

        /// <summary>
        /// 死亡
        /// </summary>
        public void Death()
        {
            isDeath.Value = true;
        }

        private void RecycleDamageData()
        {
            this.triggerElementDamageData_TemporalityPoolable.Recycle2Cache();
        }
        
        public override void DeInitData()
        {
            this.UnRegisterAll();
            controller.onBeHarmedEvent -= SufferHarmed;
            controller.onChangeInvincibleModEvent -= ChangeInvincible;
        }
    }
}

