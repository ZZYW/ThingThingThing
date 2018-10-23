Shader "ThingThingThing/Sec" {
    Properties {
        [MaterialToggle] _VertexOffset ("VertexOffset", Float ) = 0
        _VertexOffsetIntensity ("Vertex Offset Intensity", Range(0, 0.4)) = 0
        _LightCutoff ("Light Cutoff", Range(0, 1)) = 0.33
        _ColorSculpt ("Color Sculpt", Range(0, 1)) = 0.5193893
        _LightColorBrightness ("Light Color Brightness", Range(0, 1)) = 1
        _DarkColorBrightness ("Dark Color Brightness", Range(0, 1)) = 0.3932824
        _Color ("Color", Color) = (0,0,1,1)
        _AmbientColorInfluence ("Ambient Color Influence", Range(0, 1)) = 0.5
        [MaterialToggle] _UseRainbowColors ("UseRainbowColors", Float ) = 0
        _rainbowcolor1 ("rainbow color 1", Color) = (1,0,0,1)
        _rainbowcolor2 ("rainbow color 2", Color) = (0.07586192,0,1,1)
        _rainbowcolor3 ("rainbow color 3", Color) = (0,1,0.006896496,1)
        [MaterialToggle] _UseVertexColor ("UseVertexColor", Float ) = 0
        _VertexOffset_UVMult ("VertexOffset_UVMult", Range(1, 49)) = 1
        _VertexOffsetTimeMulti ("VertexOffsetTimeMulti", Range(0, 1)) = 0
        _VertexOffsetSmoothFactor ("VertexOffsetSmoothFactor", Range(0, 0.376)) = 0

        //vertex displacement
        _Density("Density", Range(0,1)) = 0.5
        _VertexYDisplacement("VertexYDisplacement", Range(-30,30)) = 0.5
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
            //vertex glitch
            uniform fixed _Density;
            uniform fixed _VertexYDisplacement;

            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 vertexColor : COLOR;
            };

            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };

            float rand(float3 co)
            {
                return frac(sin( dot(co.xyz ,float3(12.9898,78.233,45.5432) )) * 43758.5453);
            }
                    
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0; 
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;

                //glitch vertex
                _Density += rand( float3(_Time.x, _Time.y, _Time.x));
                if(v.vertex.z % _Density < _Density / 2){
                    v.vertex.z += _VertexYDisplacement;
                }
                

                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            
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
                float3 _UseRainbowColors_var = lerp( lerp( _Color.rgb, i.vertexColor.rgb, _UseVertexColor ), lerp(lerp(_rainbowcolor1.rgb,_rainbowcolor2.rgb,node_55),lerp(_rainbowcolor2.rgb,_rainbowcolor3.rgb,node_55),node_55), _UseRainbowColors );
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
}
