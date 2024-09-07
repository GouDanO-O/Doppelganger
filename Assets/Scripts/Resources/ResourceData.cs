using Game.Config;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
{
    public interface IResourcesModel: IModel
    {
        
    }


    public class ResourceData : AbstractModel, IResourcesModel
    {

        public GameSettingConfig SettingConfig { get; set; }

        protected override void OnInit()
        {
            
        }



    }
}

