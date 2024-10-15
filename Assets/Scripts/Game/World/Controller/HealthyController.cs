using System.Collections;
using System.Collections.Generic;
using GameFrame.Config;
using GameFrame.World;
using QFramework;
using UnityEngine;

namespace GameFrame.World
{
    public interface IBiology_Healthy
    {
        public BindableProperty<bool> isDeath { get; set; }
        
        public BindableProperty<float> curHealthy { get; set; }
        
        public BindableProperty<float> maxHealthy { get; set; }
        
        public BindableProperty<float> curArmor { get; set; }
        
        public BindableProperty<float> maxArmor { get; set; }

        public void Beharmed(float damage);

        public void Becuring(float cureValue);

        public void Death();
    }
    
    public class HealthyController : BasicController,IBiology_Healthy
    {
        public BindableProperty<bool> isDeath { get; set; }= new BindableProperty<bool>(false);
        
        public BindableProperty<float> curHealthy { get; set; } = new BindableProperty<float>();
        
        public BindableProperty<float> maxHealthy { get; set; } = new BindableProperty<float>();
        
        public BindableProperty<float> curArmor { get; set; } = new BindableProperty<float>();
        
        public BindableProperty<float> maxArmor { get; set; } = new BindableProperty<float>();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="healthyData"></param>
        public override void InitData(WorldObj worldObj)
        {
            base.InitData(worldObj);
            SHealthyData healthyData=owner.thisDataConfig.healthyData;
            this.curHealthy.Value = healthyData.maxHealth;
            this.maxHealthy.Value = healthyData.maxHealth;    
            this.curArmor.Value = healthyData.maxArmor;
            this.maxArmor.Value = healthyData.maxArmor;
            this.isDeath.Value = false;
        }

        /// <summary>
        /// 注销
        /// </summary>
        public override void DeInitData()
        {
            
        }
        
        /// <summary>
        /// 受伤
        /// </summary>
        /// <param name="damage"></param>
        public virtual void Beharmed(float damage)
        {
            if(this.isDeath.Value)
                return;
            this.curHealthy.Value -= damage;
            if (curHealthy.Value <= 0)
            {
                Death();
            }
        }

        /// <summary>
        /// 治疗
        /// </summary>
        /// <param name="cureValue"></param>
        public virtual void Becuring(float cureValue)
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
        public virtual void Death()
        {
            isDeath.Value = true;
        }
    }
}

