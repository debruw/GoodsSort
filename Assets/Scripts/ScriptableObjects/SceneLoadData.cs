using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameTemplate.Managers.Scene
{
    public struct SceneLoadData
    {
        public string _sceneName;
        public LoadSceneMode   _sceneMode;
        public bool   _activateLoadingCanvas;
        public bool   _setActiveScene;

        public SceneLoadData(string lastLoadedLevelScene, LoadSceneMode additive, bool activeLoadingCanvas, bool setActiveScene)
        {
            _sceneName = lastLoadedLevelScene;
            _sceneMode = additive;
            _activateLoadingCanvas = activeLoadingCanvas;
            _setActiveScene = setActiveScene;
        }
    }
}