using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using System;

namespace GameFrame
{
    public class CheaterData
    {
        public string Name { get; set; } 
        public Action CheatAction { get; set; }  

        public CheaterData(string name, System.Action action)
        {
            Name = name;
            CheatAction = action;
        }

        
        public void Execute()
        {
            CheatAction?.Invoke();
        }
    }
    
    public class CheatModel : AbstractModel
    {
        public List<CheaterData> cheatModules = new List<CheaterData>();
        
        protected override void OnInit()
        {
            
        }
        
        public void AddCheatModule(string name="", System.Action action=null)
        {
            cheatModules.Add(new CheaterData(name, action));
        }

        public List<CheaterData> GetCheaterDatas()
        {
            return cheatModules;
        }
    } 
}

