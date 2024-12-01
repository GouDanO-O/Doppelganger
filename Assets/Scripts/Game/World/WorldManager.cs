using QFramework;

namespace GameFrame.World
{
    public class WorldManager : Singleton<WorldManager>,IController
    {
        public PlayerController playerController { get;private set; }

        private WorldManager(){}
        
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
        
        public void SetPlayer(PlayerController player)
        {
            this.playerController = player;
            this.SendCommand(new AddCheatCommand("使用技能树", (() =>
            {
                if (playerController)
                {
                    playerController.skillController.UseSkill();
                }
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