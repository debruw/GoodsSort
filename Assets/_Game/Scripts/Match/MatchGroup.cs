using System;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Timer;
using Flexalon;
using GameTemplate.Managers.SceneManagers;
using UnityEngine;

namespace GameTemplate._Game.Scripts.Match
{
    public class MatchGroup : MonoBehaviour
    {
        public List<SingleGroup> SingleGroups = new List<SingleGroup>();

        public bool HasBlocker = false;
        
        public static event Action<Vector3> OnMatched;

        public void CheckMatchAndEmpty()
        {
            if (IsAllEmpty())
            {
                DestroyFirstRow();
            }
            else
            {
                bool isMatched = true;
                ObjectType currentType = SingleGroups[0].GetFirstObjectType();
                foreach (var singleGroup in SingleGroups)
                {
                    if (currentType != singleGroup.GetFirstObjectType())
                    {
                        isMatched = false;
                        break;
                    }
                }

                if (isMatched)
                {
                    //Listeners
                    //StarController
                    //ComboController
                    OnMatched?.Invoke(transform.position);
                    
                    PopFirstRaw();
                    GetComponentInParent<LevelPrefab>().CheckLevelOver();
                }
            }
        }

        private void DestroyFirstRow()
        {
            foreach (var singleGroup in SingleGroups)
            {
                singleGroup.DestroyFirstObject();
            }
        }

        private void PopFirstRaw()
        {
            foreach (var singleGroup in SingleGroups)
            {
                singleGroup.PopFirstObject();
            }
        }

        public bool IsAllEmpty()
        {
            bool isEmptyLine = true;
            foreach (var singleGroup in SingleGroups)
            {
                if (!singleGroup.CheckIsFirstEmpty())
                {
                    isEmptyLine = false;
                    break;
                }
            }

            //Debug.Log(gameObject + " // "+ isEmptyLine);
            return isEmptyLine;
        }

        public bool IsFirstEmpty()
        {
            return SingleGroups[0].CheckIsFirstEmpty();
        }

        public void CloseAllInteractables()
        {
            List<QueueObject> childInteractables = transform.GetComponentsInChildren<QueueObject>().ToList();
            
            foreach (var childInteractable in childInteractables)
            {
                childInteractable.SetInteractState(false);
            }
        }
        
        public void BlockerDeactivated()
        {
            HasBlocker = false;
            List<QueueObject> childInteractables = new List<QueueObject>();
            foreach (var childInteractable in childInteractables)
            {
                childInteractable.SetInteractState();
            }
        }

#if UNITY_EDITOR
        public void SpawnRow()
        {
            foreach (var singleGroup in SingleGroups)
            {
                singleGroup.AddQueue();
            }
        }
#endif
    }
}