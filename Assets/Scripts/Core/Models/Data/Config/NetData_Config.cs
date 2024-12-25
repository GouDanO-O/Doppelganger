using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    [CreateAssetMenu(fileName = "NetDataConfig", menuName = "配置/网络配置")]
    public class NetData_Config : SerializedScriptableObject
    {
        [LabelText("Tick时长")]
        public float TickTime=0.04f;
        
        [LabelText("短Tick检测间隔")]
        public int ShortTickTimeDeep =1;
        
        [LabelText("正常Tick检测间隔")]
        public int NormalTickTimeDeep =3;
        
        [LabelText("长Tick检测间隔")]
        public int LongTickTimeDeep =5;

        [LabelText("心跳断联次数")]
        public int HeartDisconnectCount = 3;
    }
}

