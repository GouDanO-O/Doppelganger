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
    ///
    /// 
    /// 这个是当前物体一个技能等级数据和其配置
    /// 技能可以被添加和移除
    /// 例如主动技能只有当玩家添加到主动技能列表中且主动触发时才会触发
    /// </summary>
    public class OwnedSkillData_TemporalityPoolable : TemporalityData_Pool
    {
        public SkillNodeData_Config skillNodeDataConfig { get;protected set; }

        public WorldObj owner;
        
        /// <summary>
        /// 当前等级
        /// </summary>
        public int curLevel { get; set; }

        /// <summary>
        /// 当前最大等级
        /// </summary>
        public int maxLevel { get; set; }
        
        /// <summary>
        /// 上一次使用技能的时间戳
        /// </summary>
        public float lastSkillUseTime { get; set; }
        
        /// <summary>
        /// 检测是否正确抵达时间--防止本地和服务器的时间戳对不上
        /// </summary>
        public float willEndTime { get; set; }
        
        /// <summary>
        /// 当前技能的CD
        /// </summary>
        public float skillCooldown { get; set; }

        public static OwnedSkillData_TemporalityPoolable Allocate()
        {
            return SafeObjectPool<OwnedSkillData_TemporalityPoolable>.Instance.Allocate();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="skillNodeDataConfig"></param>
        public void InitData(WorldObj owner,SkillNodeData_Config skillNodeDataConfig)
        {
            this.owner = owner;
            this.skillNodeDataConfig = skillNodeDataConfig;
            this.curLevel = 0;
            this.maxLevel = skillNodeDataConfig.MaxLevel;
            this.lastSkillUseTime = 0;
            this.skillCooldown = skillNodeDataConfig.SkillCooldown;
        }
        
        /// <summary>
        /// 升级
        /// 当达到最大等级时则不会继续升级,而是更换为其他奖励(后续添加)
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
        public bool CheckSkill(SkillNodeData_Config skillNodeDataConfig)
        {
            return this.skillNodeDataConfig == skillNodeDataConfig;
        }

        /// <summary>
        /// 触发技能前的检测
        /// </summary>
        public void ExcuateSkillCheck()
        {
            if (IsSkillReady())
            {
                lastSkillUseTime = Time.time;
                willEndTime = lastSkillUseTime + skillCooldown;
                ExcuateSkill();
                Debug.Log("技能执行:"+lastSkillUseTime+"--"+willEndTime);
            }
            else
            {
                Debug.Log("技能未准备就绪");
            }
        }
        
        /// <summary>
        /// 执行技能
        /// </summary>
        private void ExcuateSkill()
        {
            if (skillNodeDataConfig)
            {
                WorldManager.Instance.onAddSkillExecuter?.Invoke(this);
            }
        }
        
        /// <summary>
        /// 如果当前时间 - 上次使用技能的时间 >= 冷却时间，技能就可以再次使用
        /// 然后判断技能是否有执行条件
        /// 如果有条件则还需要判断条件是否满足
        /// </summary>
        /// <returns></returns>
        public bool IsSkillReady()
        {
            //判断冷却时间
            if (Time.time < lastSkillUseTime + skillCooldown)
                return false;
            
            //当前本地时间是否到达结束时间
            if (Time.time <= willEndTime)
                return false;
            
            //判断当前技能是否有执行条件,如果有执行条件则需要判断执行条件(后续添加)
            
            //还需要再判断一次服务器时间和本地时间对比,看是否能够执行(后续添加)
            
            //如果都通过了则可以执行
            return true;
        }
        
        /// <summary>
        /// 当回收时
        /// </summary>
        public override void DeInitData()
        {
            lastSkillUseTime = 0;
            skillCooldown = 0;
            owner = null;
            skillNodeDataConfig = null;
            curLevel = 0;
            maxLevel = 0;
            willEndTime = 0;
        }

        public override void OnRecycled()
        {
            DeInitData();
        }

        public override void Recycle2Cache()
        {
            SafeObjectPool<OwnedSkillData_TemporalityPoolable>.Instance.Recycle(this);
        }
    }
}