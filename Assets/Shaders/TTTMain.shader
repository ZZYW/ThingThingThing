Shader "ThingThingThing/Main" {
    Properties {
        _LightCutoff ("Light Cutoff", Range(0, 1)) = 0.33
        _ColorSculpt ("Color Sculpt", Range(0, 1)) = 0.5193893
        _LightColorBrightness ("Light Color Brightness", Range(0, 1)) = 1
        _DarkColorBrightness ("Dark Color Brightness", Range(0, 1)) = 0.3932824
        _Color ("Color", Color) = (0,0,1,1)
        [MaterialToggle] _UseVertexColor ("Use Vertex Color Instead", Float) = 0
        _AmbientColorInfluence ("Ambient Color Influence", Range(0, 1)) = 0.5
        [MaterialToggle] _UseRainbowColors ("UseRainbowColors", Float ) = 0
        _rainbowcolor1 ("rainbow color 1", Color) = (1,0,0,1)
        _rainbowcolor2 ("rainbow color 2", Color) = (0.07586192,0,1,1)
        _rainbowcolor3 ("rainbow color 3", Color) = (0,1,0.006896496,1)
		
        
		//shader displacement and sampling rate
         [MaterialToggle] _VertexOffset ("Toggle Vertex Offset", Float ) = 1
		_Cutoff ("Glitch Density", Range(0, 1)) = 0.1
		_Multiplier_displacement ("Glitch Displacement", Float) = 0.5
		_Multiplier_time ("Glitch Frequency", Range(0, 20)) = 1

		[NoScaleOffset] _MainTex ("Texture", 2D) = "white" {} //texture
		_LerpScaler("Lerp between material color and texture color", Range(0, 1)) = 0
    }

    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE

            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
			//include random number library
			#include "ClassicNoise2D.hlsl"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0

            uniform float _LightCutoff;
            uniform float _ColorSculpt;
            uniform float _LightColorBrightness;
            uniform float _DarkColorBrightness;
            uniform float4 _Color;
            uniform float _AmbientColorInfluence;
            uniform fixed _UseRainbowColors;
            uniform float4 _rainbowcolor1;
            uniform float4 _rainbowcolor2;
            uniform float4 _rainbowcolor3;
            uniform fixed _UseVertexColor;
            struct VertexInput {
                
				float4 vertex : POSITION;
                float3 normal : NORMAL;
				float2 texcoord0 : TEXCOORD0; //texture input
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
				float2 uv0 : TEXCOORD0; //texure output
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
				
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };

		
			float _Cutoff;
			float _Multiplier_time;
			float _Multiplier_displacement;
			float _VertexOffset;

            VertexOutput vert (VertexInput v) {
				
                if(_VertexOffset==1){
                    float thresh = pnoise(v.vertex.xy + _Time.xx * _Multiplier_time, v.vertex.xy);
                    float displacement = pnoise(v.vertex.xy + _Time.xx * _Multiplier_time , v.vertex.xy);
                    if(thresh < _Cutoff){
                        //models'displayed along y axis
                        v.vertex.y += displacement * _Multiplier_displacement;
                    }
                }
			
			
                VertexOutput o = (VertexOutput)0;
				o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                //o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				float4 posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o);
				
                return o;
            }

			sampler2D _MainTex; //sample texture to this variable
			float _LerpScaler;
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
				
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float attenuation = LIGHT_ATTENUATION(i);
                float4 node_9272 = _Time;
                float node_55 = (sin((i.posWorld.g+node_9272.g))*0.5+0.5);

				fixed4 col = tex2D(_MainTex, i.uv0); //unpacking each pixel color
				fixed4 _Color_texture = lerp(_Color, col, _LerpScaler); //lerp between material and texture color

                float3 _UseRainbowColors_var = lerp( lerp( _Color_texture.rgb, i.vertexColor.rgb, _UseVertexColor ), lerp(lerp(_rainbowcolor1.rgb,_rainbowcolor2.rgb,node_55),lerp(_rainbowcolor2.rgb,_rainbowcolor3.rgb,node_55),node_55), _UseRainbowColors );
				float node_7044_if_leA = step(0.5*dot(lightDirection,normalDirection)+0.5,_ColorSculpt);
                float node_7044_if_leB = step(_ColorSculpt,0.5*dot(lightDirection,normalDirection)+0.5);
                float node_4160 = clamp(_LightColorBrightness,_ColorSculpt,1.0);
                float3 finalColor = ((_UseRainbowColors_var*lerp(float4(1,1,1,1),float4(UNITY_LIGHTMODEL_AMBIENT.rgb,0.0),_AmbientColorInfluence))+((_UseRainbowColors_var*_LightColor0.rgb)*lerp((node_7044_if_leA*clamp(_DarkColorBrightness,0.0,_ColorSculpt))+(node_7044_if_leB*node_4160),node_4160,node_7044_if_leA*node_7044_if_leB)*(step(_LightCutoff,attenuation)*attenuation))).rgb;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
				return finalRGBA;
            }
            ENDCG
        }
   
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {

				
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
