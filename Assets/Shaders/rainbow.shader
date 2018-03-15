// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/rainbow" {
	Properties {
        [Toggle] _Rainbow ("Rainbow", int) = 0
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
        [Toggle] _HeightFog("Height Fog", int) = 1
        _HeightFogColor ("Height Fog Color", Color) = (1,1,1,1)
        _HeightFogStart ("Height Fog Start", Range(0,10)) = 1
        _HeightFogEnd ("Height Fog End", Range(0,10)) = 1
        _RainbowColor1("Rainbow Color 1", Color) = (1,1,1,1)
        _RainbowColor2("Rainbow Color 2", Color) = (1,1,1,1)
        _RainbowColor3("Rainbow Color 3", Color) = (1,1,1,1)
       
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows finalcolor:mycolor vertex:vert 

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			//float2 uv_MainTex;
            float3 normal;
            float4 vertex;
           
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

        fixed4 _HeightFogColor;
        float _HeightFogStart;
        float _HeightFogEnd;
        int _HeightFog;
        int _Rainbow;

        fixed4 _RainbowColor1;
        fixed4 _RainbowColor2;
        fixed4 _RainbowColor3;

        void vert (inout appdata_full v, out Input data) {       
          data.normal = v.normal;
          data.vertex = v.vertex;
        }

        void mycolor (Input IN, SurfaceOutputStandard o, inout fixed4 color){
          float3 worldPos = mul(unity_ObjectToWorld, IN.vertex).xyz;

          fixed4 finalColor = _Color;

        

          if(_Rainbow==1){
            float y = IN.vertex.y;
            if(y < 0.5){
            //0-0.5
                finalColor = lerp(_RainbowColor1, _RainbowColor2, y/0.5);
            }else{
            //0.5-1
                finalColor = lerp(_RainbowColor2, _RainbowColor3, (y-0.5)/0.5);
             }
          }


          if(_HeightFog == 0){
            color = finalColor;
          }else if(_HeightFog==1){
            float fogColorPer =  (worldPos.y - _HeightFogStart) / (_HeightFogStart-_HeightFogEnd);
            color = lerp(finalColor, _HeightFogColor, fogColorPer);
          }


        }


		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = _Color;
			o.Albedo = c;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
