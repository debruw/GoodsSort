using GameTemplate.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace GameTemplate.Managers.SceneManagers
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
        public LevelTypes levelType;

        [ShowIf("levelType", LevelTypes.Prefab)]
        public GameObject[] levelPrefabs;

        [ShowIf("levelType", LevelTypes.Scene)]
        public SceneReference[] levelScenes;

        [SerializeField] private int loopStartIndex;
        private int levelId = 0;

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
            get => levelType;
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
            if (levelType == LevelTypes.Scene)
            {
                if (lastLoadedLevelScene != "")
                {
                    SceneManager.UnloadSceneAsync(lastLoadedLevelScene);
                }

                lastLoadedLevelScene = levelScenes[levelId % levelScenes.Length].SceneName;
                //load scene additive
                SceneLoader.Instance.LoadScene(lastLoadedLevelScene, LoadSceneMode.Additive);
            }
            else
            {

                lastLoadedLevelPrefab = levelPrefabs[levelId % levelPrefabs.Length];
                //instantiate scene prefab
                Instantiate(lastLoadedLevelPrefab, levelPrefabParent);
            }
        }

        public void SetNextLevel()
        {
            levelId++;
            UserPrefs.SetLevelId(levelId);
        }
    }
}