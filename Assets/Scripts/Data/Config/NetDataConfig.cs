using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.Config
{
    [CreateAssetMenu(fileName = "NetDataConfig", menuName = "配置/网络配置")]
    public class NetDataConfig : ScriptableObject
    {
        [Header("短Tick检测间隔")]
        public int ShortTickTimeDeep =1;
        
        [Header("正常Tick检测间隔")]
        public int NormalTickTimeDeep =3;
        
        [Header("长Tick检测间隔")]
        public int LongTickTimeDeep =5;
    }
}

