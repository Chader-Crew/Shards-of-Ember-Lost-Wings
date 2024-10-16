Shader "Custom/VFX/WindDomeShader"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (1,1,1,1)
        _HorTile ("Horizontal Tiling", Float) = 1
        _Thickness ("Line Thickness", Range(0,1)) = 0
        _FadeValue ("Fade Value", Float) = 20
    }
    SubShader
    {
        Tags { "RenderQueue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
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

            float4 _MainColor;
            float _HorTile;
            float _VertOff;
            float _Thickness;
            float _FadeValue;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float RNG(float n)
            {
                return frac(n*2675.4128+1.1613)%21.951;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                //regions
                float id = floor(i.uv.x * _HorTile)/_HorTile/2;
                
                //loop
                i.uv.x = frac(i.uv.x * _HorTile);
                //center
                i.uv.x = 1-abs(i.uv.x-0.5);
                //thickness rand (nao sei se devia ficar)
                i.uv.x -= (0.5+sin(_Time.x)/2) * RNG(id)*0.2;

                //base paint
                float4 col = _MainColor;
                //line thickness
                col.a = i.uv.x - (1-_Thickness);
                
                //fade out
                col.a -= pow(i.uv.y, _FadeValue);
                
                //y offset
                i.uv.y = frac(i.uv.y - (_Time.z + RNG(id*0.2)));
                //center
                i.uv.y = abs(i.uv.y-0.5);
                //line length
                col.a -= i.uv.y;
                
                //col.rg = i.uv; col.a = 1;
                return col;
            }
            ENDCG
        }
    }
}
