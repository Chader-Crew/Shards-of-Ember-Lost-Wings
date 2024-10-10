Shader "Custom/StaticLineShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1) 
        _MainTex ("Main Texture", 2D) = "white" {}
        _Mult ("Float", Float) = 1
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	    LOD 100

        ZWrite Off
        Cull OFF
	    Blend SrcAlpha OneMinusSrcAlpha 

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _Mult;

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
                fixed2 offset = i.uv;
                offset.y -= 0.2;
                fixed b = (offset.x-0.5) * 3;
                fixed a = 0.5;
                offset.y -= (abs(frac((b - a + _Time.w*2) / (a * 2.0)) * a * 2.0 - a));
                offset = abs(offset);
                offset *= 3;
                offset = saturate(1-offset); 


                fixed4 col = _Color;
                col.a = offset.y;
                fixed w = round(col.a * _Mult);
                
                //col.rgb = 0; col.a = 1;col.rg = offset.rg;
                return col + w;
            }
            ENDCG
        }
    }
}
