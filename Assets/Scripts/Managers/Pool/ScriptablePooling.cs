using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace GameTemplate.Managers.Pool
{
    [CreateAssetMenu(fileName = "PoolingManager", menuName = "Scriptable Objects/Pooling")]
    public class ScriptablePooling : ScriptableObject
    {
        public PoolObject[] poolObjects;

#if UNITY_EDITOR
        [Button("Apply Pool")]
        public void Generate()
        {
            string filePathAndName = "Assets/Scripts/Managers/Pool/PoolId.cs"; //The folder Scripts/Enums/ is expected to exist

            using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
            {
                streamWriter.WriteLine("public enum PoolID");
                streamWriter.WriteLine("{");
                for (int i = 0; i < poolObjects.Length; i++)
                {
                    streamWriter.WriteLine("\t" + poolObjects[i].poolName + ",");
                }
                streamWriter.WriteLine("}");
            }
            AssetDatabase.Refresh();
        }
#endif
    }
}