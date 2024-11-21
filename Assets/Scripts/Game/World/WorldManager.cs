using QFramework;

namespace GameFrame.World
{
    public class WorldManager : MonoNetSingleton<WorldManager>
    {
        public PlayerController playerController { get;private set; }
        
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
        
        
    }
}