using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace GameTemplate._Game.Scripts.Match
{
    public class SingleGroup : MonoBehaviour
    {
        public GameObject QueueObjectPrefab;
        public List<QueueObject> QueueObjects = new List<QueueObject>();

        private void Start()
        {
            QueueObjects = GetComponentsInChildren<QueueObject>().ToList();
        }

        public bool IsFirstEmpty()
        {
            //Debug.LogError(QueueObjects[0].ObjectTypeAsset);
            return QueueObjects[0].ItemTypeAsset == null;
        }

        public void TakeThisObject(ItemType ıtemType, Transform getChild)
        {
            QueueObjects[0].ItemTypeAsset = ıtemType;
            getChild.parent = QueueObjects[0].transform;
            getChild.localPosition = Vector3.zero;
        }
        
        public ItemType GetFirstObject()
        {
            return QueueObjects[0].ItemTypeAsset;
        }

        public ItemID GetFirstObjectType()
        {
            return QueueObjects[0].ItemTypeAsset.itemID;
        }

        public void PopFirstObject()
        {
            transform.DOPunchScale(new Vector3(0, .1f, 0), .1f, 1).OnComplete(() => { });
            QueueObjects[0].Pop();
            QueueObjects.RemoveAt(0);
            if (QueueObjects.Count == 0)
            {
                QueueObjects.Add(Instantiate(QueueObjectPrefab, transform).GetComponent<QueueObject>());
            }

            //open interactable for new front object
            QueueObjects[0].SetInteractState();
        }

        public void DestroyFirstObject()
        {
            Destroy(QueueObjects[0].gameObject);
            QueueObjects.RemoveAt(0);
            if (QueueObjects.Count == 0)
            {
                QueueObjects.Add(Instantiate(QueueObjectPrefab, transform).GetComponent<QueueObject>());
            }

            //open interactable for new front object
            QueueObjects[0].SetInteractState();
        }

#if UNITY_EDITOR
        public void AddQueue()
        {
            PrefabUtility.InstantiatePrefab(QueueObjectPrefab, transform);
        }
#endif
    }
}