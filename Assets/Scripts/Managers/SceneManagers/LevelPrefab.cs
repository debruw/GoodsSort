using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Game.Scripts;
using GameTemplate._Game.Scripts;
using GameTemplate._Game.Scripts.Match;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

namespace GameTemplate.Managers.SceneManagers
{
    public class LevelPrefab : MonoBehaviour, IDisposable
    {
        public float LevelTime = 60;

        private List<QueueObject> _queueObjects;

        public static event Action<bool, bool> OnGameFinished;

        public static LevelPrefab Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                throw new System.Exception("Multiple LevelPrefab!");
            }

            //DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        public void CheckLevelOver()
        {
            StartCoroutine(waitandCheck());

            IEnumerator waitandCheck()
            {
                yield return new WaitForEndOfFrame();
                List<QueueObject> queueObjects = GetComponentsInChildren<QueueObject>()
                    .Where(x => x.ObjectTypeAsset != null).ToList();

                if (queueObjects.Count == 0)
                {
                    //Game Finished Win
                    OnGameFinished?.Invoke(true, false);
                }
            }
        }

        public void CheckAllFirstFilled()
        {
            bool allFilled = true;
            List<MatchGroup> matchGroups = GetComponentsInChildren<MatchGroup>().ToList();

            foreach (var mg in matchGroups)
            {
                if (mg.HasBlocker)
                {
                    continue;
                }

                if (mg.IsFirstEmpty())
                {
                    allFilled = false;
                }
            }

            //Debug.LogError("all filled = " + allFilled);
            //all lines filled there is no move left
            if (allFilled)
            {
                //Game Finished LOSE
                OnGameFinished?.Invoke(false, true);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

#if UNITY_EDITOR
        [Header("Editor Level creation settings")]
        public GameObject BlockPrefab;

        [Space] public List<int> BlockGroupStartCounts = new List<int>();
        public List<ObjectType> LevelObjectTypes = new List<ObjectType>();
        List<ObjectType> forSpawn = new List<ObjectType>();

        [ContextMenu("Create random level")]
        private void CreateRandomLevel()
        {
            forSpawn.Clear();
            Debug.Log("Initalize Level: " + gameObject.name);
            //Create object list for spawn 
            for (int i = 0; i < LevelObjectTypes.Count; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    forSpawn.Add(LevelObjectTypes[i]);
                }
            }

            forSpawn = ShuffleListWithOrderBy(forSpawn);

            // create enough queue object for prefab
            List<MatchGroup> matchGroups = GetComponentsInChildren<MatchGroup>().ToList();

            int emptyQueueCount = matchGroups.Count;
            emptyQueueCount *= 2;
            emptyQueueCount += forSpawn.Count;
            emptyQueueCount -= (emptyQueueCount % 3);

            int totalSpawnCount = 0;
            for (int j = 0; j < 10; j++)
            {
                for (int i = 0; i < matchGroups.Count; i++)
                {
                    matchGroups[i].SpawnRow();
                    totalSpawnCount += 3;

                    Debug.Log(totalSpawnCount + " // " + emptyQueueCount);
                    if (totalSpawnCount == emptyQueueCount)
                    {
                        break;
                    }
                }

                if (totalSpawnCount == emptyQueueCount)
                {
                    break;
                }
            }

            _queueObjects = transform.GetComponentsInChildren<QueueObject>().ToList();
            _queueObjects = ShuffleListWithOrderBy(_queueObjects);

            //spawn objects
            for (int i = 0; i < forSpawn.Count; i++)
            {
                _queueObjects[i].SpawnObjectEditor(forSpawn[i]);
            }

            //Fix all interactable states
            FixAllInteractableStates();

            //create blocks
            matchGroups = ShuffleListWithOrderBy(matchGroups);
            for (var i = 0; i < BlockGroupStartCounts.Count; i++)
            {
                var blockCount = BlockGroupStartCounts[i];
                GameObject prefab =
                    PrefabUtility.InstantiatePrefab(BlockPrefab, matchGroups[i].transform) as GameObject;
                prefab.GetComponent<GroupBlocker>().Initialize(blockCount);
                matchGroups[i].HasBlocker = true;
                matchGroups[i].CloseAllInteractables();
            }
        }

        public void FixAllInteractableStates()
        {
            List<QueueObject> queueObjects = transform.GetComponentsInChildren<QueueObject>().ToList();

            foreach (var queueObject in queueObjects)
            {
                queueObject.SetInteractState();
            }
        }

        [ContextMenu("Clear level")]
        public void ClearLevel()
        {
            List<QueueObject> queueObjects = transform.GetComponentsInChildren<QueueObject>().ToList();

            foreach (var queueObject in queueObjects)
            {
                DestroyImmediate(queueObject.gameObject);
            }

            List<GroupBlocker> groupBlockers = transform.GetComponentsInChildren<GroupBlocker>().ToList();

            foreach (var groupBlocker in groupBlockers)
            {
                DestroyImmediate(groupBlocker.gameObject);
            }
        }

        private List<T> ShuffleListWithOrderBy<T>(List<T> list)
        {
            Random random = new Random();
            return list.OrderBy(x => random.Next()).ToList();
        }
#endif
    }
}