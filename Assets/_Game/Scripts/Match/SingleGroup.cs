using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public bool  CheckIsFirstEmpty()
        {
            //Debug.LogError(QueueObjects[0].ObjectTypeAsset);
            return QueueObjects[0].ObjectTypeAsset == null;
        }

        public void TakeThisObject(ObjectType objectType, Transform getChild)
        {
            QueueObjects[0].ObjectTypeAsset = objectType;
            getChild.parent = QueueObjects[0].transform;
            getChild.localPosition = Vector3.zero;
        }

        public ObjectType GetFirstObjectType()
        {
            return QueueObjects[0].ObjectTypeAsset;
        }

        public void PopFirstObject()
        {
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