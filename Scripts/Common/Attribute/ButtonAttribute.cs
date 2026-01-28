using UnityEngine;
using System;
using System.Reflection;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(MonoBehaviour), true)] // 모든 MonoBehaviour를 상속받는 클래스에 적용
[CanEditMultipleObjects]
public class ButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI(); // 기존 변수들 표시

        // 해당 객체의 모든 메서드를 뒤져서 [Button] 어트리뷰트가 있는지 찾습니다.
        var methods = target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var method in methods)
        {
            var ba = (ButtonAttribute)Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));

            if (ba != null)
            {
                // 버튼 라벨 결정 (설정한 이름 혹은 메서드 이름)
                string label = string.IsNullOrEmpty(ba.ButtonLabel) ? method.Name : ba.ButtonLabel;

                if (GUILayout.Button(label))
                {
                    // 메서드 실행
                    method.Invoke(target, null);
                }
            }
        }
    }
}

#endif

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
public class ButtonAttribute : PropertyAttribute
{
    public string ButtonLabel { get; }

    public ButtonAttribute(string label = null)
    {
        ButtonLabel = label;
    }
}