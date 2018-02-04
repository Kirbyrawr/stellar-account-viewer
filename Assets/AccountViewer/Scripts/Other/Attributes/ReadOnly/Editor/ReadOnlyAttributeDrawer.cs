using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[UnityEditor.CustomPropertyDrawer (typeof(ReadOnlyAttribute))]
public class ReadOnlyAttributeDrawer : PropertyDrawer
{
	public override void OnGUI(Rect rect, UnityEditor.SerializedProperty prop, GUIContent label)
	{
		bool wasEnabled = GUI.enabled;
		GUI.enabled = false;
		UnityEditor.EditorGUI.PropertyField(rect,prop);
		GUI.enabled = wasEnabled;
	}
}