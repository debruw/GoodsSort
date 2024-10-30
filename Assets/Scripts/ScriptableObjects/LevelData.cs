using GameTemplate.Utils;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/Level data", order = 0)]
    public class LevelData : ScriptableObject
    {
        public int levelTimer;

        public GameObject levelPrefab;
        public SceneReference levelScene;
    }
}