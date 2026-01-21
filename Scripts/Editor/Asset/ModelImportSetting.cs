using UnityEditor;

namespace Default.Asset
{
    public class ModelImportSettings : SingletonAsset<ModelImportSettings>
    {
        public ModelImporterMaterialImportMode materialMode = ModelImporterMaterialImportMode.None;
#if UNITY_EDITOR
        [MenuItem("Assets/Default/Create/ModelImportSettings")]
        public static void CreateDataTablet()
        {
            TypeExtension.CreateAsset<ModelImportSettings>("ModelImportSettings");
        }
#endif
    }
}