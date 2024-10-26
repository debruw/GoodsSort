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

        [Inject] private PoolingManager _PoolingManager;

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
            _PoolingManager.GoBackToPool(this);
        }

        private void OnTransformParentChanged()
        {
            if(transform.parent != _PoolingManager.poolParent)
            {
                _PoolingManager.PoolElementParentChanged(this);
            }
        }
    }
}