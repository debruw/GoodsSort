using System;
using _Game.Scripts.Timer;
using Flexalon;
using GameTemplate.Gameplay.GameState;
using GameTemplate.Managers.SceneManagers;
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

                SetInteractState();
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

            if (transform.position.z > 0)
            {
                SetInteractState(false);
            }

            _interactable.DragStart.AddListener(DragStarted);
        }

        void DragStarted(FlexalonInteractable arg0)
        {
            GameSceneState.OnFirstTouch?.Invoke();
            _interactable.DragStart.RemoveListener(DragStarted);
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
                    SingleGroup singleGroup = target.GetComponent<SingleGroup>();
                    if (singleGroup.CheckIsFirstEmpty())
                    {
                        //move this settings to new queue object
                        singleGroup.TakeThisObject(_objectType, transform.GetChild(0));
                        ObjectTypeAsset = null;
                        singleGroup.GetComponentInParent<MatchGroup>().CheckMatchAndEmpty();
                        _matchGroup.CheckMatchAndEmpty();
                        GetComponentInParent<LevelPrefab>().CheckAllFirstFilled();
                    }
                }
            }
        }

        public void Pop()
        {
            //TODO pop effect
            Destroy(gameObject);
        }

        public void SetInteractState(bool state = true)
        {
            if (!state)
            {
                _interactable.Draggable = state;
                return;
            }
            
            if (_interactable == null)
            {
                _interactable = GetComponent<FlexalonInteractable>();
            }

            _interactable.Draggable = _objectType != null;
        }

#if UNITY_EDITOR
        public void SpawnObjectEditor(ObjectType _objectType)
        {
            _interactable = GetComponent<FlexalonInteractable>();
            ObjectTypeAsset = _objectType;
            PrefabUtility.InstantiatePrefab(this._objectType.prefab, transform);
        }
#endif
    }
}