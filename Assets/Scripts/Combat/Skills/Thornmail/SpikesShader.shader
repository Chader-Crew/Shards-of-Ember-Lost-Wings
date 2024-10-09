Shader "Custom/SpikesShader"
{
    Properties
    {
        _MainColor ("Color", Color) = (1,1,1,1)
        _VertTex ("Offset Texture", 2D) = "white" {}
        _Frequency ("Frequency", Float) = 1
        _Amp ("Amplitude", Float) = 1

    }
    SubShader
    {
        Tags { "RenderQueue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        Cull OFF
        Blend srcAlpha oneMinusSrcAlpha

        Pass
        {
            CGPROGRAM

            #pragma vertex vert

            #pragma fragment frag

            #include "UnityCG.cginc"
 
            struct appdata
            {
                float4 vertex: POSITION;
                float2 uv: TEXCOORD0;
                float3 normal: NORMAL;
            };
                
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex:  SV_POSITION;
                float offset : COLOR;
            };

            float4 _MainColor;

            sampler2D _VertTex;

            float4 _VertTex_ST;

            float _Frequency;
            float _Amp;

            v2f vert (appdata v)
            {
                v2f o;

                o.uv = TRANSFORM_TEX(v.uv, _VertTex);

                float offset = saturate(sin(_Time.x * _Frequency) * (tex2Dlod(_VertTex, float4 (o.uv,0,0)))) * _Amp;

                o.offset = offset;

                o.vertex = UnityObjectToClipPos(v.vertex + offset*v.normal);


                return o;
            }

            fixed4 frag (v2f i): SV_Target
            {
                fixed4 col = _MainColor;

                col.a = i.offset;

                return col;
            }
            ENDCG
        }
    }
}
