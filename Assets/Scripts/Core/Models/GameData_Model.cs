using QFramework;

namespace GameFrame
{
    public class GameData_Model : AbstractModel
    {
        public bool isPausing { get;protected set; }

        public void ChangGamePasuing(bool willPaused)
        {
            isPausing = willPaused;
        }
        
        protected override void OnInit()
        {
            isPausing = false;
        }
    }
}