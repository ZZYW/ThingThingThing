Shader "Hidden/VacuumShaders/The Amazing Wireframe/ColorMask0"
{
	SubShader 
	{
	   
		//PassName "BASE" 
		Pass  
		{
			Name "BASE"  
			      
			ZWrite On
			ColorMask 0
		 

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag  
			#pragma multi_compile_instancing // allow instanced shadow pass for most of the shaders
			#include "UnityCG.cginc" 

			#include "../cginc/Wireframe_ColorMask0.cginc"   

			ENDCG   
		} //Pass

	} //SubShader

} //Shader
