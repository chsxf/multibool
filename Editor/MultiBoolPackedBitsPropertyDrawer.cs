using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace chsxf
{
    [CustomPropertyDrawer(typeof(MultiBoolPackedBitsAttribute))]
    public class MultiBoolPackedBitsPropertyDrawer : PropertyDrawer
    {
        private readonly List<string> bitLabels = new();
        private Type lastLabelType;

        private void PrepareBitLabels() {
            Type labelType;
            if ((fieldInfo.DeclaringType == typeof(MultiBool8))
                || (fieldInfo.DeclaringType == typeof(MultiBool16))
                || (fieldInfo.DeclaringType == typeof(MultiBool32))
                || (fieldInfo.DeclaringType == typeof(MultiBool64))) {
                labelType = typeof(void);
            }
            else {
                Type genericType = fieldInfo.DeclaringType;
                labelType = genericType!.GetGenericArguments()[0];
            }

            if (lastLabelType != labelType) {
                lastLabelType = labelType;
                bitLabels.Clear();
                if (lastLabelType == typeof(void)) {
                    int byteCount;
                    if (fieldInfo.FieldType == typeof(ulong)) {
                        byteCount = sizeof(ulong);
                    }
                    else if (fieldInfo.FieldType == typeof(uint)) {
                        byteCount = sizeof(uint);
                    }
                    else if (fieldInfo.FieldType == typeof(ushort)) {
                        byteCount = sizeof(ushort);
                    }
                    else {
                        byteCount = sizeof(byte);
                    }
                    int labelCount = byteCount * 8;
                    GenerateNumericalLabels(labelCount);
                }
                else {
                    bitLabels.AddRange(Enum.GetNames(labelType));
                }
            }
        }

        private void GenerateNumericalLabels(int _count) {
            for (int i = 1; i <= _count; i++) {
                string suffix = "th";
                if (i is < 10 or > 19) {
                    suffix = (i % 10) switch {
                        1 => "st",
                        2 => "nd",
                        3 => "rd",
                        _ => "th"
                    };
                }
                bitLabels.Add($"{i} {suffix}");
            }
        }

        public override float GetPropertyHeight(SerializedProperty _property, GUIContent _label) {
            PrepareBitLabels();

            int linesAfterFirst = (EditorGUIUtility.wideMode ? bitLabels.Count / 2 : bitLabels.Count) - 1;
            return EditorGUIUtility.singleLineHeight
                   + ((EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * linesAfterFirst);
        }

        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label) {
            PrepareBitLabels();

            Rect togglesRect = _position;
            ulong b = _property.ulongValue;
            for (int i = 0; i < bitLabels.Count; i++) {
                b = DrawBitToggle(togglesRect, b, i);
            }
            _property.ulongValue = b;
        }

        private ulong DrawBitToggle(Rect _containerRect, ulong _value, int _index) {
            Rect toggleRect = _containerRect;
            toggleRect.height = EditorGUIUtility.singleLineHeight;
            if (EditorGUIUtility.wideMode) {
                int row = _index / 2;
                toggleRect.y += (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * row;

                int column = _index % 2;
                toggleRect.width /= 2;
                if (column != 0) {
                    toggleRect.x += toggleRect.width;
                }
            }
            else {
                toggleRect.y += (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * _index;
            }
            toggleRect.x += 20;
            toggleRect.width -= 20;

            ulong bit = (ulong) (1L << _index);
            bool wasChecked = (_value & bit) != 0;
            bool isChecked = EditorGUI.ToggleLeft(toggleRect, bitLabels[_index], wasChecked);
            if (isChecked != wasChecked) {
                if (isChecked) {
                    _value |= bit;
                }
                else {
                    _value &= ~bit;
                }
            }
            return _value;
        }
    }
}
