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
    
    public class CheatData_Model : AbstractModel
    {
        public Dictionary<string,CheaterData> cheatModules = new Dictionary<string,CheaterData>();
        
        protected override void OnInit()
        {
            
        }
        
        public void AddCheatModule(string name="", System.Action action=null)
        {
            if (!cheatModules.ContainsKey(name))
            {
                cheatModules.Add(name,new CheaterData(name, action));
            }
        }

        public void RemoveCheatModule(string name)
        {
            if (cheatModules.ContainsKey(name))
            {
                cheatModules.Remove(name);  
            }
        }
        
        public Dictionary<string,CheaterData> GetCheaterDatas()
        {
            return cheatModules;
        }
    } 
}

