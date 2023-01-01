using System.Collections;
using System.Collections.Generic;
using BeanFramework.Define;
using BeanFramework.Asset;
using UnityEngine;

namespace BeanFramework.UI
{
    /// <summary>
    /// UI 창 띄우는 기능을 해주는 매니저
    /// 캐싱도 수행한다
    /// </summary>
    public class UIManager : SingletonBase<UIManager>
    {
        [SerializeField] Canvas cachedCanvas = null;
        [SerializeField] Canvas commonCanvas = null;
        private Dictionary<string, UIBase> dictCachedUI = new Dictionary<string, UIBase>();

        private List<UIBase> uiStack = new List<UIBase>();

        /// <summary>
        /// UI를 화면에 보여준다
        /// </summary>
        /// <param name="key">UI의 key</param>
        /// <param name="tag">UI가 그려질 캔버스의 태그</param>
        /// <param name="isClear">UI를 그리기 전에 모든 UI를 날리고 그릴지 여부</param>
        /// <param name="callback">UI가 로드되고나서 호출할 함수/param>
        public void Show(string key, ECanvasTag tag = ECanvasTag.Common, bool isClear = false, System.Action<UIBase> callback = null)
        {
            UIBase ui;
            if (dictCachedUI.ContainsKey(key))
            {
                ui = dictCachedUI[key];
                ui.gameObject.SetActive(true);
                OnUILoad(ui, tag, isClear, callback);
            }
            else
            {
                AssetManager.Instance.LoadAsset<UIBase>(key, prefab =>
                {
                    ui = Instantiate(prefab).GetComponent<UIBase>();
                    ui.Init(key);
                    OnUILoad(ui, tag, isClear, callback);
                });
            }
        }

        /// <summary>
        /// UI의 생성이 끝나면 호출되는 함수
        /// Addressable로 로드할 경우 비동기로 로드되기 때문에 따로 함수로 분리함
        /// </summary>
        /// <param name="key">UI의 key</param>
        /// <param name="tag">UI가 그려질 캔버스의 태그</param>
        /// <param name="isClear">UI를 그리기 전에 모든 UI를 날리고 그릴지 여부</param>
        /// <param name="callback">UI가 로드되고나서 호출할 함수/param>
        private void OnUILoad(UIBase ui, ECanvasTag tag = ECanvasTag.Common, bool isClear = false, System.Action<UIBase> callback = null)
        {
            switch (tag)
            {
                case ECanvasTag.Cached:
                    ui.transform.SetParent(cachedCanvas.transform);
                    break;
                case ECanvasTag.Common:
                    ui.transform.SetParent(commonCanvas.transform);
                    break;
            }

            if (isClear)
            {
                int count = uiStack.Count;
                for (int i = 0; i < count; ++i)
                {
                    Close();
                }
            }
            else
            {
                switch (ui.UIType)
                {
                    case EUIType.Window:
                        for (int i = uiStack.Count - 1; i >= 0; --i)
                        {
                            uiStack[i].gameObject.SetActive(false);
                            if (uiStack[i].UIType == EUIType.Window)
                            {
                                break;
                            }
                        }
                        break;
                }
            }

            uiStack.Add(ui);

            ui.Show();

            callback?.Invoke(ui);
        }


        /// <summary>
        /// 가장 상단에 보여지고 있는 UI를 닫음
        /// </summary>
        public void Close()
        {
            UIBase ui = uiStack[uiStack.Count - 1];
            uiStack.RemoveAt(uiStack.Count - 1);

            ui.gameObject.SetActive(false);
            ui.transform.SetParent(cachedCanvas.transform);
            ui.Close();
            dictCachedUI[ui.UIKey] = ui;

            // 뒤에 배경이 다시 보여져야 함
            if (ui.UIType == EUIType.Window)
            {
                for (int i = uiStack.Count - 1; i >= 0; --i)
                {
                    uiStack[i].gameObject.SetActive(true);
                    uiStack[i].Show();

                    if (uiStack[i].UIType == EUIType.Window)
                    {
                        break;
                    }
                }
            }
        }
    }
}