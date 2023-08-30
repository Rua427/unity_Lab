Shader "Custom/Portal"
{
    Properties
    {
        _InactiveColour ("Inactive Colour", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags {"RenderType" = "Opaque"}
        LOD 100
        Cull Off

        Pass
        {
            Name "Portal"

            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                // 각 정점의 위치를 가져옴
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _InactiveColour;
            int displayMask;

            v2f vert (appdata v)
            {
                v2f o;

                // object to clip pos
                o.vertex = UnityObjectToClipPos(v.vertex);
                // clip pos to screen pos
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i ) : SV_Target
            {
                // 스크린좌표에서 텍스처 좌표로 변환
                //깊이 값에 비례한 텍스처 좌표를 얻음 // 원근법 적용
                float2 uv = i.screenPos.xy / i.screenPos.w;
                fixed4 portalCol = tex2D(_MainTex, uv);
                return portalCol * displayMask + _InactiveColour * (1-displayMask);
            }

            ENDCG
        }
    }

    Fallback "Universal Render Pipeline/Lit"
}
