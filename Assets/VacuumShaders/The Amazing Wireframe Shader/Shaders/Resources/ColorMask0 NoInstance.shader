Shader "Hidden/VacuumShaders/The Amazing Wireframe/ColorMask0 NoInstance"
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
			#include "UnityCG.cginc"    

			#include "../cginc/Wireframe_ColorMask0.cginc"   

			ENDCG   
		} //Pass

	} //SubShader

} //Shader
   
