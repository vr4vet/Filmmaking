using UnityEngine;
# if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(SoundSource))]
[CanEditMultipleObjects]
public class SoundSourceEditor : Editor

{
    SerializedProperty lookAtPoint;

    void OnEnable()
    {
        lookAtPoint = serializedObject.FindProperty("lookAtPoint");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(lookAtPoint);
        serializedObject.ApplyModifiedProperties();
    }
}
