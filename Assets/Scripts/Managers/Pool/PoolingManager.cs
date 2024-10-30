using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameTemplate.Managers.Pool
{
    public class PoolingManager : MonoBehaviour
    {
        #region Variables

        public ScriptablePooling poolingData;
        public Transform poolParent;
        public PoolID testPoolId;

        [SerializeField] private List<PoolElement> parentsChangedPoolObjects = new List<PoolElement>();
        private Dictionary<PoolID, Queue<GameObject>> objectPools = new Dictionary<PoolID, Queue<GameObject>>();
        
        public static PoolingManager Instance { get; private set; }
        

        #endregion
        private void Awake()
        {
            if (Instance != null)
            {
                throw new System.Exception("Multiple Pools!");
            }

            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        public void Initialize()
        {
            //poolParent = new GameObject("_PoolParent").transform;
            for (int i = 0; i < poolingData.poolObjects.Length; i++)
            {
                objectPools.Add((PoolID)i, new Queue<GameObject>());
                for (int z = 0; z < poolingData.poolObjects[i].objectCount; z++)
                {
                    GameObject newObject = Instantiate(poolingData.poolObjects[i].objectPrefab, poolParent);
                    newObject.SetActive(false);
                    newObject.GetComponent<PoolElement>().Initialize(poolingData.poolObjects[i].goBackOnDisable, (PoolID)i);
                    objectPools[(PoolID)i].Enqueue(newObject);
                }
            }
        }

        public void ResetPool()
        {
            for (int i = 0; i < parentsChangedPoolObjects.Count; i++)
            {
                parentsChangedPoolObjects[i].transform.SetParent(poolParent);
            }
            parentsChangedPoolObjects.Clear();
            PoolElement[] children = poolParent.GetComponentsInChildren<PoolElement>();
            for (int i = 0; i < children.Length; i++)
            {
                children[i].gameObject.SetActive(false);
            }
        }

        public void OnPoolElementDestroyed(PoolElement destroyedPoolElement)
        {
            Debug.Log("element Destroyed : " + destroyedPoolElement);
        }

        public void GoBackToPool(GameObject poolObject)
        {
            poolObject.transform.SetParent(poolParent);
            objectPools[poolObject.GetComponent<PoolElement>().PoolId].Enqueue(poolObject);
        }

        public GameObject GetParticleById(PoolID poolId, Transform referance)
        {
            return GetParticleById(poolId, referance.position, Vector3.one);
        }

        public GameObject GetParticleById(PoolID poolId, Transform referance, Vector3 targetScale)
        {
            return GetParticleById(poolId, referance.position, targetScale);
        }
        public GameObject GetParticleById(PoolID poolId, Vector3 position, Vector3 targetScale, Transform parentInfo = null)
        {
            GameObject particle = GetGameObjectById(poolId, position, Quaternion.identity);
            particle.transform.localScale = targetScale;
            if (parentInfo != null)
            {
                particle.transform.SetParent(parentInfo.parent);
                parentInfo.transform.localPosition = parentInfo.localPosition;
            }
            particle.GetComponent<ParticleSystem>().Play();
            return particle;
        }

        public GameObject GetGameObjectById(PoolID poolId)
        {
            return GetGameObjectById(poolId, Vector3.zero, Quaternion.identity);
        }

        public GameObject GetGameObjectById(PoolID poolId, Transform objectTransform)
        {
            return GetGameObjectById(poolId, objectTransform.position, objectTransform.rotation);
        }

        public GameObject GetGameObjectById(PoolID poolId, Vector3 position)
        {
            return GetGameObjectById(poolId, position, Quaternion.identity);
        }

        public GameObject GetGameObjectById(PoolID poolId, Vector3 position, Quaternion rotation)
        {
            if (!objectPools.ContainsKey(poolId))
            {
                objectPools.Add(poolId, new Queue<GameObject>());
            }
            if (objectPools[poolId].Count != 0)
            {
                GameObject poolObject = objectPools[poolId].Dequeue();
                poolObject.transform.position = position;
                poolObject.transform.rotation = rotation;
                poolObject.SetActive(true);
                return poolObject;
            }
            PoolObject selectedPoolObject = poolingData.poolObjects.Where(x => x.poolName.Equals(poolId.ToString())).First();

            if (selectedPoolObject != null)
            {
                GameObject poolObject = Instantiate(selectedPoolObject.objectPrefab, position, rotation);
                poolObject.transform.SetParent(poolParent);
                poolObject.GetComponent<PoolElement>().Initialize(selectedPoolObject.goBackOnDisable, poolId);
                poolObject.SetActive(true);
                return poolObject;
            }
            return null;
        }

        public GameObject GetGameObjectById(PoolID poolId, Vector3 position, Quaternion rotation, Vector3 targetScale)
        {
            if (!objectPools.ContainsKey(poolId))
            {
                objectPools.Add(poolId, new Queue<GameObject>());
            }
            if (objectPools[poolId].Count != 0)
            {
                GameObject poolObject = objectPools[poolId].Dequeue();
                poolObject.transform.position = position;
                poolObject.transform.rotation = rotation;
                poolObject.transform.localScale = targetScale;
                poolObject.SetActive(true);
                return poolObject;
            }
            PoolObject selectedPoolObject = poolingData.poolObjects.Where(x => x.poolName.Equals(poolId.ToString())).First();

            if (selectedPoolObject != null)
            {
                GameObject poolObject = Instantiate(selectedPoolObject.objectPrefab, position, rotation);
                poolObject.transform.SetParent(poolParent);
                poolObject.GetComponent<PoolElement>().Initialize(selectedPoolObject.goBackOnDisable, poolId);
                poolObject.transform.localScale = targetScale;
                poolObject.SetActive(true);
                return poolObject;
            }
            return null;
        }

        public void GoBackToPool(PoolElement elementToGoBackToPool)
        {
            objectPools[elementToGoBackToPool.PoolId].Enqueue(elementToGoBackToPool.gameObject);
        }

        public void GoBackToPool(PoolID poolId, GameObject objectToAddPool)
        {
            objectToAddPool.SetActive(false);
            objectPools[poolId].Enqueue(objectToAddPool);
        }

        public void PoolElementParentChanged(PoolElement parentChangedObject)
        {
            parentsChangedPoolObjects.Add(parentChangedObject);
        }

        [Button("TestGetObject")]
        public void GetTestGameObject()
        {
            GetGameObjectById(testPoolId);
        }
    }
}