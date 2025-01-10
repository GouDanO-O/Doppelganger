using System.Collections.Generic;
using GameFrame.Config;
using QFramework;
using UnityEngine;
using UnityEngine.Events;

namespace GameFrame.World
{
    public class WorldManager : Singleton<WorldManager>,IController
    {
        private WorldManager() { }
        
        private List<WorldObj> players = new List<WorldObj>();
        
        public ElementCaculateManager elementCaculateManager { get; protected set; }
        
        public SkillExecuteManager skillExecuteManager { get; protected set; }
        
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
            
            #if TestMod
            SpawnPlayer_Test();
            #endif
        }

        /// <summary>
        /// 注销
        /// </summary>
        public void DeInitData()
        {
            onAddSkillExecuter -= skillExecuteManager.AddSkillExecuter;
            elementCaculateManager.DeinitData();
        }
        
        /// <summary>
        /// 生成玩家
        /// </summary>
        /// <param name="playerConfig"></param>
        private void SpawnPlayer(WorldObjData_Config playerConfig)
        {
            
        }

        #region 测试模式

        /// <summary>
        /// 生成玩家
        /// </summary>
        private void SpawnPlayer_Test()
        {
            WorldObjData_Config testPlayerConfig = GameManager.Instance.testerWorldObjDataConfig;
            if (testPlayerConfig)
            {
                GameObject player = GameObject.Instantiate(testPlayerConfig.ThisPrefab);
                WorldObj playerObj = player.GetComponent<WorldObj>();
                playerObj.ChangePlayerControlling(true);
                AddPlayer(playerObj);
                SetPlayer_Test(playerObj);
            }
        }

        /// <summary>
        /// 设置玩家
        /// </summary>
        /// <param name="player"></param>
        public void SetPlayer_Test(WorldObj player)
        {
            this.SendCommand(new AddCheat_Command("使用技能树", (() =>
            {
                player.skillController.UseSkill();
            })));
        }
        
        #endregion

        /// <summary>
        /// 添加一名玩家
        /// </summary>
        /// <param name="newPlayer"></param>
        public void AddPlayer(WorldObj newPlayer)
        {
            this.players.Add(newPlayer);
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