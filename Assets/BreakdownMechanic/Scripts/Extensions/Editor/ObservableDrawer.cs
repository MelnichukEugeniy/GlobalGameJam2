using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomPropertyDrawer(typeof(Observable<>), true)]
public class ObservableDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Get the "value" field inside Observable<T>
        SerializedProperty valueProperty = property.FindPropertyRelative("value");

        if (valueProperty != null)
        {
            // Draw the value field with correct type handling
            EditorGUI.PropertyField(position, valueProperty, label, true);
        }
        else
        {
            // Fallback: Use reflection for non-serializable cases
            object target = GetTargetObject(property);
            if (target == null)
            {
                EditorGUI.LabelField(position, label.text, "Null Observable");
                return;
            }

            FieldInfo valueField = target.GetType().GetField("value", BindingFlags.NonPublic | BindingFlags.Instance);
            if (valueField == null)
            {
                EditorGUI.LabelField(position, label.text, "Unsupported type (Reflection failed)");
                return;
            }

            object fieldValue = valueField.GetValue(target);
            fieldValue = DrawField(position, label, fieldValue, valueField.FieldType);

            valueField.SetValue(target, fieldValue);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty valueProperty = property.FindPropertyRelative("value");
        return valueProperty != null ? EditorGUI.GetPropertyHeight(valueProperty) : EditorGUIUtility.singleLineHeight;
    }

    private object GetTargetObject(SerializedProperty property)
    {
        object obj = property.serializedObject.targetObject;
        string[] paths = property.propertyPath.Split('.');

        foreach (string path in paths)
        {
            FieldInfo field = obj.GetType().GetField(path, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (field == null) return null;
            obj = field.GetValue(obj);
        }
        return obj;
    }

    private object DrawField(Rect position, GUIContent label, object value, System.Type type)
    {
        if (type == typeof(int))
            return EditorGUI.IntField(position, label, (int)(value ?? 0));

        if (type == typeof(float))
            return EditorGUI.FloatField(position, label, (float)(value ?? 0f));

        if (type == typeof(bool))
            return EditorGUI.Toggle(position, label, (bool)(value ?? false));

        if (type == typeof(string))
            return EditorGUI.TextField(position, label, (string)(value ?? ""));

        if (typeof(UnityEngine.Object).IsAssignableFrom(type))
            return EditorGUI.ObjectField(position, label, (UnityEngine.Object)value, type, true);

        EditorGUI.LabelField(position, label.text, "Unsupported Type");
        return value;
    }
}
