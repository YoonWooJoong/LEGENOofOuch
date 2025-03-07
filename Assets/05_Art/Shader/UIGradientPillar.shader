Shader "UI/GradientPillar"
{
        Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha // 알파 블렌딩 적용
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float alphaY = 1.0 - abs(i.uv.y - 0.5) * 2.0;
                float alphaX = 1.0 - abs(i.uv.x - 0.5) * 2.0; 
                float alpha = alphaY * alphaX; 
                alpha = clamp(alpha, 0.0, 0.7);
                return fixed4(_Color.rgb, alpha);
     
            }
            ENDCG
        }
    }
}