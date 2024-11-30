using GameFrame.Config;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameFrame
{
    public interface IResourcesModel: IModel
    {
        
    }


    public class ResourcesModel : AbstractModel, IResourcesModel
    {

        public GameSettingConfig SettingConfig { get; set; }

        public NetDataConfig NetDataConfig { get; set; }
        
        public InputActionAsset InputActionAsset { get; set; }
        
        public UIPrefabsDataConfig UIPrefabsDataConfig { get; set; }
        
        protected override void OnInit()
        {
            
        }



    }
}

