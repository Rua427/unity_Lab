Shader "Custom/Slice"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0, 1)) = 0.5
        _Metallic ("Metallic", Range(0, 1)) = 0.0
        
        sliceNormal("normal", Vector) = (0, 0, 0, 0)
        sliceCentre ("centre", Vector) = (0, 0, 0, 0)
        sliceOffsetDst("offset", Float) = 0
     }

     SubShader
     {
        Tags { "Queue" = "Geometry" "IgnoreProjector" = "True" "RenderType"="Geometry"}
        LOD 200
        Name "Slice"

        CGPROGRAM
        #pragma surface surf Standard addshadow
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        
        sampler2D _MainTex;
        
        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        float3 sliceNormal;
        float3 sliceCentre;
        float sliceOffsetDst;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float3 adjustCentre = sliceCentre + sliceNormal * sliceOffsetDst;
            float3 offsetToSliceCentre = adjustCentre - IN.worldPos;
            clip(dot(offsetToSliceCentre, sliceNormal));

            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    Fallback "Universal Render Pipeline/Lit"

}
