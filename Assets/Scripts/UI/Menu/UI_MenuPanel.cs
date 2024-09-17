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
		protected Transform LocalModRoot;

		protected Button StartGameButton;

		protected Button LoadGameButton;


        protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UI_MenuPanelData ?? new UI_MenuPanelData();
			// please add init code here

			LocalModRoot = transform.Find("LocalModRoot");
			StartGameButton=LocalModRoot.Find("StartGameButton").GetComponent<Button>();
            LoadGameButton = LocalModRoot.Find("LoadGameButton").GetComponent<Button>();

			StartGameButton.onClick.AddListener(TryStartGame);
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

		protected void TryStartGame()
		{
			GameManager.Instance.StartGame();
		}
	}
}
