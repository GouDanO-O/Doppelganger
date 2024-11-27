using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.Events;

namespace GameFrame.World
{
    public class ElementCaculateManager : IController
    {
        private List<TriggerElementDamageData_Temporality> activeElementData = new List<TriggerElementDamageData_Temporality>();

        public static UnityAction<TriggerElementDamageData_Temporality> onAddElementEffecterEvent;
        
        public static UnityAction<TriggerElementDamageData_Temporality> onRemoveElementEffecterEvent;

        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void InitData()
        {
            onAddElementEffecterEvent += AddElementData;
            onRemoveElementEffecterEvent += RecycleElementData;
        }

        /// <summary>
        /// 注销
        /// </summary>
        public void DeinitData()
        {
            
        }

        /// <summary>
        /// 添加元素块
        /// </summary>
        /// <param name="data"></param>
        private void AddElementData(TriggerElementDamageData_Temporality elementData)
        {
            if (!activeElementData.Contains(elementData))
            {
                activeElementData.Add(elementData);
            }
        }
        
        /// <summary>
        /// 回收元素块
        /// </summary>
        /// <param name="elementData"></param>
        private void RecycleElementData(TriggerElementDamageData_Temporality elementData)
        {
            if (activeElementData.Contains(elementData))
            {
                activeElementData.Remove(elementData);
                elementData.Recycle2Cache();
            }
        }

        /// <summary>
        /// 更新当前所有元素块的持续时间
        /// </summary>
        /// <param name="deltaTime"></param>
        public void UpdateElementEffects(float deltaTime)
        {
            if(activeElementData.Count == 0)
                return;

            for (int i = 0; i < activeElementData.Count; i++)
            {
                activeElementData[i].UpdateElementDuration(deltaTime);
            }
        }
    }
}