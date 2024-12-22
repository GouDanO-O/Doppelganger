using GameFrame.Config;
using GameFrame.World;
using QFramework;
using UnityEngine;

namespace GameFrame
{
    /// <summary>
    ///         *******目前存在的问题*******
    /// 目前的方案是走配置表中的流程去执行技能行为逻辑
    /// 但是会出现一个问题,如果有很多玩家读同一份配置表,这样会导致这个配置表的临时数据混乱
    /// 造成数据错误
    /// 解决方案A:每个玩家拥有一个技能时,深拷贝一份这个配置表
    /// 解决方案B:配置是配置逻辑,执行是执行逻辑,两者不能共用,当要执行技能时,执行逻辑会读取配置数据和玩家数据去执行,执行完毕回收(更改为这种方案)
    /// </summary>
    public class SOwnedSkillData : TemporalityData_Pool
    {
        public SkillNodeDataConfig skillNodeDataConfig { get;protected set; }
        
        public int curLevel { get; set; }

        public int maxLevel { get; set; }
        
        /// <summary>
        /// 上一次使用技能的时间戳
        /// </summary>
        public float lastSkillUseTime { get; set; }
        
        /// <summary>
        /// 检测是否正确抵达时间--防止本地和服务器的时间戳对不上
        /// </summary>
        public float willEndTime { get; set; }
        
        public float skillCooldown { get; set; }

        public static SOwnedSkillData Allocate()
        {
            return SafeObjectPool<SOwnedSkillData>.Instance.Allocate();
        }

        public void InitData(SkillNodeDataConfig skillNodeDataConfig)
        {
            this.skillNodeDataConfig = skillNodeDataConfig;
            this.curLevel = 0;
            this.lastSkillUseTime = 0;
            this.skillCooldown=skillNodeDataConfig.SkillCooldown;
            this.maxLevel = skillNodeDataConfig.MaxLevel;
        }

        /// <summary>
        /// 触发技能
        /// </summary>
        public void TriggerSkill(WorldObj owner=null)
        {
            if (IsSkillReady())
            {
                lastSkillUseTime = Time.time;
                willEndTime = lastSkillUseTime + skillCooldown;
                skillNodeDataConfig.TriggerSkill(owner);
            }
        }
        
        /// <summary>
        /// 如果当前时间 - 上次使用技能的时间 >= 冷却时间，技能就可以再次使用
        /// </summary>
        /// <returns></returns>
        public bool IsSkillReady()
        {
            if (Time.time <= willEndTime)
            {
                return false;
            }
            return Time.time >= lastSkillUseTime + skillCooldown;
        }
        
        /// <summary>
        /// 升级
        /// </summary>
        public void LevelUp()
        {
            curLevel = Mathf.Min(curLevel + 1, maxLevel); 
        }
        
        /// <summary>
        /// 检测是否是相同技能
        /// </summary>
        /// <param name="skillNodeDataConfig"></param>
        /// <returns></returns>
        public bool CheckSkill(SkillNodeDataConfig skillNodeDataConfig)
        {
            return this.skillNodeDataConfig == skillNodeDataConfig;
        }

        public override void DeInitData()
        {
            
        }

        public override void OnRecycled()
        {
            
        }

        public override void Recycle2Cache()
        {
            
        }
    }
}