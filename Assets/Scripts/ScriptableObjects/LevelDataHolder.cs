using GameTemplate.Managers.Scene;
using GameTemplate.Utils;
using ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameTemplate.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/Level data holder", order = 0)]
    public class LevelDataHolder : ScriptableObject
    {
        public LevelTypes levelType;
        
        public LevelData[] levels;
    }
}