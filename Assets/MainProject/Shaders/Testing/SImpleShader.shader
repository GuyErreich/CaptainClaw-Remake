Shader "CaptainClaw/SimpleShader"
{
    SubShader {
        // Tags {"RenderPipeline" = "UniversalPipeline"}
        // LOD 100

        Pass {
            Name "ForwardLit"
            Tags {"LightMode" = "UniversalForward"}
            
            CGPROGRAM

            #include "MyLitForwardLitPass.hlsl"

            ENDCG
        }
    }
}
