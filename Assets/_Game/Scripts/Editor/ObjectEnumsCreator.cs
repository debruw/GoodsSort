using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace GameTemplate._Game.Scripts.Editor
{
    [CreateAssetMenu(fileName = "ObjectEnumsCreator", menuName = "Scriptable Objects/Object Enums Creator")]
    public class ObjectEnumsCreator : ScriptableObject
    {
        public ObjectType[] objects;

#if UNITY_EDITOR
        [Button("Apply Enums")]
        public void Generate()
        {
            string filePathAndName = "Assets/_Game/Scripts/Objects/ObjectId.cs"; //The folder Scripts/Enums/ is expected to exist

            using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
            {
                streamWriter.WriteLine("public enum ObjectID");
                streamWriter.WriteLine("{");
                for (int i = 0; i < objects.Length; i++)
                {
                    string objectName = objects[i].name.Split("_")[1];
                    streamWriter.WriteLine("\t" + objectName + ",");
                }
                streamWriter.WriteLine("}");
            }
            AssetDatabase.Refresh();
        }
#endif
    }
}