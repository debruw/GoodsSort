using System;
using System.Collections.Generic;
using System.Linq;
using GameTemplate._Game.Scripts;
using GameTemplate._Game.Scripts.Match;
using UnityEngine;
using Random = System.Random;

namespace GameTemplate.Managers.SceneManagers
{
    public class LevelPrefab : MonoBehaviour, IDisposable
    {
        public List<ObjectType> LevelObjectTypes = new List<ObjectType>();
        public List<ObjectType> forSpawn = new List<ObjectType>();

        public int counter = -1;
        
        private List<QueueObject> _queueObjects;
        
        public static LevelPrefab Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                throw new System.Exception("Multiple Sound Players!");
            }

            //DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        [ContextMenu("Create random level")]
        public void CreateRandomLevel()
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
            MatchGroup[] matchGroups = GetComponentsInChildren<MatchGroup>();
            
            int emptyQueueCount = matchGroups.Length;
            emptyQueueCount *= 2;
            emptyQueueCount += forSpawn.Count;
            emptyQueueCount -= (emptyQueueCount % 3);
            
            int totalSpawnCount = 0;
            for (int j = 0; j < 10; j++)
            {
                for (int i = 0; i < matchGroups.Length; i++)
                {
                    matchGroups[i].SpawnRow();
                    totalSpawnCount += 3;
                    
                    Debug.Log(totalSpawnCount +" // "+ emptyQueueCount);
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
        }

        //Use this method to initialize level specific elements
        public void Start()
        {
            
        }

        public ObjectType GetObjectForSpawn()
        {
            if (counter == forSpawn.Count - 1)
            {
                return null;
            }
            else
            {
                counter++;
                return forSpawn[counter];
            }
        }

        public List<T> ShuffleListWithOrderBy<T>(List<T> list)
        {
            Random random = new Random();
            return list.OrderBy(x => random.Next()).ToList();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}