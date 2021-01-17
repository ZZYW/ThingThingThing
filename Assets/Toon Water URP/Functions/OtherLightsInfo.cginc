void OtherLights_half(half4 SpecColor, half SpecularPower, half SpecularCutoff, half SpecularTolerance, half3 WorldPosition, half3 WorldNormal, half3 WorldView, out half3 Specular)
{
   half3 specularColor = 0;
 
#ifndef SHADERGRAPH_PREVIEW
   WorldNormal = normalize(WorldNormal);
   WorldView = SafeNormalize(WorldView);
   
   int pixelLightCount = GetAdditionalLightsCount();
   
   for (int i = 0; i < pixelLightCount; i++)
   {
       Light light = GetAdditionalLight(i, WorldPosition);
       half3 attenuatedLightColor = light.color * (light.distanceAttenuation * light.shadowAttenuation); 

	   half3 dirView = normalize(light.direction + WorldView);  
	   
	   half NdotV = saturate(dot(WorldNormal, dirView));
	   NdotV = pow(NdotV, SpecularPower * 3);
	   NdotV = smoothstep(SpecularCutoff - SpecularTolerance, SpecularCutoff + SpecularTolerance, NdotV);

	   specularColor += NdotV * (SpecColor.rgb * attenuatedLightColor);
   }
#endif
 
   specularColor = lerp(half3(0, 0, 0), specularColor, SpecColor.a);
   Specular = specularColor;
}