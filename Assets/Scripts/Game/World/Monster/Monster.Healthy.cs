using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.World;
using UnityEngine;

namespace GameFrame.World
{
    public partial class Monster : IWorldObj_Healthy
    {
        public bool isDeath { get; set; }
        public float curHealthy { get; set; }
        public float maxHealthy { get; set; }

        public void Beharmed(float damage)
        {
            
        }
    }
}


