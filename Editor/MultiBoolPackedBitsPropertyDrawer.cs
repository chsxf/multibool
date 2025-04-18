using System;
using System.Collections.Generic;
using chsxf;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(MultiBoolPackedBitsAttribute))]
    public class MultiBoolPackedBitsPropertyDrawer : PropertyDrawer
    {
        private readonly List<string> bitLabels = new();
        private Type lastLabelType;

        private void PrepareBitLabels() {
            Type labelType;
            if (fieldInfo.DeclaringType == typeof(MultiBool)) {
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
                    bitLabels.Add("First");
                    bitLabels.Add("Second");
                    bitLabels.Add("Third");
                    bitLabels.Add("Fourth");
                    bitLabels.Add("Fifth");
                    bitLabels.Add("Sixth");
                    bitLabels.Add("Seventh");
                    bitLabels.Add("Eighth");
                }
                else {
                    bitLabels.AddRange(Enum.GetNames(labelType));
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty _property, GUIContent _label) {
            int lines = EditorGUIUtility.wideMode ? 3 : 7;
            return EditorGUIUtility.singleLineHeight
                   + ((EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * lines);
        }

        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label) {
            PrepareBitLabels();

            Rect togglesRect = _position;
            byte b = (byte) _property.intValue;
            b = DrawBitToggle(togglesRect, b, _index: 0);
            b = DrawBitToggle(togglesRect, b, _index: 1);
            b = DrawBitToggle(togglesRect, b, _index: 2);
            b = DrawBitToggle(togglesRect, b, _index: 3);
            b = DrawBitToggle(togglesRect, b, _index: 4);
            b = DrawBitToggle(togglesRect, b, _index: 5);
            b = DrawBitToggle(togglesRect, b, _index: 6);
            b = DrawBitToggle(togglesRect, b, _index: 7);
            _property.intValue = b;
        }

        private byte DrawBitToggle(Rect _containerRect, byte _value, int _index) {
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

            int bit = 1 << _index;
            bool wasChecked = (_value & bit) != 0;
            bool isChecked = EditorGUI.ToggleLeft(toggleRect, bitLabels[_index], wasChecked);
            if (isChecked != wasChecked) {
                if (isChecked) {
                    _value |= (byte) bit;
                }
                else {
                    _value &= (byte) ~bit;
                }
            }
            return _value;
        }
    }
}
