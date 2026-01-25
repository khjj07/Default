using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Default;

namespace EFZ
{ 
    public class DataTableManager : Singleton<DataTableManager>
    {
        public List<DataTable> dataTablePool;
        private static Dictionary<Type, List<DataTable>> dataTableDictionary;

        public static IEnumerator Initialize()
        {
            dataTableDictionary = new Dictionary<Type, List<DataTable>>();
            foreach (var asset in GetInstance().dataTablePool)
            {
                var type = asset.rowTypeName;
                dataTableDictionary.TryAdd(Type.GetType(type), new List<DataTable>() { asset });
            }

            yield return null;
        }

        public static T FindRow<T>(string id) where T : DataTableRowBase
        {
            var typeList = GetDerivedTypes(typeof(T));
            T result = null;

            foreach (var type in typeList)
            {
                var dtList = dataTableDictionary[type];
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
            var typeList = dataTableDictionary.Keys.ToList();
            var derivedTypeList = typeList.FindAll(type.IsAssignableFrom);
            return derivedTypeList;
        }

        public static T FindRow<T>(Predicate<T> predicate) where T : DataTableRowBase
        {
            var typeList = GetDerivedTypes(typeof(T));

            foreach (var type in typeList)
            {
                var dtList = dataTableDictionary[type];
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
                var dtList = dataTableDictionary[type];
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
                var dtList = dataTableDictionary[type];
                foreach (var dt in dtList)
                {
                    return dt.FindAll<T>(predicate);
                }
            }

            return null;
        }
    }
}