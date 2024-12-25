using System;
using QFramework;

namespace GameFrame
{
    public class AddCheatCommand : AbstractCommand
    {
        protected string Name;

        protected Action CheatAction;

        public AddCheatCommand(string name, Action action)
        {
            Name = name;
            CheatAction = action;
        }
        
        protected override void OnExecute()
        {
            this.GetModel<CheatData_Model>().AddCheatModule(Name,CheatAction);
        }
    }
}