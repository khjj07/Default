using System;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
#endif

namespace Default
{
    
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class BigHeaderAttribute : PropertyAttribute
    {
        public string _Text
        {
            get { return mText; }
        }

        private string mText = String.Empty;

        public BigHeaderAttribute(string text)
        {
            mText = text;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(BigHeaderAttribute))]
    public class BigHeaderAttributeDrawer : DecoratorDrawer
    {
        public override void OnGUI(Rect position)
        {
            BigHeaderAttribute attributeHandle = (BigHeaderAttribute)attribute;

            // 1. 텍스트 기반으로 색상 생성
            Color32 headerColor = GetColorFromText(attributeHandle._Text);

            position.yMin += EditorGUIUtility.singleLineHeight * 0.5f;

            // This line of code was fetched from the internal unity header attribute implementation
            position = EditorGUI.IndentedRect(position);

            GUIStyle headerTextStyle = new GUIStyle()
            {
                fontSize = 16,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft
            };

            // 2. GUIStyle에 생성된 색상 적용
            headerTextStyle.normal.textColor = headerColor;

            GUI.Label(position, attributeHandle._Text, headerTextStyle);

            // 3. 구분선에도 생성된 색상 적용
            EditorGUI.DrawRect(new Rect(position.xMin, position.yMin, position.width, 1), headerColor);
        }

        public override float GetHeight()
        {
            return EditorGUIUtility.singleLineHeight * 2;
        }

        /// <summary>
        /// 문자열을 기반으로 일관성 있는 Color32를 생성합니다.
        /// </summary>
        private Color32 GetColorFromText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new Color32(125, 100, 250, 255); // 기본 색상 반환
            }

            // 1. 문자열의 해시 코드를 가져옵니다.
            // C# 문자열의 GetHashCode()는 일관적이지만, Unity 에디터 세션마다 다를 수 있으므로,
            // 보다 일관적인 커스텀 해시 함수를 사용하는 것이 좋습니다. 여기서는 간단하게 GetHashCode()를 사용합니다.
            int hash = text.GetHashCode();

            // 2. 해시 값을 각 R, G, B 채널에 매핑합니다. (0~255 범위로)
            // 비트 연산자를 사용하여 해시를 3개의 8비트 값으로 나눕니다.
            // 이렇게 하면 텍스트가 달라질 때마다 색상도 크게 달라져 고유해집니다.

            byte r = (byte)((hash & 0xFF0000) >> 16);
            byte g = (byte)((hash & 0x00FF00) >> 8);
            byte b = (byte)(hash & 0x0000FF);

            // 3. 색상 대비를 위해 약간의 보정을 해줍니다. (선택 사항)
            // 배경색(흰색/밝은 회색)과 잘 구별되는 밝기 범위(예: 50-200)로 강제 조정합니다.
            // 예를 들어, 너무 어두운 색상은 밝게, 너무 밝은 색상은 조금 어둡게 보정합니다.

            // 각 채널을 100~200 범위로 강제 조정하여 적절한 대비를 만듭니다.
            r = (byte)(r % 100 + 100);
            g = (byte)(g % 100 + 100);
            b = (byte)(b % 100 + 100);

            return new Color32(r, g, b, 255);
        }
    }
#endif
}
