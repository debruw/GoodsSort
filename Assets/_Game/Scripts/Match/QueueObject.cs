using Flexalon;
using UnityEditor;
using UnityEngine;

namespace GameTemplate._Game.Scripts.Match
{
    public class QueueObject : MonoBehaviour
    {
        public ObjectType _objectType;
        public LayerMask _layerMask;

        public void SpawnObjectEditor(ObjectType _objectType)
        {
            this._objectType = _objectType;
            PrefabUtility.InstantiatePrefab(this._objectType.prefab, transform);
        }

        public void TryToDropNewPlace()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 50, Color.red, 60);

            if (Physics.Raycast(ray, out hit, 50, _layerMask))
            {
                Transform objectHit = hit.transform;
                Debug.Log(objectHit);
                if (objectHit.TryGetComponent(out FlexalonDragTarget target))
                {
                    SingleGroup group = target.GetComponent<SingleGroup>();
                    if (group.CheckIsFirstEmpty())
                    {
                        //move this settings to new queue object
                        group.TakeThisObject(_objectType, transform.GetChild(0));
                        _objectType = null;
                    }
                }
            }
        }
    }
}