using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraVPUniform : MonoBehaviour
{
    public Camera cam;
    private void Update()
    {

        Shader.SetGlobalMatrix("_Vmatrix", cam.worldToCameraMatrix);
        Shader.SetGlobalMatrix("_Pmatrix", cam.projectionMatrix);
    }
}
