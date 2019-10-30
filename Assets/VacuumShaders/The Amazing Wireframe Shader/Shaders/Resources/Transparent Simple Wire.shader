Shader "Hidden/VacuumShaders/The Amazing Wireframe/Mobile/Vertex Lit/Transparent/Wire Only" 
{
	Properties 
	{   
		//Tag         
		[V_WIRE_Tag] _V_WIRE_Tag("", float) = 0 
		
		//Rendering Options
		[V_WIRE_RenderingOptions_VertexLit] _V_WIRE_RenderingOptions_VertexLitEnumID("", float) = 0
		 
		 		
		//Base 
		[HideInInspector] _Color("Color (RGB) Trans (A)", color) = (1, 1, 1, 1)
		[HideInInspector] _MainTex("Base (RGB) Trans (A)", 2D) = "white"{}		
				 
		 
		//Wire S Options  
		[V_WIRE_Title] _V_WIRE_Title_S_Options("Wire Source Options", float) = 0  		
		
		//Source
		[V_WIRE_Source] _V_WIRE_Source_Options ("", float) = 0
		[HideInInspector] _V_WIRE_SourceTex("", 2D) = "white"{}
		[HideInInspector] _V_WIRE_SourceTex_Scroll("", vector) = (0, 0, 0, 0)

		[HideInInspector] _V_WIRE_FixedSize("", float) = 0
		[HideInInspector] _V_WIRE_Size("", Float) = 1

		//Wire Options  
		[V_WIRE_Title] _V_WIRE_Title_W_Options("Wire Visual Options", float) = 0  	

		_V_WIRE_Color("Color", color) = (0, 0, 0, 1)
		_V_WIRE_WireTex("Color Texture (RGBA)", 2D) = "white"{}
		[V_WIRE_UVScroll] _V_WIRE_WireTex_Scroll("    ", vector) = (0, 0, 0, 0)
		[Enum(UV0,0,UV1,1)] _V_WIRE_WireTex_UVSet("    UV Set", float) = 0

		//Emission
		[V_WIRE_PositiveFloat]_V_WIRE_EmissionStrength("Emission Strength", float) = 0

		//Vertex Color
		[V_WIRE_Toggle] _V_WIRE_WireVertexColor("Vertex Color", Float) = 0

		//Light
		[V_WIRE_IncludeLight] _V_WIRE_IncludeLightEnumID ("", float) = 0

		//Transparency          
		[V_WIRE_Title]		  _V_WIRE_Transparency_M_Options("Wire Transparency Options", float) = 0  
		[V_WIRE_Transparency] _V_WIRE_TransparencyEnumID("", float) = 0 				
		[HideInInspector]	  _V_WIRE_TransparentTex_Invert("    ", float) = 0
		[HideInInspector]	  _V_WIRE_TransparentTex_Alpha_Offset("    ", Range(-1, 1)) = 0

		//Distance Fade  
	    [V_WIRE_DistanceFade]  _V_WIRE_DistanceFade ("Distance Fade", Float) = 0
		[HideInInspector] _V_WIRE_DistanceFadeStart("", Float) = 5
		[HideInInspector] _V_WIRE_DistanceFadeEnd("", Float) = 10 

		//Dynamic Mask
		[V_WIRE_Title]		 _V_WIRE_Title_M_Options("Dynamic Mask Options", float) = 0  
		[V_WIRE_DynamicMask] _V_WIRE_DynamicMaskEnumID("", float) = 0
		[HideInInspector]    _V_WIRE_DynamicMaskInvert("", float) = -1
		[HideInInspector]    _V_WIRE_DynamicMaskEffectsBaseTexEnumID("", int) = 0
		[HideInInspector]    _V_WIRE_DynamicMaskEffectsBaseTexInvert("", float) = 0	
		[HideInInspector]    _V_WIRE_DynamicMaskType("", Float) = 1
		[HideInInspector]    _V_WIRE_DynamicMaskSmooth("", Range(0, 1)) = 1

			[V_WIRE_Title]		 _V_WIRE_Title_UAR_Options("Unity Advanced Rendering Options", float) = 0
	} 
	    
	Category      
	{
		Tags { "Queue"="Transparent+1" 
		       "IgnoreProjector"="True"  
			   "RenderType"="Transparent"  
			 }    
		LOD 150 

		Blend SrcAlpha OneMinusSrcAlpha
	 
		SubShader  
		{			  
		 
			// Vertex Lit, emulated in shaders (4 lights max, no specular)
			Pass  
			{
				Tags { "LightMode" = "Vertex" }
				Lighting On 

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 3.0
				#pragma multi_compile_instancing // allow instanced shadow pass for most of the shaders


				#pragma multi_compile_fog


				#pragma shader_feature V_WIRE_SOURCE_BAKED V_WIRE_SOURCE_TEXTURE

				#ifdef UNITY_PASS_DEFERRED
					#define V_WIRE_LIGHT_ON
				#else
					#pragma shader_feature V_WIRE_LIGHT_OFF V_WIRE_LIGHT_ON
				#endif
				#pragma shader_feature V_WIRE_TRANSPARENCY_OFF V_WIRE_TRANSPARENCY_ON

				#pragma shader_feature V_WIRE_DYNAMIC_MASK_OFF V_WIRE_DYNAMI_MASK_PLANE V_WIRE_DYNAMIC_MASK_SPHERE V_WIRE_DYNAMIC_MASK_BOX 


				#define V_WIRE_TRANSPARENT
				#define V_WIRE_NO_COLOR_BLACK
				#define V_WIRE_SAME_COLOR

				#include "../cginc/Wireframe_VertexLit.cginc"
				
				ENDCG
			}
		 
			// Lightmapped
			Pass 
			{
				Tags { "LightMode" = "VertexLM" }

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 3.0
				#pragma multi_compile_instancing // allow instanced shadow pass for most of the shaders


				#pragma multi_compile_fog


				#pragma shader_feature V_WIRE_SOURCE_BAKED V_WIRE_SOURCE_TEXTURE

				#ifdef UNITY_PASS_DEFERRED
					#define V_WIRE_LIGHT_ON
				#else
					#pragma shader_feature V_WIRE_LIGHT_OFF V_WIRE_LIGHT_ON
				#endif
				#pragma shader_feature V_WIRE_TRANSPARENCY_OFF V_WIRE_TRANSPARENCY_ON

				#pragma shader_feature V_WIRE_DYNAMIC_MASK_OFF V_WIRE_DYNAMI_MASK_PLANE V_WIRE_DYNAMIC_MASK_SPHERE V_WIRE_DYNAMIC_MASK_BOX 


				#define V_WIRE_LIGHTMAP_ON
				#define V_WIRE_TRANSPARENT
				#define V_WIRE_NO_COLOR_BLACK
				#define V_WIRE_SAME_COLOR

				#include "../cginc/Wireframe_VertexLit.cginc"

				 
				ENDCG         
			}    
		     
			// Lightmapped, encoded as RGBM
			Pass 
	 		{
				Tags { "LightMode" = "VertexLMRGBM" }

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag 
				#pragma target 3.0
				#pragma multi_compile_instancing // allow instanced shadow pass for most of the shaders


				#pragma multi_compile_fog 


				#pragma shader_feature V_WIRE_SOURCE_BAKED V_WIRE_SOURCE_TEXTURE

				#ifdef UNITY_PASS_DEFERRED
					#define V_WIRE_LIGHT_ON
				#else
					#pragma shader_feature V_WIRE_LIGHT_OFF V_WIRE_LIGHT_ON
				#endif
				#pragma shader_feature V_WIRE_TRANSPARENCY_OFF V_WIRE_TRANSPARENCY_ON
				
				#pragma shader_feature V_WIRE_DYNAMIC_MASK_OFF V_WIRE_DYNAMI_MASK_PLANE V_WIRE_DYNAMIC_MASK_SPHERE V_WIRE_DYNAMIC_MASK_BOX 
			  

				#define V_WIRE_LIGHTMAP_ON
				#define V_WIRE_TRANSPARENT
				#define V_WIRE_NO_COLOR_BLACK
				#define V_WIRE_SAME_COLOR

				#include "../cginc/Wireframe_VertexLit.cginc"
				 
				ENDCG
			}  			
		}
	}

	FallBack Off
}
 
