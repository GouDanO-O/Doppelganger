using System.Collections.Generic;
using QFramework;
using UnityEngine.Events;

namespace GameFrame.World
{
    public class WorldManager : Singleton<WorldManager>,IController
    {
        private WorldManager() { }
        
        public List<WorldObj> players = new List<WorldObj>();
        
        public ElementCaculateManager elementCaculateManager { get; set; }
        
        public SkillExecuteManager skillExecuteManager { get; set; }
        
        /// <summary>
        /// 添加技能处理块
        /// </summary>
        public UnityAction<OwnedSkillData_TemporalityPoolable> onAddSkillExecuter;

        public override void OnSingletonInit()
        {
            base.OnSingletonInit();
            InitData();
        }

        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitData()
        {
            elementCaculateManager=new ElementCaculateManager();
            skillExecuteManager=new SkillExecuteManager();
            
            onAddSkillExecuter += skillExecuteManager.AddSkillExecuter;
        }

        /// <summary>
        /// 注销
        /// </summary>
        public void DeInitData()
        {
            elementCaculateManager.DeinitData();
        }
        
        /// <summary>
        /// 设置玩家
        /// </summary>
        /// <param name="player"></param>
        public void SetPlayer(WorldObj player)
        {
            this.players.Add(player);
            this.SendCommand(new AddCheat_Command("使用技能树", (() =>
            {
                
            })));
        }

        /// <summary>
        /// 更新世界逻辑
        /// </summary>
        public void UpdateWorldLogic()
        {
            elementCaculateManager.UpdateElementEffects(0.02f);
            skillExecuteManager.UpdateSkillExecution(0.02f);
        }
    }
}