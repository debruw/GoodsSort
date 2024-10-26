using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace GameTemplate.Managers.Pool
{
    [System.Serializable]
    public class PoolObject
    {
        [OnValueChanged("OnPrefabSet")]
        public GameObject objectPrefab;
        public string poolName;
        public int objectCount;
        public bool goBackOnDisable;

        public void OnPrefabSet()
        {
#if UNITY_EDITOR
            PoolElement poolElement;
            if(!objectPrefab.TryGetComponent<PoolElement>(out poolElement))
            {
                objectPrefab.AddComponent<PoolElement>();
                EditorUtility.SetDirty(objectPrefab);
                AssetDatabase.SaveAssets();
            }
#endif
        }
    }
}