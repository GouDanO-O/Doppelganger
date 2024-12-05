using System.Collections.Generic;
using QFramework;

namespace GameFrame.World
{
    public class WorldManager : Singleton<WorldManager>,IController
    {
        private WorldManager(){}
        
        public List<WorldObj> players = new List<WorldObj>();
        
        public ElementCaculateManager elementCaculateManager { get; set; }

        public override void OnSingletonInit()
        {
            base.OnSingletonInit();
            InitData();
        }

        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }
        
        private void InitData()
        {
            elementCaculateManager=new ElementCaculateManager();
        }

        public void DeInitData()
        {
            elementCaculateManager.DeinitData();
        }
        
        public void SetPlayer(WorldObj player)
        {
            this.players.Add(player);
            this.SendCommand(new AddCheatCommand("使用技能树", (() =>
            {
                
            })));
        }

        public void UpdateWorldLogic()
        {
            UpdateElementCaculateManager();
        }

        private void UpdateElementCaculateManager()
        {
            elementCaculateManager.UpdateElementEffects(0.02f);
        }
    }
}