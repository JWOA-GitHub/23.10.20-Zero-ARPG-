using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class UIManager
    {
        private static UIManager _instance;
        private Transform _uiRoot;
        // 路径配置字典
        private Dictionary<string, string> pathDict;
        // 预制件缓存字典
        private Dictionary<string, GameObject> prefabDict;
        // 已打开界面的缓存字典
        public Dictionary<string, BasePanel> panelDict;

        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UIManager();
                }
                return _instance;
            }
        }

        public Transform UIRoot
        {
            get
            {
                if (_uiRoot == null)
                {
                    _uiRoot = GameObject.Find("Canvas").transform;
                    return _uiRoot;
                };
                return _uiRoot;
            }
        }

        private UIManager()
        {
            InitDicts();
        }

        private void InitDicts()
        {
            prefabDict = new Dictionary<string, GameObject>();
            panelDict = new Dictionary<string, BasePanel>();

            pathDict = new Dictionary<string, string>()
            {
                // {UIConst.AllCardPanel, "Menu/AllCardPanel"},
                // {UIConst.MenuPanel, "Menu/MenuPanel"},
                // {UIConst.MainMenuPanel, "Menu/MainMenuPanel"},
                // {UIConst.MenuTipPanel, "Menu/MenuTipPanel"},
                // {UIConst.NewUserPanel, "Menu/NewUserPanel"},
                // {UIConst.UserPanel, "Menu/UserPanel"},
                // {UIConst.ReNameUserPanel, "Menu/ReNameUserPanel"},
            };
        }

        public BasePanel OpenPanel(string name)
        {
            BasePanel panel = null;
            // 检查是否已打开
            if (panelDict.TryGetValue(name, out panel))
            {
                Debug.LogError("界面已打开: " + name);
                return null;
            }

            // 检查路径是否配置
            string path = "";
            if (!pathDict.TryGetValue(name, out path))
            {
                Debug.LogError("界面名称错误，或未配置路径: " + name);
                return null;
            }

            // 使用缓存预制件
            GameObject panelPrefab = null;
            if (!prefabDict.TryGetValue(name, out panelPrefab))
            {
                string realPath = "Prefab/Panel/" + path;
                panelPrefab = Resources.Load<GameObject>(realPath) as GameObject;
                prefabDict.Add(name, panelPrefab);
            }

            // 打开界面
            GameObject panelObject = GameObject.Instantiate(panelPrefab, UIRoot, false);
            panel = panelObject.GetComponent<BasePanel>();
            panelDict.Add(name, panel);
            panel.OpenPanel(name);
            return panel;
        }

        public bool ClosePanel(string name)
        {
            BasePanel panel = null;
            if (!panelDict.TryGetValue(name, out panel))
            {
                Debug.LogError("界面未打开: " + name);
                return false;
            }

            panel.ClosePanel();
            // panelDict.Remove(name);
            return true;
        }

    }

    public class UIConst
    {
        public const string MainMenuPannel = "GameStartPanel";
        public const string GameOverPannel = "GameOverPanel";
    }
}
