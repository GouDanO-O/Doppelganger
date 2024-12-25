using QFramework;

namespace Command
{
    public class GameIsPause_Command : AbstractCommand
    {
        public bool isPasued = false;

        public GameIsPause_Command(bool willPasue)
        {
            isPasued = willPasue;
        }
        
        protected override void OnExecute()
        {
            
        }
    }
}