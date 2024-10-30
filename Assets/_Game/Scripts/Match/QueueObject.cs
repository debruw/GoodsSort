using DG.Tweening;
using Flexalon;
using GameTemplate.Gameplay.GameState;
using GameTemplate.Managers.Scene;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameTemplate._Game.Scripts.Match
{
    public class QueueObject : MonoBehaviour
    {
        public ItemType ItemTypeAsset
        {
            get { return ıtemType; }
            set
            {
                ıtemType = value;

                SetInteractState();
            }
        }

        public LayerMask _layerMask;

        [FormerlySerializedAs("_objectType")] [SerializeField] private ItemType ıtemType;
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
                    if (singleGroup.IsFirstEmpty())
                    {
                        //move this settings to new queue object
                        singleGroup.TakeThisObject(ıtemType, transform.GetChild(0));
                        ItemTypeAsset = null;
                        singleGroup.GetComponentInParent<MatchGroup>().CheckMatchAndEmpty();
                        _matchGroup.CheckMatchAndEmpty();
                        
                        _ = GetComponentInParent<LevelPrefab>().CheckAllFirstFilled();
                    }
                }
            }
        }

        public void Pop()
        {
            PunchScaleY();
            Destroy(gameObject,.15f);
        }

        void PunchScaleY()
        {
            transform.DOPunchScale(new Vector3(0, .1f, 0), .1f, 1);
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

            _interactable.Draggable = ıtemType != null;
        }

#if UNITY_EDITOR
        public void SpawnObjectEditor(ItemType ıtemType)
        {
            _interactable = GetComponent<FlexalonInteractable>();
            ItemTypeAsset = ıtemType;
            PrefabUtility.InstantiatePrefab(this.ıtemType.prefab, transform);
        }
#endif
    }
}