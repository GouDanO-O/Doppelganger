using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.World;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameFrame.Config
{
    [Serializable]
    public class SCommonProjectileData_Persistence : PersistentData
    {
        [LabelText("是否从对象池中进行加载")]
        public bool isLoadFromPool;
        
        [ShowIf("isLoadFromPool")]
        public EObjectPoolType ObjectPoolType;
        
        [ShowIf("@isLoadFromPool ==false"),LabelText("物体")]
        public GameObject ObjectPrefab;
        
        [LabelText("发射次数"),MinValue(1)]
        public int FireCount =1;

        [LabelText("飞行速度"),MinValue(0)]
        public float MoveSpeed;
        
        [ShowIf("@FireCount >1"),LabelText("开火间隔"),MinValue(0)]
        public float FireDelayTime;

        [ShowIf("@FireCount >1"),LabelText("间隔角度(以一个扇形来计算)"),MinValue(0)]
        public float FireFurcationAngle;
        
        [LabelText("最大飞行距离"),MinValue(0)]
        public int MaxFlyDistance;
        
        [LabelText("碰撞到不同碰撞等级物体时的处理(共有4种碰撞等级)")]
        public EAction_Projectile_CollisionType[] CollisionTypes=new EAction_Projectile_CollisionType[4];

        [LabelText("伤害衰减比例(只有当碰到敌人时才会进行衰减,如果没有就代表没有衰减)")]
        public float[] DamageAttenuations;

        public EAction_Projectile_ShootType ShootProjectileType;
        
        [ShowIf("@ShootProjectileType==EAction_Projectile_ShootType.Parabola"),LabelText("抛物线最高点")]
        public float MaxParabolaHeight;

        public EAction_Skill_ElementType elementType;

        [ShowIf("@elementType!=EAction_Skill_ElementType.None"),LabelText("最大元素可积累等级")]
        public float maxElementAccLevel;
        
        [ShowIf("@elementType!=EAction_Skill_ElementType.None"),LabelText("元素伤害")]
        public float elementDamage;
        
        [ShowIf("@elementType!=EAction_Skill_ElementType.None"),LabelText("最大元素伤害")]
        public float maxElementDamage;
        
        [ShowIf("@elementType!=EAction_Skill_ElementType.None"),LabelText("每次升级增加的伤害比例")]
        public List<float> elementLevelUpAddedDamage;
        
        [ShowIf("@elementType!=EAction_Skill_ElementType.None"),LabelText("元素造成伤害的间隔时间")]
        public float elementTriggerInterval;
        
        [ShowIf("@elementType!=EAction_Skill_ElementType.None"),LabelText("每次升级减少造成伤害的间隔时间")]
        public List<float> elementLevelUpDesriggerInterval;
        
        public void ClearData()
        {
            
        }

        public void SaveData()
        {
            
        }
        
        // 使用 OnValidate 来动态调整列表长度
        private void OnValidate()
        {
            // 限制 elementLevelUpAddedDamage 列表长度
            if (elementLevelUpAddedDamage.Count > maxElementAccLevel)
            {
                elementLevelUpAddedDamage.RemoveRange((int)maxElementAccLevel, elementLevelUpAddedDamage.Count - (int)maxElementAccLevel);
            }
            else
            {
                while (elementLevelUpAddedDamage.Count < maxElementAccLevel)
                {
                    elementLevelUpAddedDamage.Add(0f); // 填充默认值
                }
            }

            // 限制 elementLevelUpDesriggerInterval 列表长度
            if (elementLevelUpDesriggerInterval.Count > maxElementAccLevel)
            {
                elementLevelUpDesriggerInterval.RemoveRange((int)maxElementAccLevel, elementLevelUpDesriggerInterval.Count - (int)maxElementAccLevel);
            }
            else
            {
                while (elementLevelUpDesriggerInterval.Count < maxElementAccLevel)
                {
                    elementLevelUpDesriggerInterval.Add(0f); // 填充默认值
                }
            }
        }
    }
    
    /// <summary>
    /// 普通弹体类型
    /// </summary>
    [CreateAssetMenu(fileName = "CommonProjectile",menuName = "配置/技能/行为/弹体/普通弹体")]
    public class SActionClip_DetailAction_CommonProjectile : SActionClip_DetailAction_Basic
    {
        [LabelText("弹体数据")]
        public SCommonProjectileData_Persistence persistenceProjectileData;

        public override void StartExecute()
        {
            base.StartExecute();
        }

        public override void Trigger()
        {
            base.Trigger();
            ShootProjectileCheck();
        }

        /// <summary>
        /// 发射弹体前置检查
        /// </summary>
        protected virtual void ShootProjectileCheck()
        {
            for (int i = 0; i < persistenceProjectileData.FireCount; i++)
            {
                if (persistenceProjectileData.FireDelayTime > 0)
                {
                    Main.Interface.GetUtility<CoroutineUtility>().StartRoutine(FireTimeDelay(i));
                }
                else
                {
                    ShootProjectile(i);
                }
            }
        }

        IEnumerator FireTimeDelay(int curShootCount)
        {
            yield return new WaitForSeconds(persistenceProjectileData.FireDelayTime);
            ShootProjectile(curShootCount);
        }

        /// <summary>
        /// 发射弹体
        /// </summary>
        protected virtual void ShootProjectile(int curShootCount)
        {
            GameObject projectile = null;
            if (persistenceProjectileData.isLoadFromPool)
            {
                projectile = PoolManager.Instance.LoadObjFromPool(persistenceProjectileData.ObjectPoolType);
            }
            else
            {
                projectile = Instantiate(persistenceProjectileData.ObjectPrefab);
            }
            if(projectile==null)
                return;
            ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
            projectileController.InitData(persistenceProjectileData);
        }
    }
}