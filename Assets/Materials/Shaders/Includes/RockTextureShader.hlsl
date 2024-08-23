#include <Packages/com.blendernodesgraph.core/Editor/Includes/Importers.hlsl>

void RockTextureShader_float(float3 _POS, float3 _PVS, float3 _PWS, float3 _NOS, float3 _NVS, float3 _NWS, float3 _NTS, float3 _TWS, float3 _BTWS, float3 _UV, float3 _SP, float3 _VVS, float3 _VWS, Texture2D gradient_15572, Texture2D gradient_15574, out float4 Color, out float3 Normal)
{
	
	float _NoiseTexture_15566_fac; float4 _NoiseTexture_15566_col; node_noise_texture_full(_POS, 0, 3,3, 15, 0,55, 0, 2, _NoiseTexture_15566_fac, _NoiseTexture_15566_col);
	float _VoronoiTexture_15558_dis; float4 _VoronoiTexture_15558_col; float3 _VoronoiTexture_15558_pos; float _VoronoiTexture_15558_w; float _VoronoiTexture_15558_rad; voronoi_tex_getValue(_NoiseTexture_15566_col, 0, 1,6, 1, 0,5, 1, 0, 2, 0, _VoronoiTexture_15558_dis, _VoronoiTexture_15558_col, _VoronoiTexture_15558_pos, _VoronoiTexture_15558_w, _VoronoiTexture_15558_rad);
	float4 _ColorRamp_15572 = color_ramp(gradient_15572, _VoronoiTexture_15558_dis);
	float4 _MixRGB_15562 = mix_dark(0,3, _ColorRamp_15572, float4(1, 1, 1, 1));
	float _NoiseTexture_15570_fac; float4 _NoiseTexture_15570_col; node_noise_texture_full(_POS, 0, 3, 15, 0,55, 0, 2, _NoiseTexture_15570_fac, _NoiseTexture_15570_col);
	float _VoronoiTexture_15568_dis; float4 _VoronoiTexture_15568_col; float3 _VoronoiTexture_15568_pos; float _VoronoiTexture_15568_w; float _VoronoiTexture_15568_rad; voronoi_tex_getValue(_NoiseTexture_15570_col, 0, 2, 1, 0,5, 1, 0, 2, 0, _VoronoiTexture_15568_dis, _VoronoiTexture_15568_col, _VoronoiTexture_15568_pos, _VoronoiTexture_15568_w, _VoronoiTexture_15568_rad);
	float4 _ColorRamp_15574 = color_ramp(gradient_15574, _VoronoiTexture_15568_dis);
	float4 _Bump_15576; node_bump(_POS, 1, 0,908, 1, _ColorRamp_15574, _NTS, _Bump_15576);
	float4 _Bump_15578; node_bump(_POS, 1, 0,908, 1, _MixRGB_15562, _Bump_15576, _Bump_15578);

	Color = _MixRGB_15562;
	Normal = _Bump_15578;
}