Shader "Custom/ShoveDashVFXShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1) 
        _MainTex ("Main Texture", 2D) = "white" {}
        _Thickness ("Thickness", Range(0,1)) = 1
        _ScaleX ("ScaleX", Float) = 1
        _ScaleY ("ScaleY", Float) = 1
        _Warp ("Warp Intensity", Float) = 1
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}

        ZWrite Off
        Cull Off
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
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _Thickness;
            float _ScaleX;
            float _ScaleY;
            float _Warp;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                i.uv.x = sin(i.uv.x* _ScaleX)-0.5;
                
                float fade = i.uv.y;

                i.uv.y += i.uv.x * _Warp;
                i.uv.y -= _Time.z;
                i.uv.y = abs(frac(i.uv.y * _ScaleY) - 0.5);
                float4 col = (_Thickness - i.uv.y) / _Thickness;

                fade = abs(fade-0.5)*1.8;
                col = saturate(col - fade);
                col *= _Color;
                return col;
                
                //col = 0; col.a = 1; col.rg = i.uv;
            }
            ENDCG
        }
    }
}
