using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JWOAGameSystem
{
    public class MainMenuPannel : BasePanel
    {
        public Button btn;

        protected void Awake()
        {
            btn.onClick.AddListener(OnBtn);
        }

        private void OnBtn()
        {
            //TODO: 打开界面
            Debug.Log("             OnBtn");
            OpenPanel(UIConst.MainMenuPannel);
        }
    }
}
