using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KCoreKit
{
     public class PrefabManager : SingletonAsset<PrefabManager>
    {
        public List<GameObject> prefabs;
        public static T Create<T>(string name = "")
        { 
            var instance = GetInstance();
            if (name.Length > 0)
            {
                var data = instance.prefabs.Find(x => x.name == name && x.GetComponent<T>() != null);
                return Instantiate(data).GetComponent<T>();
            }
            else
            {
                var data = instance.prefabs.Find(x => x.GetComponent<T>() != null);
                return Instantiate(data).GetComponent<T>();
            }
  
        }
        
        public static T CachePrefab<T>(string name = "")
        {
            var instance = GetInstance();
            if (name.Length > 0)
            {
                var data = instance.prefabs.Find(x =>  x.name == name && x.GetComponent<T>() != null);
                return data.GetComponent<T>();
            }
            else
            {
                var data = instance.prefabs.Find(x => x.GetComponent<T>() != null);
                return data.GetComponent<T>();
            }
        }
        
        [MenuItem("Assets/KCoreKit/Create/PrefabManager",priority=100000000)]
        public static void Create()
        {
            TypeExtension.CreateAsset<PrefabManager>("PrefabManager");
        }
    }
}