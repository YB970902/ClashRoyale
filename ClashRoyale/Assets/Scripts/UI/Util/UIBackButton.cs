using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeanFramework.UI
{
    public class UIBackButton : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>()?.onClick.AddListener(UIManager.Instance.Close);
        }
    }
}