using Cinemachine;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.World
{
    public class CameraController : MonoSingleton<CameraController>, IController,IUnRegisterList
    {
        public List<IUnRegister> UnregisterList { get; } = new List<IUnRegister>();

        public IArchitecture GetArchitecture()
        {
            return Main.Interface;
        }

        public CinemachineVirtualCamera virtualCamera { get; set; }

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            this.RegisterEvent<SInputEvent_MouseDrag>(mouseData =>
            {
                MouseDrag(mouseData);
            }).AddToUnregisterList(this);
            this.RegisterEvent<SInputEvent_MouseLeftClick>(mouseData =>
            {
                LeftMouseClick(mouseData);
            }).AddToUnregisterList(this);
            this.RegisterEvent<SInputEvent_MouseRightClick>(mouseData =>
            {
                RightMouseClick(mouseData);
            }).AddToUnregisterList(this);
        }

        private void OnDestroy()
        {
            DeInit();
        }

        public void DeInit()
        {
            this.UnRegisterAll(); 
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

