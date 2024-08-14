Shader "Custom/FlatColor"
{
    Properties
    {
        _Color ("Color", Color) = (1,0,0,1)
    }
    SubShader
    {
        Pass
        {
            Color[_Color]
            ZTest Always
        }
    }
}
