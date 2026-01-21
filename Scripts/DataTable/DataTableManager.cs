using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Default
{
    public class DataTableManager : Singleton<DataTableManager>
    {
        private static Dictionary<Type, List<DataTable>> dataTables;

        public static IEnumerator Initialize()
        {
            dataTables = new Dictionary<Type, List<DataTable>>();
            var handle = AddressableExtension.LoadAssets<DataTable>("Main", (asset) =>
            {
                var type = asset.GetRowTypeName();
                Debug.Log(type);
                dataTables.TryAdd(Type.GetType(type), new List<DataTable>() { asset });
            });
            yield return handle.Result;
        }

        public static T FindRow<T>(string id) where T : DataTableRowBase
        {
            var typeList = GetDerivedTypes(typeof(T));
            T result = null;
            
            foreach (var type in typeList)
            {
                var dtList = dataTables[type];
                if (dtList != null)
                {
                    foreach (var dt in dtList)
                    {
                        result = dt.Find<T>(id);
                        if (result)
                        {
                            return result;
                        }
                    }
                }
            }

            return null;
        }

        private static List<Type> GetDerivedTypes(Type type)
        {
            var typeList = dataTables.Keys.ToList();
            var derivedTypeList = typeList.FindAll(type.IsAssignableFrom);
            return derivedTypeList;
        }

        public static T FindRow<T>(Predicate<T> predicate) where T : DataTableRowBase
        {
            var typeList = GetDerivedTypes(typeof(T));

            foreach (var type in typeList)
            {
                var dtList = dataTables[type];
                foreach (var dt in dtList)
                {
                    return dt.Find<T>(predicate);
                }
            }

            return null;
        }
        public static List<T> FindAllRow<T>() where T : DataTableRowBase
        {
            var typeList = GetDerivedTypes(typeof(T));

            foreach (var type in typeList)
            {
                var dtList = dataTables[type];
                foreach (var dt in dtList)
                {
                    return dt.FindAll<T>();
                }
            }

            return null;
        }
        public static List<T> FindAllRow<T>(Predicate<T> predicate) where T : DataTableRowBase
        {
            var typeList = GetDerivedTypes(typeof(T));

            foreach (var type in typeList)
            {
                var dtList = dataTables[type];
                foreach (var dt in dtList)
                {
                    return dt.FindAll<T>(predicate);
                }
            }

            return null;
        }
    }
}