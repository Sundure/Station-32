using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections.Generic;
[CustomEditor(typeof(LightSystem))]
public class LightSystemCustomEditor : Editor
{
    private List<float> _lightSwitchInterval;
    private List<bool> _enabled;

    private void OnEnable()
    {
        Type type = typeof(LightSystem);

        FieldInfo info = type.GetField("_lightSwitchInterval", BindingFlags.NonPublic | BindingFlags.Instance); //Change String Values If You Want To Change LightSystem Variable Names

        _lightSwitchInterval = (List<float>)info.GetValue(target);

        info = type.GetField("_enabled", BindingFlags.NonPublic | BindingFlags.Instance); //Change String Values If You Want To Change LightSystem Variable Names

        _enabled = (List<bool>)info.GetValue(target);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();

        CreateLightBehaviorList();

        Type type = typeof(LightSystem);

        type.GetField("_lightSwitchInterval", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, _lightSwitchInterval);
        type.GetField("_enabled", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(target, _enabled);

        EditorUtility.SetDirty(target);

        serializedObject.ApplyModifiedProperties();
    }

    private void CreateLightBehaviorList()
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField($"List Size: {_lightSwitchInterval.Count}", GUILayout.Width(110));

        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < _lightSwitchInterval.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField($"Element {i}", GUILayout.Width(80));

            EditorGUILayout.LabelField("Time", GUILayout.Width(30));

            _lightSwitchInterval[i] = EditorGUILayout.FloatField(_lightSwitchInterval[i], GUILayout.Width(100));

            EditorGUILayout.LabelField("Light", GUILayout.Width(30));

            _enabled[i] = EditorGUILayout.Toggle(_enabled[i]);

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Add Element"))
        {
            if (_lightSwitchInterval.Count == 0)
            {
                _lightSwitchInterval.Add(0);
                _enabled.Add(true);
                return;
            }
            _lightSwitchInterval.Add(_lightSwitchInterval[^1]);
            _enabled.Add(!_enabled[^1]);
        }

        if (_lightSwitchInterval.Count > 0 && GUILayout.Button("Delete Element"))
        {
            _lightSwitchInterval.RemoveAt(_enabled.Count - 1);
            _enabled.RemoveAt(_enabled.Count - 1);
        }
    }
}