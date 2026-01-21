using UnityEditor;
using UnityEngine;

namespace Default.Asset
{
    public class ModelImportConfiguration : AssetPostprocessor
    {
        // 모델 임포트가 완료되기 직전에 호출됨

        void OnPreprocessModel()
        {
            // 1. 설정 인스턴스 로드 (없으면 자동 생성)
            ModelImportSettings settings = ModelImportSettings.GetInstance();
            
            // 2. ModelImporter 인스턴스 캐스팅
            ModelImporter modelImporter = (ModelImporter)assetImporter;

            if (settings == null)
            {
                UnityEngine.Debug.LogError("ModelImportSettings 로드 실패. 기본 설정을 사용합니다.");
                return;
            }

            // 3. 설정에 따라 modelImporter 속성 수정
        
            // Material Creation Mode 설정 적용
            modelImporter.materialImportMode = settings.materialMode;
            
        }
    }
    public class ModelImportConfigurationPanel : EditorWindow
    {
        private ModelImportSettings settings;
        private SerializedObject serializedSettings;

        [MenuItem("Tools/Default/Model Import Config Panel")]
        public static void ShowWindow()
        {
            // 창을 열거나 포커스합니다.
            GetWindow<ModelImportConfigurationPanel>("Model Import Config");
        }

        private void OnEnable()
        {
            // 설정 인스턴스를 가져와 SerializedObject로 래핑하여 undo/redo 및 dirty 관리를 쉽게 합니다.
            //settings = ModelImportSettings.Instance;
            serializedSettings = new SerializedObject(settings);
        }

        private void OnGUI()
        {
            if (serializedSettings == null) return;
            
            serializedSettings.Update(); // 필드 값 최신화

            EditorGUILayout.LabelField("Global Model Import Configuration", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // 1. 머티리얼 임포트 모드 설정
            SerializedProperty materialModeProp = serializedSettings.FindProperty(nameof(ModelImportSettings.materialMode));
            EditorGUILayout.PropertyField(materialModeProp, new GUIContent("Material Import Mode", "모델 임포트 시 머티리얼을 생성하지 않도록 설정합니다."));
            
            EditorGUILayout.Space();
            
            // 변경 사항 적용
            if (serializedSettings.ApplyModifiedProperties())
            {
                // SerializedObject를 통해 변경했으므로, 에셋을 저장해야 변경 내용이 영구적으로 남습니다.
                EditorUtility.SetDirty(settings);
                AssetDatabase.SaveAssets();
            }

            if (GUILayout.Button("Force Reimport All Models (Apply Config)"))
            {
                // 설정을 강제로 모든 모델에 적용하려면 전체 재임포트를 실행해야 합니다.
                AssetDatabase.ImportAsset("Assets", ImportAssetOptions.ForceUpdate | ImportAssetOptions.ImportRecursive);
                UnityEngine.Debug.Log("모든 모델에 새로운 임포트 설정을 적용하기 위해 전체 재임포트를 시작했습니다.");
            }
        }
    }
}