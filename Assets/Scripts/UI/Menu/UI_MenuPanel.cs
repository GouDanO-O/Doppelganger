using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace GameFrame.UI
{
	public class UI_MenuPanelData : UIPanelData
	{
	}
	public partial class UI_MenuPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UI_MenuPanelData ?? new UI_MenuPanelData();
			// please add init code here
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
