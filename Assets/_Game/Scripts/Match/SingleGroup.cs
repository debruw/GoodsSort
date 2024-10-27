using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameTemplate._Game.Scripts.Match
{
    public class SingleGroup : MonoBehaviour
    {
        public GameObject QueueObjectPrefab;
        public List<QueueObject> QueueObjects = new List<QueueObject>();
        
        //TODO qeueue mechanic
        public void AddQueue()
        {
            PrefabUtility.InstantiatePrefab(QueueObjectPrefab, transform);
        }

        public void ClearQueue()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        public bool  CheckIsFirstEmpty()
        {
            return QueueObjects[0]._objectType == null;
        }

        public void TakeThisObject(ObjectType objectType, Transform getChild)
        {
            QueueObjects[0]._objectType = objectType;
            getChild.parent = QueueObjects[0].transform;
            getChild.localPosition = Vector3.zero;
        }
    }
}