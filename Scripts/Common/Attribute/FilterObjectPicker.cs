using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Default
{
    // System.Type 사용을 위해 추가

    public class FilterObjectPickerAttribute : PropertyAttribute
    {
        public string filter;
        public bool allowSceneObjects;

        public FilterObjectPickerAttribute(string filter, bool allowSceneObjects = false)
        {
            this.filter = filter;
            this.allowSceneObjects = allowSceneObjects;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(FilterObjectPickerAttribute))]
    public class FilterObjectPickerPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 1. 프로퍼티 범위 시작
            EditorGUI.BeginProperty(position, label, property);
 
            int controlID = GUIUtility.GetControlID(FocusType.Passive);
            Event e = Event.current;
    
            // ⭐️ 이벤트 처리 여부를 추적하는 플래그
            bool eventWasConsumed = false; 

            // --- 커스텀 Object Picker 로직 (마우스 클릭) ---
            if (e.type == EventType.MouseUp && position.Contains(e.mousePosition))
            {
                // 마우스 클릭은 항상 GUI.changed를 유발하므로, 여기서 바로 e.Use()를 호출하여 
                // 하위 ObjectField가 이 이벤트를 잡지 못하도록 막고, 커스텀 Picker를 엽니다.
                e.Use(); 
                eventWasConsumed = true; 
        
                System.Type type = fieldInfo.FieldType;
        
                // 배열/리스트 타입 처리
                if (type.IsArray)
                {
                    type = type.GetElementType();
                }
                else if (type.IsGenericType && type.GenericTypeArguments.Length > 0)
                {
                    type = type.GenericTypeArguments[0];
                }

                // Object Picker 호출을 위한 타입 결정 (이전 로직과 동일)
                if (type == typeof(GameObject))
                {
                    ShowObjectPicker<GameObject>(property.objectReferenceValue, controlID);
                }
                else if (type == typeof(Mesh))
                {
                    ShowObjectPicker<Mesh>(property.objectReferenceValue, controlID);
                }
                // ... (나머지 타입 처리) ...
                else if (type == typeof(Material))
                {
                    ShowObjectPicker<Material>(property.objectReferenceValue, controlID);
                }
                else if (type == typeof(Texture2D))
                {
                    ShowObjectPicker<Texture2D>(property.objectReferenceValue, controlID);
                }
                else if (type == typeof(Sprite))
                {
                    ShowObjectPicker<Sprite>(property.objectReferenceValue, controlID);
                }
                else if (type.BaseType == typeof(ScriptableObject))
                {
                    ShowObjectPicker<ScriptableObject>(property.objectReferenceValue, controlID);
                }
                else
                {
                    ShowObjectPicker<Object>(property.objectReferenceValue, controlID);
                }
            }
            // --- Object Picker 결과 처리 ---
            else if (e.type == EventType.ExecuteCommand && e.commandName == "ObjectSelectorUpdated" &&
                     controlID == EditorGUIUtility.GetObjectPickerControlID())
            {
                e.Use();
                eventWasConsumed = true; 
                property.objectReferenceValue = EditorGUIUtility.GetObjectPickerObject();
            }
    
            // ⭐️ 최종 수정: 이벤트가 처리되었든 안 되었든, 항상 ObjectField를 사용하여 드로잉을 단순화합니다.
            // ObjectField는 SerializedProperty의 값을 직접적으로 조작하지 않기 때문에 
            // 반환 값을 다시 property에 할당해야 합니다.
    
            var att = (FilterObjectPickerAttribute)attribute;
    
            // ObjectField 드로잉
            Object newObjectValue = EditorGUI.ObjectField(
                position, 
                label, 
                property.objectReferenceValue, 
                fieldInfo.FieldType, 
                att.allowSceneObjects
            );

            // ObjectField에서 변경 사항이 발생하면 SerializedProperty에 반영합니다.
            if (newObjectValue != property.objectReferenceValue)
            {
                property.objectReferenceValue = newObjectValue;
            }
    
            // 2. 프로퍼티 범위 끝
            EditorGUI.EndProperty();
        }

        public void ShowObjectPicker<T>(Object obj, int controlID) where T : Object
        {
            var att = (FilterObjectPickerAttribute)attribute;
            EditorGUIUtility.ShowObjectPicker<T>(obj, att.allowSceneObjects, att.filter, controlID);
        }
    }
#endif
}