using GameFrame.Config;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameFrame
{
    public class ResourcesData_Model : AbstractModel
    {
        public GameSetting_Config SettingConfig { get; set; }

        public NetData_Config NetDataConfig { get; set; }
        
        public InputActionAsset InputActionAsset { get; set; }
        
        public UIPrefabsData_Config UIPrefabsDataConfig { get; set; }
        
        protected override void OnInit()
        {
            
        }



    }
}

