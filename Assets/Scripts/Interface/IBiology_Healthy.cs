using QFramework;

namespace GameFrame
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
}