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

        public void Beharmed(float damage);

        public void Becuring(float cureValue);

        public void Death();
    }
    
    
    public class HealthyController : AbstractController,IBiology_Healthy,IUnRegisterList
    { 
        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();
        
        public WorldObj worldObj;
        
        public HealthyStatusFollower healthyStatusFollower;
        
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
            isDeath.Register((data) =>
            {
                this.worldObj.Death(data);
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

            SHealthyData healthyData = worldObj.thisDataConfig.healthyData;
            this.curHealthy.Value = healthyData.maxHealth;
            this.maxHealthy.Value = healthyData.maxHealth;    
            this.curArmor.Value = healthyData.maxArmor;
            this.maxArmor.Value = healthyData.maxArmor;

            healthyStatusFollower = PoolManager.Instance.healthyStatusFollower_Pool.Allocate();
            healthyStatusFollower.Show();

            healthyStatusFollower.InitFollowerStatus(worldObj,healthyData);
        }
        
        /// <summary>
        /// 受伤
        /// </summary>
        /// <param name="damage"></param>
        public void Beharmed(float damage)
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
        

        public override void DeInitData()
        {
            this.UnRegisterAll();
        }
    }
}

