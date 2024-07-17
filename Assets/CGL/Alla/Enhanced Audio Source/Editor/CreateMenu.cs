using System.IO;
using UnityEngine;
using UnityEditor;

public class CreateMenu
{
    [MenuItem("GameObject/Audio/Sound Source")]

    static void CreateSoundSource()
    {
        GameObject soundSource = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Enhanced Audio Source/Sound Source.prefab", typeof(GameObject));
        PrefabUtility.InstantiatePrefab(soundSource);
    }
}
