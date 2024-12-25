using QFramework;

namespace GameFrame
{
    public class RemoveCheat_Command : AbstractCommand
    {
        protected string Name;

        public RemoveCheat_Command(string name)
        {
            Name = name;
        }
        
        protected override void OnExecute()
        {
            this.GetModel<CheatData_Model>().RemoveCheatModule(Name);
        }
    }
}