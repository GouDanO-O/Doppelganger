using GameFrame;
using QFramework;

namespace Command
{
    public class GameIsPause_Command : AbstractCommand
    {
        public bool willPasue = false;

        public GameIsPause_Command(bool willPasue)
        {
            willPasue = willPasue;
        }
        
        protected override void OnExecute()
        {
            this.GetModel<GameData_Model>().ChangGamePasuing(willPasue);
        }
    }
}