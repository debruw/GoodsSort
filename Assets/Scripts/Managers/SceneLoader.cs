using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameTemplate.Managers
{
    public class SceneLoader : MonoBehaviour
    {
        public const string MainMenu = "MainMenu";
        public const string Game = "Game";
        
        /// <summary>
        /// Manages a loading screen by wrapping around scene management APIs. It loads scene using the SceneManager,
        /// or handles the starting and stopping of the loading screen.
        /// </summary>
        [SerializeField] LoadingScreen m_LoadingScreen;

        public static SceneLoader Instance { get; protected set; }

        public virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            DontDestroyOnLoad(this);
        }

        public virtual void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        // SceneLoader.Instance.LoadScene("MainMenu");
        public virtual void LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            Debug.Log(sceneName);
            // Load using SceneManager
            var loadOperation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
            if (loadSceneMode == LoadSceneMode.Single)
            {
                m_LoadingScreen.StartLoadingScreen(sceneName);
            }
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            m_LoadingScreen.StopLoadingScreen();
        }
    }
}