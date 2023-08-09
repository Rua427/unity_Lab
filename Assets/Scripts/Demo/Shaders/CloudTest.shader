Shader "Demo/CloudTest"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Emission ("Emission", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Transparent" }
        LOD 200

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"
            #include "UnityShaderVariables.cginc"

            sampler2D _MainTex;

            struct Input
            {
                float2 uv_MainTex;
            };

            half _Glossiness;
            half _Metallic;
            fixed4 _Color;
            fixed4 _Emission;

            void vert(inout appdata_full data)
            {
                float3 worldVert = mul(unity_ObjectToWorld, data.vertex).xyz;
                //data.vertex.xyz += sin(_Time.x + data.vertex.xyz + worldVert * .1) * .2;
            }

            void frag(Input IN, inout SurfaceOutputStandard o)
            {
                // Albedo comes from a texture tinted by color
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                // Metallic and smoothness come from slider variables
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
                o.Emission = _Emission.rgb;
                o.Alpha = c.a;
            }
            ENDHLSL
        }
    }
    FallBack "Diffuse"
}
