using GameFrame.Config;
using GameFrame.World;
using UnityEngine;

namespace GameFrame
{
    public class SOwnedSkillData
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

        public SOwnedSkillData(SkillNodeDataConfig skillNodeDataConfig)
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
        public void TriggerSkill(WorldObj owner=null,WorldObj target=null)
        {
            if (IsSkillReady())
            {
                lastSkillUseTime = Time.time;
                willEndTime = lastSkillUseTime + skillCooldown;
                skillNodeDataConfig.TriggerSkill(owner, target);
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
    }
}