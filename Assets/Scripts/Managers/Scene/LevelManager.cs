using _Game.Scripts.Timer;
using GameTemplate.ScriptableObjects;
using GameTemplate.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace GameTemplate.Managers.Scene
{
    public enum LevelTypes
    {
        None,
        Scene,
        Prefab
    }

    [CreateAssetMenu(fileName = "LevelManager", menuName = "Scriptable Objects/Level Manager")]
    public class LevelManager : ScriptableObject
    {
        [SerializeField] private int loopStartIndex;
        private int levelId = 0;

        [Inject] SceneLoader _SceneLoader;
        [Inject] LevelData _LevelData;

        public int LevelId
        {
            get => levelId;
        }

        public int UILevelId
        {
            get => levelId + 1;
        }

        public LevelTypes LevelType
        {
            get => _LevelData.levelType;
        }

        private string lastLoadedLevelScene = "";
        private GameObject lastLoadedLevelPrefab;

        public void Initialize(Transform levelPrefabParent)
        {
            this.levelId = UserPrefs.GetLevelId();

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

                lastLoadedLevelScene = _LevelData.levelScenes[levelId % _LevelData.levelScenes.Length].SceneName;
                //load scene additive
                _SceneLoader.LoadScene(new SceneLoadData(
                    lastLoadedLevelScene, LoadSceneMode.Additive, true, true));
            }
            else
            {
                lastLoadedLevelPrefab = _LevelData.levelPrefabs[levelId % _LevelData.levelPrefabs.Length];
                //instantiate scene prefab
                lastLoadedLevelPrefab = Instantiate(lastLoadedLevelPrefab, levelPrefabParent);
                TimerController.OnSetTimer.Invoke(lastLoadedLevelPrefab.GetComponent<LevelPrefab>().LevelTime);
            }
        }

        public void SetNextLevel()
        {
            levelId++;
            UserPrefs.SetLevelId(levelId);
        }

        public void SetPreviousLevel()
        {
            levelId--;
            UserPrefs.SetLevelId(levelId);
        }
    }
}