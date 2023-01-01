using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BeanFramework.Asset
{
    /// <summary>
    /// 에셋 매니저
    /// 에셋을 로드하고 캐싱하며 삭제할 수 있다.
    /// </summary>
    public class AssetManager : SingletonBase<AssetManager>
    {
        /// <summary> 캐싱된 에셋 딕셔너리 </summary>
        private Dictionary<string, AsyncOperationHandle<GameObject>> cachedAssets = new Dictionary<string, AsyncOperationHandle<GameObject>>();

        /// <summary>
        /// Addressable 에셋 로드
        /// </summary>
        /// <param name="key">로드할 에셋의 Key</param>
        /// <param name="onCompleted">에셋이 성공적으로 로드되면 호출할 함수</param>
        public void LoadAsset<T>(string key, System.Action<T> onCompleted) where T : Component
        {
            AsyncOperationHandle<GameObject> result;
            if(cachedAssets.TryGetValue(key, out result))
            {
                onCompleted.Invoke(result.Result.GetComponent<T>());
                return;
            }

            Addressables.LoadAssetAsync<GameObject>(key).Completed += handle => { OnLoadComplete<T>(handle, key, onCompleted); };
        }

        private void OnLoadComplete<T>(AsyncOperationHandle<GameObject> handle, string key, System.Action<T> onCompleted) where T : Component 
        {
            cachedAssets.Add(key, handle);
            onCompleted.Invoke(handle.Result.GetComponent<T>());
        }

        /// <summary>
        /// Addressable 에셋 삭제
        /// </summary>
        /// <param name="key">삭제할 에셋의 Key</param>
        public void DestroyAsset(string key)
        {
            AsyncOperationHandle<GameObject> result;
            if (cachedAssets.TryGetValue(key, out result))
            {
                Addressables.Release(result);
            }
        }
    }
}