Shader "Custom/ImpactVFXShader"
{
    Properties
    {
        _Color ("Tint", Color) = (1,1,1,1)
        _Slider ("Slider", Float) = 0
        _Thickness ("Thickness", Range(0.0000001,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull Off
        LOD 100

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

            float4 _Color;
            float _Slider, _Thickness, _SparkCount, _SparkSlider;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;//TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                i.uv = (i.uv-0.5)*2;
                i.uv.y = length(i.uv);

                i.uv.y = abs(i.uv.y-_Slider);   //radial loop
                i.uv.y = 1-i.uv.y;              //invert
                i.uv.y = (i.uv.y/_Thickness)-(1/_Thickness)+1;    //thin

                clip(i.uv.y);

                return _Color;
            }
            ENDCG
        }
    }
}
