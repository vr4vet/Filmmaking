using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[ExecuteAlways]
public class ToonLightsManager : MonoBehaviour
{
    public Light[] lights;

    private void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
    }

    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
    }

    private void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        // Collect light data
        Vector4[] lightPositions = new Vector4[lights.Length];
        Vector4[] lightColors = new Vector4[lights.Length];
        Vector4[] lightDirections= new Vector4[lights.Length];
        float[] lightRanges = new float[lights.Length];
        float[] lightStrengths = new float[lights.Length];
        float[] lightAngels = new float[lights.Length];
        for (int i = 0; i < lights.Length; i++)
        {
           
            lightPositions[i] = new Vector4(lights[i].transform.position.x, lights[i].transform.position.y, lights[i].transform.position.z, 1.0f); // Use w component for shadow attenuation if needed
            lightColors[i] = lights[i].color ;
            lightRanges[i] = lights[i].range;
            lightStrengths[i] = lights[i].enabled? lights[i].intensity:0;
            lightDirections[i] = lights[i].transform.forward;
            lightAngels[i] = lights[i].spotAngle;
        }

        // Pass data to the shader
        Shader.SetGlobalVectorArray("_LightPositions", lightPositions);
        Shader.SetGlobalVectorArray("_LightColors", lightColors);
        Shader.SetGlobalInt("_LightCount", lights.Length);
        Shader.SetGlobalFloatArray("_LightRanges", lightRanges);
        Shader.SetGlobalFloatArray("_LightStrengths", lightStrengths);
        Shader.SetGlobalVectorArray("_LightDirections", lightDirections);
        Shader.SetGlobalFloatArray("_LightAngels", lightAngels);
        
    }
}
