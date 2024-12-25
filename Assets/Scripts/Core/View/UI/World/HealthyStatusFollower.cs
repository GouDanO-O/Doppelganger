using System;
using GameFrame.Config;
using GameFrame.World;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using TMPro;

namespace GameFrame.UI
{
	public class HealthyStatusFollowerData : UIPanelData
	{
		
	}
	public partial class HealthyStatusFollower : UIPanel
	{
		protected WorldObj worldObj;

		protected GameObject Root;
		
		protected TextMeshProUGUI HealthText;

		protected TextMeshProUGUI ArmorText;
		
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as HealthyStatusFollowerData ?? new HealthyStatusFollowerData();
			// please add init code here
			Root = transform.Find("Root").gameObject;
			HealthText=Root.transform.Find("HealthText").GetComponent<TextMeshProUGUI>();
			ArmorText=Root.transform.Find("ArmorText").GetComponent<TextMeshProUGUI>();
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
			
		}
		
		protected override void OnShow()
		{
			
		}
		
		protected override void OnHide()
		{
			Root.SetActive(false);
		}
		
		protected override void OnClose()
		{
			
		}

		public void InitFollowerStatus(WorldObj worldObj,SHealthyData healthyData)
		{
			this.worldObj = worldObj;
			HealthText.text = healthyData.maxHealth + " / " + healthyData.maxHealth;
			ArmorText.text = healthyData.maxArmor + " / " + healthyData.maxArmor;
		}

		private void Update()
		{
			UpdateFollow();
		}

		public virtual void UpdateFollow()
		{
			if (worldObj)
			{
				if (CheckIsInCameraArea())
				{
					Root.SetActive(true);
				}
				else
				{
					Root.SetActive(false);
				}
			}
			else
			{
				RecyleThis();
			}
		}

		/// <summary>
		/// 检查是否在相机范围内
		/// 如果不在就不显示
		/// </summary>
		/// <returns></returns>
		protected virtual bool CheckIsInCameraArea()
		{
			return false;
		}
		
		public virtual void Death(bool isDeath)
		{
			if (isDeath)
			{
				RecyleThis();
			}
		}

		public virtual void RecyleThis()
		{
			PoolManager.Instance.healthyStatusFollower_Pool.Recycle(this);
		}
		
		public virtual void ChangeHealthy(float curHealthy, float maxHealthy)
		{
			HealthText.text = curHealthy +" / " + maxHealthy;
		}

		public virtual void ChangeArmor(float curArmor, float maxArmor)
		{
			ArmorText.text = curArmor +" / " + maxArmor;
		}
	}
}
