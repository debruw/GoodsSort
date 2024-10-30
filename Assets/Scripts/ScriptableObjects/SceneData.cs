using System.Collections.Generic;
using GameTemplate.Managers;
using GameTemplate.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SceneData", menuName = "Scriptable Objects/Scene Data", order = 0)]
    public class SceneData : SerializedScriptableObject
    {
        public Dictionary<SceneType, string> scenes = new Dictionary<SceneType, string>();
    }
}