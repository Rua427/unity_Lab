Shader "Demo/CloudTest"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Emission ("Emission", Color) = (1, 1, 1, 1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0, 1)) = 0.5
        _Metallic ("Metallic", Range(0, 1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "Queue" = "Transparent" }
        LOD 200

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 4.5
            #include "Packages/UniversalRP/Runtime/Shaders/ShaderLibrary/UnityCG.cginc"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            float4 _Color;
            float4 _Emission;
            float _Glossiness;
            float _Metallic;
            Texture2D<float4> _MainTex;
            SamplerState _MainTex_sampler;

            struct VertexInput
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct VertexOutput
            {
                float2 uv_MainTex;
                float4 pos : SV_POSITION;
            };

            VertexOutput vert(VertexInput v)
            {
                VertexOutput o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv_MainTex = v.uv;
                return o;
            }

            float4 frag(VertexOutput IN [[stage_in]]) : SV_Target
            {
                float4 albedo = _MainTex.Sample(_MainTex_sampler, IN.uv_MainTex) * _Color;
                float4 emission = _Emission.rgb;

                float4 output;
                output.rgb = albedo.rgb;
                output.a = albedo.a;
                output.rgb += emission.rgb;

                return output;
            }

            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
