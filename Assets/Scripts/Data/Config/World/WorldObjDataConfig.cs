using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.World;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameFrame.Config
{
    [CreateAssetMenu(fileName = "WorldObj",menuName = "配置/世界/基础物体配置")]
    public class WorldObjDataConfig : SerializedScriptableObject
    {
        [Required(InfoMessageType.Error),LabelText("预制体")]
        public GameObject ThisPrefab;
        
        [LabelText("碰撞等级")]
        public EWorldObjCollisionType CollisionType;
        
        [Space(3),LabelText("是否有技能")]
        public bool HasSkill;
        
        [ShowIf("HasSkill"),LabelText("技能树")]
        public SkillTreeConfig skillTree;
        
        [LabelText("重力"),Range(-20,0)]
        public float Gravity;
        
        [Space(3),LabelText("是否具有生命")]
        public bool Healthyable;
        
        [ShowIf("Healthyable")]
        public SHealthyData HealthyData;
        
        [ShowIf("Healthyable"),LabelText("元素属性配置--归属玩家的可成长数据", SdfIconType.Box),DictionaryDrawerSettings()]
        public Dictionary<EElementType,SElementPropertyData> ElementPropertyDataDict = new Dictionary<EElementType,SElementPropertyData>();
        
        [ShowIf("Healthyable"),LabelText("元素伤害配置--归属玩家的不可成长属性--是对其他物体或玩家造成的伤害", SdfIconType.Box),DictionaryDrawerSettings()]
        public Dictionary<EElementType,ElementDamageData_Persistent> ElementDamageDataDict = new Dictionary<EElementType,ElementDamageData_Persistent>();

        [Header("变身所需要的能量,x=>仅外貌, y=>完全变身")]
        [LabelText("")]
        public Vector2 CostDeformationEnergy;
        
        [LabelText("是否能够进行移动(不会接受输入数据,由物体本身控制移动)")]
        public bool Moveable;

        [ShowIf("Moveable")]
        public SMoveData MoveData;

        [Space(3)]
        [ShowIf("Moveable"), LabelText("是否能够进行闪烁")]
        public bool Dashable;

        [ShowIf("@Moveable && Dashable")]
        public SDashData DashData;

        [Space(3)]
        [ShowIf("Moveable"), LabelText("是否能够进行跳跃")]
        public bool Jumpable;

        [ShowIf("@Moveable && Jumpable")]
        public SJumpData JumpData;

        [Space(3)]
        [ShowIf("Moveable"), LabelText("是否能够进行蹲伏")]
        public bool Crouchable;

        [ShowIf("@Moveable && Crouchable")]
        public SCrouchData CrouchData;
        
        [Space(3),LabelText("是否有攻击模块")]
        public bool WeaponAttackable;

        [ShowIf("@Healthyable && WeaponAttackable")]
        public SPlayerWeaponData PlayerWeaponData;
        
        [ShowIf("@Healthyable && WeaponAttackable"), LabelText("初始武器", SdfIconType.Box)]
        public List<BasicWeaponDataConfig> InitalWeaponsList;
    }
}

