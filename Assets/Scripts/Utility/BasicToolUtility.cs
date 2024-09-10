using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace GameFrame
{
    public class BasicToolUtility : MonoBehaviour,IUtility
    {
        public bool isShowing;
        
        // Start is called before the first frame update
        private void Start()
        {
            InitUtility();
        }

        private void OnDestroy()
        {
            DeInitUtility();
        }

        protected virtual void InitUtility()
        {
            Main.Interface.RegisterUtility(this);
        }

        private void OnGUI()
        {
            DrawGUI();
        }

        protected virtual void DrawGUI()
        {
            
        }

        public virtual void CheckButtonWillShow()
        {
            isShowing = !isShowing;
        }
        
        protected virtual void DeInitUtility()
        {
            
        }
    }
}

