#include "UnityCG.cginc"
// #include "HLSLSupport.cginc"
// #include "UnityShaderVariables.cginc"

#pragma vertex Vertex
#pragma fragment Fragment

struct Attributes {
    float3 positionOS : POSITION;
};

struct Interpolators {
    float4 positionCS : SV_POSITION;
};

Interpolators Vertex(Attributes input) {
    Interpolators output;

    output.positionCS = UnityObjectToClipPos(input.positionOS);

    return output;
}

float4 Fragment(Interpolators input) : SV_TARGET {
    return float4(1, 1, 1, 1);
}
