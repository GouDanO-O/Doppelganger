using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace GameFrame.Config
{
    [CreateAssetMenu(fileName = "UIPrefabsDataConfig",menuName = "配置/UI配置")]
    public class UIPrefabsDataConfig : SerializedScriptableObject
    {
        [LabelText("名称跟随对象")]
        public GameObject HealthyStatusFollowPrefab;
    }
}