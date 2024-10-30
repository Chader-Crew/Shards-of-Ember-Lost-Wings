Shader "Unlit/GolemSkillShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MainColor ("Color", Color) = (1,1,1,1)
        _Slider ("Distance Slider", Range(0,2)) = 0
        _BorderSize("BorderSize", Float) = 1
        _TexSize ("Texture Size", Float) = 1
    }
    SubShader
    {
        Tags { "RenderQueue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 position : W_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed _Slider;
            fixed _TexSize;
            float4 _MainColor;
            fixed _BorderSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.position = mul(UNITY_MATRIX_M, v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //world xz texture
                fixed4 col = tex2D(_MainTex, i.position.xz * _TexSize);

                //wave using slider for animation
                fixed wave = frac(i.uv.y - saturate(_Slider)) - saturate(i.uv.y*(1-_Slider));
                //slider 1-2 values for fading out
                wave -= saturate(_Slider-1);
                //im to bad at math to not saturate this
                wave = saturate(wave);

                //wave that crack
                col *= wave;
                //vwoosh that crack (god damn it these negative values)
                col += saturate(_MainColor * (wave * _BorderSize - _BorderSize+1));
                

                return col;
            }
            ENDCG
        }
    }
}
