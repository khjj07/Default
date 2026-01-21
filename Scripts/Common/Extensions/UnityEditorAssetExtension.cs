using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;

namespace Default
{
#if UNITY_EDITOR
    public static class UnityEditorAssetExtensions
    {
        public static string GetSelectedDirectoryPath()
        {
            Object selectedObject = Selection.activeObject;

            if (selectedObject == null)
            {
                // ???? ???? ??? ??
                return null;
            }

            // 1. ??? ??? ??? ?????.
            string path = AssetDatabase.GetAssetPath(selectedObject);

            if (string.IsNullOrEmpty(path))
            {
                // ??? ??? ???? ??
                return null;
            }

            // 2. ??? ???? ???? ?????.
            if (AssetDatabase.IsValidFolder(path))
            {
                // ??? ?? ??? ??, ?? ?? ??? ??
                return path;
            }
            else
            {
                // ??? ?? ??? ??, ?? ??? ?? ?? ??? ??
                return Path.GetDirectoryName(path);
            }
        }

        public static bool IsInPrefabStage()
        {
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            return prefabStage != null;
        }

        public static string GetLocalPath(this DefaultAsset @this)
        {
            var success =
                AssetDatabase.TryGetGUIDAndLocalFileIdentifier(@this, out var guid, out long _);

            if (success)
                return AssetDatabase.GUIDToAssetPath(guid);
            return null;
        }

        public static string GetAbsolutePath(this DefaultAsset @this)
        {
            var path = GetLocalPath(@this);
            if (path == null)
                return null;

            path = path.Substring(path.IndexOf('/') + 1);
            return Application.dataPath + "/" + path;
        }

        public static DirectoryInfo GetDirectoryInfo(this DefaultAsset @this)
        {
            var absPath = GetAbsolutePath(@this);
            return absPath != null ? new DirectoryInfo(absPath) : null;
        }

        public static List<T> LoadAllObjectsInFolder<T>(this DefaultAsset @this) where T : class
        {
            var assets = new List<T>();
            // ???? ?? ??? ????? GUID ???? ??????
            var assetGUIDs = AssetDatabase.FindAssets("", new[] { GetLocalPath(@this) });

            foreach (var guid in assetGUIDs)
            {
                // GUID?? ???? ???
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                // ????? Object ??????? ???
                if (AssetDatabase.LoadAssetAtPath<Object>(assetPath) is T tmp) assets.Add(tmp);
            }

            return assets;
        }
    }
#endif
}