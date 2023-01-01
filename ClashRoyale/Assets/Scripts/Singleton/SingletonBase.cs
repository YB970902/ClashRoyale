using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeanFramework
{
    /// <summary>
    /// 싱글턴을 사용하기 위해 상속받아야 하는 클래스
    /// DontDestroyObject가 필요없는 클래스가 있을수도 있으므로 처리하지 않는다
    /// DontDestroyObject는 GameMangerd의 Awake에서 처리한다
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonBase<T> : MonoBehaviour where T : Component
    {
        private static T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if(_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }
    }
}