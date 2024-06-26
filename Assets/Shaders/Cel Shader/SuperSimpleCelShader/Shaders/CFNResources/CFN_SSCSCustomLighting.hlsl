#ifndef SSCS_CUSTOM_INCLUDED
#define SSCS_CUSTOM_INCLUDED

#pragma multi_compile _MAIN_LIGHT_SHADOWS
#pragma multi_compile _MAIN_LIGHT_SHADOWS_CASCADE
#pragma multi_compile _SHADOWS_SOFT

#define EPSILON 0.0001

float Pow2OneMinus(float a)
{
	a *= a;
	return (1.0 - a);
}

void GetSSCSLighting_float(float3 baseColor, UnityTexture2D baseColorTexture, float2 UV, 
float highlightArea, float4 highlightColor, float rimArea, float4 rimColor, float4 shadowColor, 
float3 worldPosition, float3 normalDirection,
out float3 result)
{
	result = 0;
	
	#ifndef SHADERGRAPH_PREVIEW 
	float4 shadowCoord = TransformWorldToShadowCoord(worldPosition);
	Light light = GetMainLight(shadowCoord);
	float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - worldPosition);
	float3 lightDirection = normalize(light.direction);
	float nDotL = dot(normalDirection, lightDirection);
	float3 halfDirection = normalize(lightDirection + viewDirection);
	float nDotH = dot(normalDirection, halfDirection);
	
	// Diffuse
	float diffuse = nDotL;
	diffuse = step(0.0, diffuse);
	
	// Specular
	highlightArea = Pow2OneMinus(highlightArea);
	float specular = step(highlightArea, nDotH);
	
	// Rim
	float ra = Pow2OneMinus(rimArea);
	float fresnel = 1.0 - saturate(dot(normalDirection, viewDirection));
	float rim = step(ra, fresnel);
	
	// Combine
	float shadowAtten = step(EPSILON, light.shadowAttenuation);
	float litRegion = diffuse * shadowAtten;
	
	float3 color = baseColor * SAMPLE_TEXTURE2D(baseColorTexture.tex, baseColorTexture.samplerstate, UV).rgb;
	color = lerp(color, rimColor.rgb, rim * rimColor.a * step(EPSILON, rimArea) * litRegion);
	color = lerp(color, highlightColor.rgb, specular * highlightColor.a * litRegion);
	
	result = lerp(color, shadowColor.rgb, (1.0 - litRegion) * shadowColor.a);
	#endif
}

#endif