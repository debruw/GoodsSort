using _Game.Scripts.Timer;
using GameTemplate.ScriptableObjects;
using GameTemplate.Utils;
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
        private int _levelId = 0;

        [Inject] SceneLoader _SceneLoader;
        [Inject] LevelData _LevelData;

        [Inject]
        public void Construct(SceneLoader sceneLoader, LevelData levelData)
        {
            Debug.Log("LevelManager constructed");
            _SceneLoader = sceneLoader;
            _LevelData = levelData;
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
            get => _LevelData.levelType;
        }

        private string lastLoadedLevelScene = "";
        private GameObject lastLoadedLevelPrefab;
        
        public void Initialize()
        {
            _levelId = UserPrefs.GetLevelId();
        }
        
        public void SpawnLevel(Transform levelPrefabParent)
        {
            LoadLevel(levelPrefabParent);
        }

        public void LoadLevel(Transform levelPrefabParent)
        {
            if (_LevelData.levelType == LevelTypes.Scene)
            {
                if (lastLoadedLevelScene != "")
                {
                    SceneManager.UnloadSceneAsync(lastLoadedLevelScene);
                }

                lastLoadedLevelScene = _LevelData.levelScenes[_levelId % _LevelData.levelScenes.Length].SceneName;
                //load scene additive
                _SceneLoader.LoadScene(new SceneLoadData(
                    lastLoadedLevelScene, LoadSceneMode.Additive, true, true));
            }
            else
            {
                lastLoadedLevelPrefab = _LevelData.levelPrefabs[_levelId % _LevelData.levelPrefabs.Length];
                //instantiate scene prefab
                lastLoadedLevelPrefab = Object.Instantiate(lastLoadedLevelPrefab, levelPrefabParent);
                TimerController.OnSetTimer.Invoke(lastLoadedLevelPrefab.GetComponent<LevelPrefab>().LevelTime);
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