// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/rainbow" {
	Properties {
        [Toggle] _Rainbow ("Rainbow", int) = 0

		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
        
        [Toggle] _HeightFog("Height Fog", int) = 0
        _HeightFogColor ("Height Fog Color", Color) = (1,1,1,1)
        _HeightFogStart ("Height Fog Start", Range(-20,20)) = 1
        _HeightFogEnd ("Height Fog End", Range(-20,20)) = 1

        _RainbowColor1("Rainbow Color 1", Color) = (1,1,1,1)
        _RainbowColor2("Rainbow Color 2", Color) = (1,1,1,1)
        _RainbowColor3("Rainbow Color 3", Color) = (1,1,1,1)

        _TimeOfffset("Time Offset", Float) = 0.0
        _TimeScale("Time Elapse Speed", Float) = 30
        _NoiseScaler("Noise Scaler", Float) = 0
       
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
        //===========

		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows  vertex:vert finalcolor:mycolor
        #include "ClassicNoise3D.hlsl"
		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

        struct Input {
            float3 viewDir;
            float4 vertex;
            float2 uv_MainTex;
            float3 worldPos;
        };


		sampler2D _MainTex;      
		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
       
        fixed4 _HeightFogColor;
        float _HeightFogStart;
        float _HeightFogEnd;
        int _HeightFog;
        int _Rainbow;
        float _TimeOfffset;
        float _TimeScale;

        fixed4 _RainbowColor1;
        fixed4 _RainbowColor2;
        fixed4 _RainbowColor3;

        float _NoiseScaler;


        void vert (inout appdata_full v, out Input o) {
          //do something
          UNITY_INITIALIZE_OUTPUT(Input,o);
          o.vertex = v.vertex;
        }

        //do height fog
        void mycolor (Input IN, SurfaceOutputStandard o, inout fixed4 color){
          float3 worldPos =  IN.worldPos; //mul(unity_ObjectToWorld, IN.vertex).xyz;
          fixed4 c = color;
          if(_HeightFog == 0){
            color = c;
          }else if(_HeightFog==1){
            float fogColorPer =  (worldPos.y - _HeightFogStart) / (_HeightFogStart-_HeightFogEnd);
            color = lerp(c, _HeightFogColor, fogColorPer);
          }

        }
       

		UNITY_INSTANCING_CBUFFER_START(Props)
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {

			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            
            float y = IN.vertex.y;

            if(_Rainbow==1)
            {
                c *= lerp(  lerp(_RainbowColor1, 
                                    _RainbowColor2, (sin( (_Time.x + _TimeOfffset) *_TimeScale)+1) / 2), 
                                    lerp(_RainbowColor2, _RainbowColor3, (sin((_Time.x + _TimeOfffset)*_TimeScale)+1) / 2), 
                                    y);
            }
             
           

			o.Albedo = c;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}

        //===========
		ENDCG
	}
	FallBack "Diffuse"
}
