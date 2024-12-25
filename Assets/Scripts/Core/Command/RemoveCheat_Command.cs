using QFramework;

namespace GameFrame
{
    public class RemoveCheatCommand : AbstractCommand
    {
        protected string Name;

        public RemoveCheatCommand(string name)
        {
            Name = name;
        }
        
        protected override void OnExecute()
        {
            this.GetModel<CheatData_Model>().RemoveCheatModule(Name);
        }
    }
}