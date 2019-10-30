Shader "Hidden/Internal-DepthNormalsTexture" {
Properties {
	_MainTex ("", 2D) = "white" {}
	_Cutoff ("", Float) = 0.5
	_Color ("", Color) = (1,1,1,1)
}

SubShader {
	Tags { "RenderType"="Opaque" }
	Pass {
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
struct v2f {
    float4 pos : SV_POSITION;
    float4 nz : TEXCOORD0;
};
v2f vert( appdata_base v ) {
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.nz.xyz = COMPUTE_VIEW_NORMAL;
    o.nz.w = COMPUTE_DEPTH_01;
    return o;
}
fixed4 frag(v2f i) : SV_Target {
	return EncodeDepthNormal (i.nz.w, i.nz.xyz);
}
ENDCG
	}
}

SubShader {
	Tags { "RenderType"="TransparentCutout" }
	Pass {
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
struct v2f {
    float4 pos : SV_POSITION;
	float2 uv : TEXCOORD0;
    float4 nz : TEXCOORD1;
};
uniform float4 _MainTex_ST;
v2f vert( appdata_base v ) {
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
	o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
    o.nz.xyz = COMPUTE_VIEW_NORMAL;
    o.nz.w = COMPUTE_DEPTH_01;
    return o;
}
uniform sampler2D _MainTex;
uniform fixed _Cutoff;
uniform fixed4 _Color;
fixed4 frag(v2f i) : SV_Target {
	fixed4 texcol = tex2D( _MainTex, i.uv );
	clip( texcol.a*_Color.a - _Cutoff );
	return EncodeDepthNormal (i.nz.w, i.nz.xyz);
}
ENDCG
	}
}

SubShader {
	Tags { "RenderType"="TreeBark" }
	Pass {
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "UnityBuiltin3xTreeLibrary.cginc"
struct v2f {
    float4 pos : SV_POSITION;
    float2 uv : TEXCOORD0;
	float4 nz : TEXCOORD1;
};
v2f vert( appdata_full v ) {
    v2f o;
    TreeVertBark(v);
	
	o.pos = UnityObjectToClipPos(v.vertex);
	o.uv = v.texcoord.xy;
    o.nz.xyz = COMPUTE_VIEW_NORMAL;
    o.nz.w = COMPUTE_DEPTH_01;
    return o;
}
fixed4 frag( v2f i ) : SV_Target {
	return EncodeDepthNormal (i.nz.w, i.nz.xyz);
}
ENDCG
	}
}

SubShader {
	Tags { "RenderType"="TreeLeaf" }
	Pass {
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "UnityBuiltin3xTreeLibrary.cginc"
struct v2f {
    float4 pos : SV_POSITION;
    float2 uv : TEXCOORD0;
	float4 nz : TEXCOORD1;
};
v2f vert( appdata_full v ) {
    v2f o;
    TreeVertLeaf(v);
	
	o.pos = UnityObjectToClipPos(v.vertex);
	o.uv = v.texcoord.xy;
    o.nz.xyz = COMPUTE_VIEW_NORMAL;
    o.nz.w = COMPUTE_DEPTH_01;
    return o;
}
uniform sampler2D _MainTex;
uniform fixed _Cutoff;
fixed4 frag( v2f i ) : SV_Target {
	half alpha = tex2D(_MainTex, i.uv).a;

	clip (alpha - _Cutoff);
	return EncodeDepthNormal (i.nz.w, i.nz.xyz);
}
ENDCG
	}
}

SubShader {
	Tags { "RenderType"="TreeOpaque" "DisableBatching"="True" }
	Pass {
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
#include "TerrainEngine.cginc"
struct v2f {
	float4 pos : SV_POSITION;
	float4 nz : TEXCOORD0;
};
struct appdata {
    float4 vertex : POSITION;
    float3 normal : NORMAL;
    fixed4 color : COLOR;
};
v2f vert( appdata v ) {
	v2f o;
	TerrainAnimateTree(v.vertex, v.color.w);
	o.pos = UnityObjectToClipPos(v.vertex);
    o.nz.xyz = COMPUTE_VIEW_NORMAL;
    o.nz.w = COMPUTE_DEPTH_01;
	return o;
}
fixed4 frag(v2f i) : SV_Target {
	return EncodeDepthNormal (i.nz.w, i.nz.xyz);
}
ENDCG
	}
} 

SubShader {
	Tags { "RenderType"="TreeTransparentCutout" "DisableBatching"="True" }
	Pass {
		Cull Back
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
#include "TerrainEngine.cginc"

struct v2f {
	float4 pos : SV_POSITION;
	float2 uv : TEXCOORD0;
	float4 nz : TEXCOORD1;
};
struct appdata {
    float4 vertex : POSITION;
    float3 normal : NORMAL;
    fixed4 color : COLOR;
    float4 texcoord : TEXCOORD0;
};
v2f vert( appdata v ) {
	v2f o;
	TerrainAnimateTree(v.vertex, v.color.w);
	o.pos = UnityObjectToClipPos(v.vertex);
	o.uv = v.texcoord.xy;
    o.nz.xyz = COMPUTE_VIEW_NORMAL;
    o.nz.w = COMPUTE_DEPTH_01;
	return o;
}
uniform sampler2D _MainTex;
uniform fixed _Cutoff;
fixed4 frag(v2f i) : SV_Target {
	half alpha = tex2D(_MainTex, i.uv).a;

	clip (alpha - _Cutoff);
	return EncodeDepthNormal (i.nz.w, i.nz.xyz);
}
ENDCG
	}
	Pass {
		Cull Front
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
#include "TerrainEngine.cginc"

struct v2f {
	float4 pos : SV_POSITION;
	float2 uv : TEXCOORD0;
	float4 nz : TEXCOORD1;
};
struct appdata {
    float4 vertex : POSITION;
    float3 normal : NORMAL;
    fixed4 color : COLOR;
    float4 texcoord : TEXCOORD0;
};
v2f vert( appdata v ) {
	v2f o;
	TerrainAnimateTree(v.vertex, v.color.w);
	o.pos = UnityObjectToClipPos(v.vertex);
	o.uv = v.texcoord.xy;
    o.nz.xyz = -COMPUTE_VIEW_NORMAL;
    o.nz.w = COMPUTE_DEPTH_01;
	return o;
}
uniform sampler2D _MainTex;
uniform fixed _Cutoff;
fixed4 frag(v2f i) : SV_Target {
	fixed4 texcol = tex2D( _MainTex, i.uv );
	clip( texcol.a - _Cutoff );
	return EncodeDepthNormal (i.nz.w, i.nz.xyz);
}
ENDCG
	}

}

SubShader {
	Tags { "RenderType"="TreeBillboard" }
	Pass {
		Cull Off
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
#include "TerrainEngine.cginc"
struct v2f {
	float4 pos : SV_POSITION;
	float2 uv : TEXCOORD0;
	float4 nz : TEXCOORD1;
};
v2f vert (appdata_tree_billboard v) {
	v2f o;
	TerrainBillboardTree(v.vertex, v.texcoord1.xy, v.texcoord.y);
	o.pos = UnityObjectToClipPos(v.vertex);
	o.uv.x = v.texcoord.x;
	o.uv.y = v.texcoord.y > 0;
    o.nz.xyz = float3(0,0,1);
    o.nz.w = COMPUTE_DEPTH_01;
	return o;
}
uniform sampler2D _MainTex;
fixed4 frag(v2f i) : SV_Target {
	fixed4 texcol = tex2D( _MainTex, i.uv );
	clip( texcol.a - 0.001 );
	return EncodeDepthNormal (i.nz.w, i.nz.xyz);
}
ENDCG
	}
}

SubShader {
	Tags { "RenderType"="GrassBillboard" }
	Pass {
		Cull Off		
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
#include "TerrainEngine.cginc"

struct v2f {
	float4 pos : SV_POSITION;
	fixed4 color : COLOR;
	float2 uv : TEXCOORD0;
	float4 nz : TEXCOORD1;
};

v2f vert (appdata_full v) {
	v2f o;
	WavingGrassBillboardVert (v);
	o.color = v.color;
	o.pos = UnityObjectToClipPos(v.vertex);
	o.uv = v.texcoord.xy;
    o.nz.xyz = COMPUTE_VIEW_NORMAL;
    o.nz.w = COMPUTE_DEPTH_01;
	return o;
}
uniform sampler2D _MainTex;
uniform fixed _Cutoff;
fixed4 frag(v2f i) : SV_Target {
	fixed4 texcol = tex2D( _MainTex, i.uv );
	fixed alpha = texcol.a * i.color.a;
	clip( alpha - _Cutoff );
	return EncodeDepthNormal (i.nz.w, i.nz.xyz);
}
ENDCG
	}
}

SubShader {
	Tags { "RenderType"="Grass" }
	Pass {
		Cull Off
CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
#include "TerrainEngine.cginc"
struct v2f {
	float4 pos : SV_POSITION;
	fixed4 color : COLOR;
	float2 uv : TEXCOORD0;
	float4 nz : TEXCOORD1;
};

v2f vert (appdata_full v) {
	v2f o;
	WavingGrassVert (v);
	o.color = v.color;
	o.pos = UnityObjectToClipPos(v.vertex);
	o.uv = v.texcoord;
    o.nz.xyz = COMPUTE_VIEW_NORMAL;
    o.nz.w = COMPUTE_DEPTH_01;
	return o;
}
uniform sampler2D _MainTex;
uniform fixed _Cutoff;
fixed4 frag(v2f i) : SV_Target {
	fixed4 texcol = tex2D( _MainTex, i.uv );
	fixed alpha = texcol.a * i.color.a;
	clip( alpha - _Cutoff );
	return EncodeDepthNormal (i.nz.w, i.nz.xyz);
}
ENDCG
	}
}




//Wireframe/////////////////////////////////////////////////////////////////////////////////////
SubShader
{
	Tags{ "RenderType" = "Wireframe_Opaque" }
	
	Pass
	{
		CGPROGRAM                  
		#pragma vertex vert_surf    
	    #pragma fragment fragOpaque     
		#pragma target 3.0 
			  
		      
		#include "../cginc/Wireframe_DepthNormals.cginc"

		fixed4 fragOpaque(v2f_surf i) : SV_Target
		{
			return EncodeDepthNormal(i.nz.w, i.nz.xyz);
		}
		ENDCG   	
	}
}


SubShader
{
	Tags{ "RenderType" = "Wireframe_Full_TransparentCutout" }
	
	Pass
	{
		CGPROGRAM                  
		#pragma vertex vert_surf    
	    #pragma fragment frag     
		#pragma target 3.0 
			         

		#pragma shader_feature V_WIRE_SOURCE_BAKED V_WIRE_SOURCE_TEXTURE

		#pragma shader_feature V_WIRE_TRANSPARENCY_OFF V_WIRE_TRANSPARENCY_ON

		#pragma shader_feature V_WIRE_DYNAMIC_MASK_OFF V_WIRE_DYNAMI_MASK_PLANE V_WIRE_DYNAMIC_MASK_SPHERE V_WIRE_DYNAMIC_MASK_BOX 
		#pragma shader_feature V_WIRE_DYNAMIC_MASK_BASE_TEX_OFF V_WIRE_DYNAMIC_MASK_BASE_TEX_ON 
			  
			   
		#define V_WIRE_HAS_TEXTURE
		#define V_WIRE_CUTOUT
		      
		#include "../cginc/Wireframe_DepthNormals.cginc"
		ENDCG   	
	}
}

SubShader
{
	Tags{ "RenderType" = "Wireframe_WireOnly_TransparentCutout" }
	
	Pass
	{
		CGPROGRAM                  
		#pragma vertex vert_surf    
	    #pragma fragment frag     
		#pragma target 3.0 
			         

		#pragma shader_feature V_WIRE_SOURCE_BAKED V_WIRE_SOURCE_TEXTURE

		#pragma shader_feature V_WIRE_TRANSPARENCY_OFF V_WIRE_TRANSPARENCY_ON

		#pragma shader_feature V_WIRE_DYNAMIC_MASK_OFF V_WIRE_DYNAMI_MASK_PLANE V_WIRE_DYNAMIC_MASK_SPHERE V_WIRE_DYNAMIC_MASK_BOX 

			
		#define V_WIRE_CUTOUT 
		#define V_WIRE_CUTOUT_HALF
		#define V_WIRE_SAME_COLOR
		#define V_WIRE_NO_COLOR_BLACK
			  
			   
		#define V_WIRE_HAS_TEXTURE
		      
		#include "../cginc/Wireframe_DepthNormals.cginc"
		ENDCG   	
	}
}


SubShader 
{
	Tags{ "RenderType" = "Wireframe_Full_Geometry_TransparentCutout" }
	
	Pass
	{
		CGPROGRAM                   
		#pragma vertex vert_surf    
		#pragma geometry geom
	    #pragma fragment frag     
		#include "../cginc/Wireframe_GS_Platform.cginc"


		#pragma shader_feature V_WIRE_TRANSPARENCY_OFF V_WIRE_TRANSPARENCY_ON

		#pragma shader_feature V_WIRE_DYNAMIC_MASK_OFF V_WIRE_DYNAMI_MASK_PLANE V_WIRE_DYNAMIC_MASK_SPHERE V_WIRE_DYNAMIC_MASK_BOX 
		#pragma shader_feature V_WIRE_DYNAMIC_MASK_BASE_TEX_OFF V_WIRE_DYNAMIC_MASK_BASE_TEX_ON 
			  
			   
		#define V_WIRE_HAS_TEXTURE
		#define V_WIRE_CUTOUT
		      
		#include "../cginc/Wireframe_DepthNormals.cginc"
		#include "../cginc/Wireframe_GS.cginc"

		ENDCG   	
	}
}

SubShader
{
	Tags{ "RenderType" = "Wireframe_WireOnly_Geometry_TransparentCutout" }
	
	Pass
	{
		CGPROGRAM                  
		#pragma vertex vert_surf    
		#pragma geometry geom
	    #pragma fragment frag     
		#include "../cginc/Wireframe_GS_Platform.cginc"
			         

		#pragma shader_feature V_WIRE_TRANSPARENCY_OFF V_WIRE_TRANSPARENCY_ON

		#pragma shader_feature V_WIRE_DYNAMIC_MASK_OFF V_WIRE_DYNAMI_MASK_PLANE V_WIRE_DYNAMIC_MASK_SPHERE V_WIRE_DYNAMIC_MASK_BOX 

			
		#define V_WIRE_CUTOUT 
		#define V_WIRE_CUTOUT_HALF
		#define V_WIRE_SAME_COLOR
		#define V_WIRE_NO_COLOR_BLACK
			  
			   
		#define V_WIRE_HAS_TEXTURE
		      
		#include "../cginc/Wireframe_DepthNormals.cginc"
		#include "../cginc/Wireframe_GS.cginc"

		ENDCG   	
	}
}


SubShader 
{
	Tags{ "RenderType" = "Wireframe_Full_Tessellation_TransparentCutout" }
	
	Pass
	{
		CGPROGRAM                   
		#pragma vertex tessvert_surf 
		#pragma hull hs_surf
		#pragma domain ds_surf
		#pragma geometry geom
		#pragma fragment frag
		#pragma target 5.0 
		#include "Lighting.cginc"


		#pragma shader_feature V_WIRE_TRANSPARENCY_OFF V_WIRE_TRANSPARENCY_ON

		#pragma shader_feature V_WIRE_DYNAMIC_MASK_OFF V_WIRE_DYNAMI_MASK_PLANE V_WIRE_DYNAMIC_MASK_SPHERE V_WIRE_DYNAMIC_MASK_BOX 
		#pragma shader_feature V_WIRE_DYNAMIC_MASK_BASE_TEX_OFF V_WIRE_DYNAMIC_MASK_BASE_TEX_ON 
			  
			   
		#define V_WIRE_HAS_TEXTURE
		#define V_WIRE_CUTOUT

		#include "../cginc/Wireframe_DepthNormals.cginc"		

		#include "../cginc/Wireframe_GS.cginc"


		#pragma shader_feature _ V_WIRE_TESSELLATION_DISTANCE_BASED V_WIRE_TESSELLATION_EDGE_LENGTH
		#pragma shader_feature _ V_WIRE_TESSELLATION_NORMAL_RECONSTRUCT
		#include "../cginc/Wireframe_Tessellation.cginc"

		ENDCG   	 
	}
}

SubShader 
{
	Tags{ "RenderType" = "Wireframe_WireOnly_Tessellation_TransparentCutout" }
	
	Pass
	{
		CGPROGRAM                   
		#pragma vertex tessvert_surf 
		#pragma hull hs_surf
		#pragma domain ds_surf
		#pragma geometry geom
		#pragma fragment frag
		#pragma target 5.0 
		#include "Lighting.cginc"


		#pragma shader_feature V_WIRE_TRANSPARENCY_OFF V_WIRE_TRANSPARENCY_ON

		#pragma shader_feature V_WIRE_DYNAMIC_MASK_OFF V_WIRE_DYNAMI_MASK_PLANE V_WIRE_DYNAMIC_MASK_SPHERE V_WIRE_DYNAMIC_MASK_BOX 

			
		#define V_WIRE_CUTOUT 
		#define V_WIRE_CUTOUT_HALF
		#define V_WIRE_SAME_COLOR
		#define V_WIRE_NO_COLOR_BLACK
			  
			   
		#define V_WIRE_HAS_TEXTURE

		#include "../cginc/Wireframe_DepthNormals.cginc"		

		#include "../cginc/Wireframe_GS.cginc"


		#pragma shader_feature _ V_WIRE_TESSELLATION_DISTANCE_BASED V_WIRE_TESSELLATION_EDGE_LENGTH
		#pragma shader_feature _ V_WIRE_TESSELLATION_NORMAL_RECONSTRUCT
		#include "../cginc/Wireframe_Tessellation.cginc"

		ENDCG   	 
	}
}

//Wireframe/////////////////////////////////////////////////////////////////////////////////////



Fallback Off
}
 
