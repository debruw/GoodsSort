using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace GameTemplate.Managers.Pool
{
    public class PoolElement : MonoBehaviour
    {
        private PoolID poolId;

        private bool goBackOnDisable;
        private bool isApplicationQuitting;

        public PoolID PoolId { get => poolId; set => poolId = value; }

        public void Initialize(bool gBackOnDisable, PoolID poolId)
        {
            this.goBackOnDisable = gBackOnDisable;
            this.poolId = poolId;
        }

        private void OnDisable()
        {
            if(!isApplicationQuitting && goBackOnDisable)
            {
                GoBackToPool();
            }
        }

        private void OnApplicationQuit()
        {
            isApplicationQuitting = true;
        }

        [Button("GoBackToPool")]
        public void Deactivator()
        {
            gameObject.SetActive(false);
        }

        public void GoBackToPool()
        {
            PoolingManager.Instance.GoBackToPool(this);
        }

        private void OnTransformParentChanged()
        {
            if(transform.parent != PoolingManager.Instance.poolParent)
            {
                PoolingManager.Instance.PoolElementParentChanged(this);
            }
        }
    }
}