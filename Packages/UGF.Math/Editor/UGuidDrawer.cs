using System;
using UGF.Math.Runtime;
using UnityEditor;
using UnityEngine;

namespace UGF.Math.Editor
{
    [CustomPropertyDrawer(typeof(UGuid))]
    internal sealed class UGuidDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty propertyValue0 = property.FindPropertyRelative("m_value0");
            SerializedProperty propertyValue1 = property.FindPropertyRelative("m_value1");

            var uguid = new UGuid(propertyValue0.longValue, propertyValue1.longValue);
            string value = uguid.ToString();

            string changed = EditorGUI.DelayedTextField(position, label, value);
            
            if (value != changed)
            {
                value = changed;
                
                if (Guid.TryParse(value, out Guid guid))
                {
                    uguid = guid;

                    propertyValue0.longValue = uguid.Value0;
                    propertyValue1.longValue = uguid.Value1;
                }
                else
                {
                    Debug.LogWarning($"Invalid value for guid: '{value}'.");
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}