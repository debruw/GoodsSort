using System;
using Flexalon;
using UnityEditor;
using UnityEngine;

namespace GameTemplate._Game.Scripts.Match
{
    public class QueueObject : MonoBehaviour
    {
        public ObjectType ObjectTypeAsset
        {
            get { return _objectType; }
            set
            {
                _objectType = value;

                _interactable.enabled = _objectType != null;
            }
        }

        public LayerMask _layerMask;

        [SerializeField] private ObjectType _objectType;
        private MatchGroup _matchGroup;
        private FlexalonInteractable _interactable;

        private void Start()
        {
            _matchGroup = GetComponentInParent<MatchGroup>();
            _interactable = GetComponent<FlexalonInteractable>();
        }

        public void SpawnObjectEditor(ObjectType _objectType)
        {
            this._objectType = _objectType;
            PrefabUtility.InstantiatePrefab(this._objectType.prefab, transform);
        }

        public void TryToDropNewPlace()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(ray.origin, ray.direction * 50, Color.red, 60);

            if (Physics.Raycast(ray, out hit, 50, _layerMask))
            {
                Transform objectHit = hit.transform;
                if (objectHit.TryGetComponent(out FlexalonDragTarget target))
                {
                    SingleGroup group = target.GetComponent<SingleGroup>();
                    if (group.CheckIsFirstEmpty())
                    {
                        //move this settings to new queue object
                        group.TakeThisObject(_objectType, transform.GetChild(0));
                        _objectType = null;
                        _matchGroup.CheckMatchAndEmpty();
                    }
                }
            }
        }

        public void Pop()
        {
            //TODO pop effect
            Destroy(gameObject);
        }
    }
}