using Cinemachine;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.World
{
    public class CameraController : MonoBehaviour, IController
    {
        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }

        private CinemachineVirtualCamera virtualCamera;

        private void Start()
        {
            virtualCamera=GetComponent<CinemachineVirtualCamera>();
            this.RegisterEvent<SInputEvent_MouseDrag>(mouseData =>
            {
                MouseDrag(mouseData);
            });
            this.RegisterEvent<SInputEvent_MouseLeftClick>(mouseData =>
            {
                LeftMouseClick(mouseData);
            });
            this.RegisterEvent<SInputEvent_MouseRightClick>(mouseData =>
            {
                RightMouseClick(mouseData);
            });
        }

        private void MouseDrag(SInputEvent_MouseDrag dragData)
        {

        }

        private void LeftMouseClick(SInputEvent_MouseLeftClick clickData)
        {

        }

        private void RightMouseClick(SInputEvent_MouseRightClick clickData)
        {

        }
    }
}

