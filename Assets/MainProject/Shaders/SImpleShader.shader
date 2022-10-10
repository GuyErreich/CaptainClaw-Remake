Shader "Unlit/SImpleShader"
{
    Properties
    {
        // _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        // LOD 100

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma Geometry geom

            #include "UnityCG.cginc"

            struct VertexInput
            {
                float4 vertex : POSITION;
                float3 normal: NORMAL;
                float2 uv0: TEXCOORD0;
                // float4 colors: COLOR;
                // flaot4 tangent: TANGENT;
                // float2 uv1: TEXCOORD1;
            };

            struct VertexOutput
            {
                float4 clipSpacePosition : SV_POSITION;
                float2 uv0: TEXCOORD0;
                float3 normal : TEXCOORD1;
            };

            // sampler2D _MainTex;
            // float4 _MainTex_ST;

            VertexOutput vert (VertexInput v)
            {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.normal = v.normal;
                o.clipSpacePosition = UnityObjectToClipPos(v.vertex);
                return o;
            }

            float4 frag (VertexOutput o) : SV_Target
            {
                float2 uv = o.uv0;
                float3 lightDir = normalize(float3(1, 1, 1));
                float3 normal = o.normal;
                return float4(normal, 0);
            }

            ENDCG
        }
    }
}
