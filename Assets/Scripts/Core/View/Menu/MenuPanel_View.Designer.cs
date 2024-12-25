using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace GameFrame.UI
{
	// Generate Id:99c899e6-a2f6-4d31-9de7-f1e6d3846083
	public partial class MenuPanel_View
	{
		public const string Name = "UI_MenuPanel";
		
		
		private UI_MenuPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			
			mData = null;
		}
		
		public UI_MenuPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		UI_MenuPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new UI_MenuPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
