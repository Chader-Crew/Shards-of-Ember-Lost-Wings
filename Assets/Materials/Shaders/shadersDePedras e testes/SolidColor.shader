Shader "Custom/SolidColor"
{
    Properties
    {
        _MainColor ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Pass
        {
            Color[_MainColor]
        }
    }
    Fallback "Default"
}
