Shader "Custom/VFX/SpikeyAttackVFXShader"
{
    Properties
    {
        _LineHeight("Altura da Linha", Float) = 0.5
        _LineThickness("Grossura da Linha", Float) = 0.2
        _FadeOut("Fade", Float) = 0
        _Color("Main Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderQueue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZTest Always
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            float _LineHeight;
            float _LineThickness;
            float _FadeOut;
            float4 _Color;
            
            fixed4 frag (v2f i) : SV_Target
            {
                
                i.uv.x = abs(i.uv.x-0.5);
                i.uv.x *= 1-i.uv.y;
                
                i.uv.y += i.uv.x;
                
                float4 col = 0;
                col += 1-(abs(i.uv.y - _LineHeight) * _LineThickness);
                col -= 1-saturate(abs(i.uv.x-0.5)*8 - _FadeOut);
                
                col *= _Color;
                col = saturate(col);
                //col = 0; col.a = 1; col.rg = i.uv;
                return col;
            }
            ENDCG
        }
    }
}
