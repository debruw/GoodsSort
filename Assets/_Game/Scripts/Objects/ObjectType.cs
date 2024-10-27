using UnityEngine;

namespace GameTemplate._Game.Scripts
{
    [CreateAssetMenu(fileName = "Object", menuName = "Scriptable Objects/Object Type", order = 0)]
    public class ObjectType : ScriptableObject
    {
        public ObjectID type;
        public GameObject prefab;
    }
}