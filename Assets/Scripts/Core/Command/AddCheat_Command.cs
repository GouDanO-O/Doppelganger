using System;
using QFramework;

namespace GameFrame
{
    public class AddCheat_Command : AbstractCommand
    {
        protected string Name;

        protected Action CheatAction;

        public AddCheat_Command(string name, Action action)
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