using System;
using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine.Events;

namespace GameFrame.World
{
using System.Threading;
using System.Threading.Tasks;

public class TriggerElementDamageData_Temporality : TemporalityData
{
    public Dictionary<EAction_Skill_ElementType, int> elementLevelDic = new Dictionary<EAction_Skill_ElementType, int>();
    
    public Dictionary<EAction_Skill_ElementType, float> elementDamageDic = new Dictionary<EAction_Skill_ElementType, float>();
    
    public Dictionary<EAction_Skill_ElementType, float> lastElementDurationDic = new Dictionary<EAction_Skill_ElementType, float>();

    private bool isElementDisappear = false;
    
    private CancellationTokenSource cancellationTokenSource;
    
    private Task calculationTask;
    
    private object lockObj = new object(); // 用于线程同步

    public UnityAction<float,EAction_Skill_ElementType> onElementDamageTriggerEvent;

    // 添加元素并启动计算线程
    public void AddElement(EAction_Skill_ElementType element)
    {
        lock (lockObj)
        {
            if (elementLevelDic.ContainsKey(element))
            {
                elementLevelDic[element]++;
            }
            else
            {
                elementLevelDic.Add(element, 1);
            }

            // 如果计算线程未启动或已停止，启动线程
            if (calculationTask == null || calculationTask.IsCompleted)
            {
                StartCalculationThread();
            }
        }
    }

    // 检查所有元素是否都消失
    public bool IsElementDisappear()
    {
        lock (lockObj)
        {
            isElementDisappear = lastElementDurationDic.Count == 0 && elementLevelDic.Count == 0;
            return isElementDisappear;
        }
    }

    // 启动计算线程
    private void StartCalculationThread()
    {
        cancellationTokenSource = new CancellationTokenSource();
        var token = cancellationTokenSource.Token;

        calculationTask = Task.Run(() => 
        {
            while (!token.IsCancellationRequested)
            {
                lock (lockObj)
                {
                    // 更新元素持续时间
                    UpdateElementDuration();

                    // 检查是否所有元素都已消失
                    if (IsElementDisappear())
                    {
                        // 停止线程
                        cancellationTokenSource.Cancel();
                        break;
                    }
                }

                // 等待一段时间再计算（例如，每秒计算一次）
                Thread.Sleep(1000);
            }
        }, token);
    }

    // 更新元素持续时间和计算伤害
    private void UpdateElementDuration()
    {
        // 遍历所有元素类型
        foreach (var element in elementLevelDic.Keys.ToList())
        {
            // 更新持续时间
            if (!lastElementDurationDic.ContainsKey(element))
            {
                lastElementDurationDic[element] = GetInitialDuration(element);
            }
            else
            {
                lastElementDurationDic[element] -= 1.0f; 
            }
            
            if (lastElementDurationDic[element] <= 0)
            {
                lastElementDurationDic.Remove(element);
                elementLevelDic.Remove(element);
            }
            else
            {
                elementDamageDic[element] = CalculateElementDamage(element, elementLevelDic[element]);
            }
        }
    }

    // 获取元素的初始持续时间
    private float GetInitialDuration(EAction_Skill_ElementType element)
    {
        if(lastElementDurationDic.ContainsKey(element))
            return lastElementDurationDic[element];
        return 0;
    }

    // 计算元素伤害
    private float CalculateElementDamage(EAction_Skill_ElementType element, int level)
    {
        // 注意：不要在这里调用 Unity API
        float damage = level * 10.0f;
        
        
        return damage;
    }

    // 在对象被销毁或不再需要时调用
    public void StopCalculation()
    {
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
        }
    }

    public override void DeInitData()
    {
        StopCalculation();

        lock (lockObj)
        {
            elementLevelDic.Clear();
            elementDamageDic.Clear();
            lastElementDurationDic.Clear();
        }
    }
}

}