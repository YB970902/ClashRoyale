using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeanFramework.Define;

namespace BeanFramework.UI
{
    /// <summary>
    /// UI의 기본이 되는 베이스.
    /// </summary>
    public class UIBase : MonoBehaviour
    {
        [SerializeField] EUIType type;
        public EUIType UIType => type;

        public string UIKey { get; private set; }

        /// <summary>
        /// UI의 초기 세팅
        /// </summary>
        public void Init(string key)
        {
            UIKey = key;
        }

        /// <summary>
        /// UI가 열리면 해야하는 행동
        /// </summary>
        protected virtual void OnShow()
        {

        }

        public void Show()
        {
            OnShow();
        }

        /// <summary>
        /// UI가 닫히면 해야하는 행동
        /// </summary>
        protected virtual void OnClose()
        {

        }

        public void Close()
        {
            OnClose();
        }
    }
}