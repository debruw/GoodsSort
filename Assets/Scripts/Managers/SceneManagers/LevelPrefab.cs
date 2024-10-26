using System;
using UnityEngine;

namespace GameTemplate.Managers.SceneManagers
{
    public class LevelPrefab : MonoBehaviour, IDisposable
    {
        //Use this method to initialize level specific elements
        public void Start()
        {
            Debug.Log("Initalize Level: " + gameObject.name);
        }
        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}