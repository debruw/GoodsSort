using _Game.Scripts.Timer;
using GameTemplate.ScriptableObjects;
using GameTemplate.Utils;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Managers.Scene
{
    public enum LevelTypes
    {
        None,
        Scene,
        Prefab
    }

    public class LevelManager: IInitializable
    {
        #region Variables

        private int _levelId
        {
            get => UserPrefs.GetLevelId();
            set => UserPrefs.SetLevelId(value);
        }

        #endregion

        [Inject] SceneLoader _SceneLoader;
        [Inject] LevelDataHolder _levelDataHolder;

        [Inject]
        public void Construct(SceneLoader sceneLoader, LevelDataHolder levelDataHolder)
        {
            Debug.Log("LevelManager constructed");
            _SceneLoader = sceneLoader;
            _levelDataHolder = levelDataHolder;
        }

        public int LevelId
        {
            get => _levelId;
        }

        public int UILevelId
        {
            get => _levelId + 1;
        }

        public LevelTypes LevelType
        {
            get => _levelDataHolder.levelType;
        }

        private string lastLoadedLevelScene = "";
        private GameObject lastLoadedLevelPrefab;
        
        public void Initialize()
        {
            
        }
        
        public void SpawnLevel(Transform levelPrefabParent)
        {
            LoadLevel(levelPrefabParent);
        }

        public void LoadLevel(Transform levelPrefabParent)
        {
            int currentId = _levelId % _levelDataHolder.levels.Length;
            LevelData currentData = _levelDataHolder.levels[currentId];
            
            if (_levelDataHolder.levelType == LevelTypes.Scene)
            {
                if (lastLoadedLevelScene != "")
                {
                    SceneManager.UnloadSceneAsync(lastLoadedLevelScene);
                }
                lastLoadedLevelScene = currentData.levelScene;
                //load scene additive
                _SceneLoader.LoadScene(new SceneLoadData(
                    lastLoadedLevelScene, LoadSceneMode.Additive, true, true));
            }
            else
            {
                lastLoadedLevelPrefab = currentData.levelPrefab;
                //instantiate scene prefab
                lastLoadedLevelPrefab = Object.Instantiate(lastLoadedLevelPrefab, levelPrefabParent);
                TimerController.OnSetTimer.Invoke(currentData.levelTimer);
            }
        }

        public void SetNextLevel()
        {
            _levelId++;
            UserPrefs.SetLevelId(_levelId);
        }

        public void SetPreviousLevel()
        {
            _levelId--;
            UserPrefs.SetLevelId(_levelId);
        }
    }
}