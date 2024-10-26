using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameTemplate.Editor
{
    public class SceneBoot : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod]
        public static void LoadScenes()
        {
            EditorGameSettings gameSettings = Resources.LoadAll<EditorGameSettings>("Managers")[0];
#if UNITY_EDITOR
            if (gameSettings.startFromGameScene)
            {
                SceneManager.LoadScene("Startup");
            }
#endif
        }
    }
}