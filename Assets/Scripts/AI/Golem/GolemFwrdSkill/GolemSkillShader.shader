Shader "Unlit/GolemSkillShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Slider ("Distance Slider", Range(0,1)) = 0
        _Size ("Size", Float) = 1
    }
    SubShader
    {
        Tags { "RenderQueue"="Transparent" "RenderType"="Transparent" }

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
            float _Slider;
            float _Size;

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
                fixed4 col = tex2D(_MainTex, i.position.xz * _Size);



                return col;
            }
            ENDCG
        }
    }
}
