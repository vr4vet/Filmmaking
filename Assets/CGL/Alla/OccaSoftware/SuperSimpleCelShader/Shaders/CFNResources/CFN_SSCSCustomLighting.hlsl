#ifndef SSCS_CUSTOM_INCLUDED
#define SSCS_CUSTOM_INCLUDED

#pragma multi_compile _MAIN_LIGHT_SHADOWS
#pragma multi_compile _MAIN_LIGHT_SHADOWS_CASCADE
#pragma multi_compile _SHADOWS_SOFT

#define EPSILON 0.0001
#define MAX_LIGHTS 16 // Define the maximum number of lights

float Pow2OneMinus(float a)
{
    a *= a;
    return (1.0 - a);
}

float smoothstep(float edge0, float edge1, float x)
{
    x = saturate((x - edge0) / (edge1 - edge0));
    return x * x * (3 - 2 * x);
}
float4 _LightPositions[10];
float3 _LightColors[10];
float _LightAngels[10];
float _LightAngelsSoftness[10];
float _LightStrengths[10];
float3 _LightDirections[10];
float _LightRanges[10];
int _LightCount;
// Function to get lighting with multiple lights
void GetSSCSLighting_float(float3 baseColor, UnityTexture2D baseColorTexture, float2 UV,
    float highlightArea, float4 highlightColor, float rimArea, float4 rimColor, float4 shadowColor,
    float3 worldPosition, float3 normalDirection,
    out float3 result)
{
    result = 0;

#ifndef SHADERGRAPH_PREVIEW 
    float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - worldPosition);
    float3 color =float3(0,0,0);
    float3 orgcolor = color;
    highlightArea = Pow2OneMinus(highlightArea);
    // Loop through each light
    for (int i = 0; i < _LightCount; i++)
    {
        float4 lightPos = _LightPositions[i];
        float3 lightColor = _LightColors[i].rgb;

        float3 lightDirection = normalize(lightPos.xyz - worldPosition);
        float nDotL = dot(normalDirection, lightDirection);
        float3 halfDirection = normalize(lightDirection + viewDirection);
        float nDotH = dot(normalDirection, halfDirection);
        float inAngel = dot(_LightDirections[i], normalize(worldPosition- lightPos.xyz)  );
        inAngel = acos(inAngel);

        // Convert radians to degrees (optional)
        inAngel = degrees(inAngel);

        // Diffuse
        float diffuse = nDotL ;
        diffuse = step(0.0, diffuse) * (_LightStrengths[i] * 1.2f) * clamp((1-(inAngel /(_LightAngels[i]/2))),0,1);
        diffuse = smoothstep(0.0f , 1.0f, diffuse);
        // Specular
        
        float specular = step(highlightArea , (nDotH) * (_LightStrengths[i]*0.7f))* smoothstep(highlightArea-0.5f, 1.0f, nDotH * _LightStrengths[i]);

        // Rim
        float ra = Pow2OneMinus(rimArea);
        float fresnel = 1.0 - saturate(dot(normalDirection, viewDirection));
        float rim = step(ra, fresnel);

        // Combine
        float shadowAtten = step(EPSILON, (1-(distance(lightPos.xyz,worldPosition)/ _LightRanges[i]))); // Use w component for shadow attenuation
        float litRegion = diffuse * shadowAtten;
        litRegion *= (_LightStrengths[i] / 100);
        //float3 clr = lerp(color, rimColor.rgb, rim * rimColor.a * step(EPSILON, rimArea) * litRegion);
        //clr = lerp(clr, _LightColors[i]/** highlightColor.rgb*/, specular /** highlightColor.a*/ * litRegion);
        float3 clr = lerp(color, baseColor * SAMPLE_TEXTURE2D(baseColorTexture.tex, baseColorTexture.samplerstate, UV).rgb*shadowColor.rgb, (1.0 - litRegion) * shadowColor.a);
        clr = lerp(clr, _LightColors[i] * SAMPLE_TEXTURE2D(baseColorTexture.tex, baseColorTexture.samplerstate, UV).rgb, litRegion);
        color += clr;
    }

    result = color;
#endif
}

#endif