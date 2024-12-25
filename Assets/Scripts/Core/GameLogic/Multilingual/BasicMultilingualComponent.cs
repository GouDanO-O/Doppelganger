using System;
using QFramework;
using UnityEngine;

namespace GameFrame.Multilingual
{
    public class BasicMultilingualComponent : MonoBehaviour
    {
        public string key;

        public bool willActivateOnEnable;
        private void OnEnable()
        {
            if (willActivateOnEnable)
            {
                UpdateMultilingual();
            }
        }

        private void Start()
        {
            UpdateMultilingual();
            TypeEventSystem.Global.Register<SChangeMultingual_Event>((eventData) =>
            {
                UpdateMultilingual();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        
        /// <summary>
        /// 更新多语言
        /// </summary>
        protected virtual void UpdateMultilingual()
        {
            
        }
    }
}