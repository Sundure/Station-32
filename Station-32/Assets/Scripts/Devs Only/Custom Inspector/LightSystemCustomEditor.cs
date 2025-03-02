using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LightSystem))]
public class LightSystemCustomEditor : Editor
{
    private SerializedProperty _floatList;
    private SerializedProperty _boolList;
    private SerializedProperty _light;
    private SerializedProperty _enabledLightMaterial;
    private SerializedProperty _disabledLightMaterial;
    private SerializedProperty _lampGlassGameObject;

    private void OnEnable()
    {
        _floatList = serializedObject.FindProperty("_lightSwitchInterval");                 //Change String Values If You Want To Change LightSystem Variable Names
        _boolList = serializedObject.FindProperty("_enabled");                              
        _light = serializedObject.FindProperty("_light");
        _enabledLightMaterial = serializedObject.FindProperty("_enabledLightMaterial");
        _disabledLightMaterial = serializedObject.FindProperty("_disabledLightMaterial");
        _lampGlassGameObject = serializedObject.FindProperty("_lampGlass");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(_lampGlassGameObject, new GUIContent("Lamp Glass"));

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_light, new GUIContent("Light"));

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(_enabledLightMaterial, new GUIContent("Enabled Light Material"));
        EditorGUILayout.PropertyField(_disabledLightMaterial, new GUIContent("Disabled Light Material"));

        EditorGUILayout.Space();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        int count = _floatList.arraySize;

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField($"List Size: {count}", GUILayout.Width(110));

        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < count; i++)
        {
            SerializedProperty floatElement = _floatList.GetArrayElementAtIndex(i);
            SerializedProperty boolElement = _boolList.GetArrayElementAtIndex(i);

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField($"Element {i}", GUILayout.Width(80));

            EditorGUILayout.LabelField("Time", GUILayout.Width(30));

            floatElement.floatValue = EditorGUILayout.FloatField(floatElement.floatValue, GUILayout.Width(100));

            EditorGUILayout.LabelField("Light", GUILayout.Width(30));

            boolElement.boolValue = EditorGUILayout.Toggle(boolElement.boolValue);

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Add Element"))
        {
            _floatList.arraySize++;
            _boolList.arraySize++;
        }

        if (count > 0 && GUILayout.Button("Delete Element"))
        {
            _floatList.arraySize--;
            _boolList.arraySize--;
        }

        serializedObject.ApplyModifiedProperties();
    }
}