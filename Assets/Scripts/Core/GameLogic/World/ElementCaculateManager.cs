using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.Events;

namespace GameFrame.World
{
    public class ElementCaculateManager
    {
        private List<TriggerElementDamageData_TemporalityPoolable> activeElementData = new List<TriggerElementDamageData_TemporalityPoolable>();

        public static UnityAction<TriggerElementDamageData_TemporalityPoolable> onAddElementEffecterEvent;
        
        public static UnityAction<TriggerElementDamageData_TemporalityPoolable> onRemoveElementEffecterEvent;

        public ElementCaculateManager()
        {
            InitData();
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitData()
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
        private void AddElementData(TriggerElementDamageData_TemporalityPoolable elementData)
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
        private void RecycleElementData(TriggerElementDamageData_TemporalityPoolable elementData)
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