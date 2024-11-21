using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace GameFrame.UI
{
	// Generate Id:3fd438c2-e2a6-4460-ba7d-5539122b4d8f
	public partial class HealthyStatusFollower
	{
		public const string Name = "HealthyStatusFollower";
		
		
		private HealthyStatusFollowerData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public HealthyStatusFollowerData Data
		{
			get
			{
				return mData;
			}
		}
		
		HealthyStatusFollowerData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new HealthyStatusFollowerData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
