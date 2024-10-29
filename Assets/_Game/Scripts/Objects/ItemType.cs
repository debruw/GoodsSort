using UnityEngine;
using UnityEngine.Serialization;

namespace GameTemplate._Game.Scripts
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item Type", order = 0)]
    public class ItemType : ScriptableObject
    {
        [FormerlySerializedAs("type")] public ItemID itemID;
        public GameObject prefab;
    }
}