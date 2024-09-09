using GameFrame.Config;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame
{
    public interface IResourcesModel: IModel
    {
        
    }


    public class ResourcesModel : AbstractModel, IResourcesModel
    {

        public GameSettingConfig SettingConfig { get; set; }

        public NetDataConfig NetDataConfig { get; set; }
        
        protected override void OnInit()
        {
            
        }



    }
}

