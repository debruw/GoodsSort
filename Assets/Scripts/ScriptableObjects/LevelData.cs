using GameTemplate.Managers.Scene;
using GameTemplate.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameTemplate.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/Level data", order = 0)]
    public class LevelData : ScriptableObject
    {
        public LevelTypes levelType;

        [ShowIf("levelType", LevelTypes.Prefab)]
        public GameObject[] levelPrefabs;

        [ShowIf("levelType", LevelTypes.Scene)]
        public SceneReference[] levelScenes;
    }
}