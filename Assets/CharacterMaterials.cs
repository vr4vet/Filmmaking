using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CharacterMaterials : MonoBehaviour
{
    public Color shadowColor;
    public float shadowStrength;
    public List<Material> materials;
    private void Update()
    {
        foreach (Material mat in materials)
        {
            mat.SetColor("_ShadowColor", shadowColor);
            mat.SetFloat("Vector1_c3323d9d6a354a70b26eed8c20185909", shadowStrength);
        }
    }
}
