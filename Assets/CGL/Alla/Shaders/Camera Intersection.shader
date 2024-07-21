Shader "Unlit/Camera Intersection"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
       Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float4 t :TEXCOORD1;
            };
            float4x4 _Vmatrix;
            float4x4 _Pmatrix;
            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.t = mul(_Pmatrix,mul(_Vmatrix,mul(unity_ObjectToWorld, v.vertex)));
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 pos = i.t / i.t.w;
                fixed4 col = tex2D(_MainTex, i.uv);
                col = float4(pos.x, pos.y, pos.z, 0);
                if (pos.z >= -1 && pos.z <= 1 && pos.x >= -1 && pos.x <= 1 && pos.y >= -1 && pos.y <= 1)
                {
                    col = float4(1, 0, 0, 0.3f);
                }

                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }