using chsxf;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(MultiBool))]
    public class MultiBoolPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty _property, GUIContent _label) {
            int lines = EditorGUIUtility.wideMode ? 4 : 8;
            return EditorGUIUtility.singleLineHeight
                   + ((EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * lines);
        }

        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label) {
            Rect labelRect = _position;
            labelRect.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(labelRect, _label);

            Rect togglesRect = _position;
            float offset = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            togglesRect.y += offset;
            togglesRect.height -= offset;

            SerializedProperty boolBits = _property.FindPropertyRelative("boolBits");

            byte b = (byte) boolBits.intValue;
            b = DrawBitToggle(togglesRect, "First", b, _index: 0);
            b = DrawBitToggle(togglesRect, "Second", b, _index: 1);
            b = DrawBitToggle(togglesRect, "Third", b, _index: 2);
            b = DrawBitToggle(togglesRect, "Fourth", b, _index: 3);
            b = DrawBitToggle(togglesRect, "Fifth", b, _index: 4);
            b = DrawBitToggle(togglesRect, "Sixth", b, _index: 5);
            b = DrawBitToggle(togglesRect, "Seventh", b, _index: 6);
            b = DrawBitToggle(togglesRect, "Eighth", b, _index: 7);
            boolBits.intValue = b;
        }

        private byte DrawBitToggle(Rect _containerRect, string _label, byte _value, int _index) {
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
            bool isChecked = EditorGUI.ToggleLeft(toggleRect, _label, wasChecked);
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
