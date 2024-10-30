using System;
using System.Collections.Generic;
using System.Linq;
using GameTemplate.Managers.Scene;
using UnityEngine;

namespace GameTemplate._Game.Scripts.Match
{
    public class MatchGroup : MonoBehaviour
    {
        #region Variables

        public List<SingleGroup> SingleGroups = new List<SingleGroup>();

        public bool HasBlocker = false;

        public static event Action<Vector3> OnMatched;

        #endregion

        public void CheckMatchAndEmpty()
        {
            if (IsAllEmpty())
            {
                DestroyFirstRow();
            }
            else
            {
                bool isMatched = true;
                ItemType currentType = SingleGroups[0].GetFirstObject();
                if (currentType == null)
                {
                    isMatched = false;
                }
                else
                {
                    foreach (var singleGroup in SingleGroups)
                    {
                        if (currentType.itemID != singleGroup.GetFirstObjectType())
                        {
                            isMatched = false;
                            break;
                        }
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
                if (!singleGroup.IsFirstEmpty())
                {
                    isEmptyLine = false;
                    break;
                }
            }

            //Debug.Log(gameObject + " // "+ isEmptyLine);
            return isEmptyLine;
        }

        public bool IsAllFirstFilled()
        {
            bool isAllFilled = true;
            foreach (var singleGroup in SingleGroups)
            {
                if (singleGroup.IsFirstEmpty())
                {
                    isAllFilled = false;
                    break;
                }
            }

            return isAllFilled;
        }

        public void BlockerDeactivated()
        {
            HasBlocker = false;
            List<QueueObject> childInteractables = GetComponentsInChildren<QueueObject>().ToList();
            foreach (var childInteractable in childInteractables)
            {
                childInteractable.SetInteractState();
            }
        }

#if UNITY_EDITOR
        public void CloseAllInteractables()
        {
            List<QueueObject> childInteractables = transform.GetComponentsInChildren<QueueObject>().ToList();

            foreach (var childInteractable in childInteractables)
            {
                childInteractable.SetInteractState(false);
            }
        }
        
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