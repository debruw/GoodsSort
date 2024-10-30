using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameTemplate.Managers.Scene;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Managers
{
    public enum SceneType
    {
        MainMenu,
        Game
    }
    
    public class SceneLoader
    {
        public static event Action OnBeforeSceneLoad = delegate { };
        public static event Action OnSceneLoaded = delegate { };

        private SceneData _sceneData;
        
        [Inject]
        public void Construct(SceneData sceneData)
        {
            //Debug.Log("Constructing SceneLoader");
            _sceneData = sceneData;
        }

        public void LoadSceneByType(SceneType sceneType, LoadSceneMode mode = LoadSceneMode.Single)
        {
            LoadScene(new SceneLoadData(_sceneData.scenes[sceneType], mode, true, true));
        }

        // SceneLoader.Instance.LoadScene("MainMenu");
        public async UniTask LoadScene(SceneLoadData sceneLoadData)
        {
            OnBeforeSceneLoad?.Invoke();
            //Debug.Log("OnBeforeSceneLoad");

            // Load using SceneManager
            await SceneManager.LoadSceneAsync(sceneLoadData._sceneName, sceneLoadData._sceneMode).ToUniTask();

            OnSceneLoaded?.Invoke();
            //Debug.Log("OnSceneLoaded");
        }
    }
}