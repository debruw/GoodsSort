using System;
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

        //TODO qeueue mechanic
        public void AddQueue()
        {
            PrefabUtility.InstantiatePrefab(QueueObjectPrefab, transform);
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
            GetComponentInParent<MatchGroup>().CheckMatchAndEmpty();
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
        }
        
        public void DestroyFirstObject()
        {
            Destroy(QueueObjects[0].gameObject);
            QueueObjects.RemoveAt(0);
            if (QueueObjects.Count == 0)
            {
                QueueObjects.Add(Instantiate(QueueObjectPrefab, transform).GetComponent<QueueObject>());
            }
        }
    }
}