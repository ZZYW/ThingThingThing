// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:1.014047,fgcg:0.9189249,fgcb:0.8194912,fgca:1,fgde:0.007,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:34290,y:32411,varname:node_9361,prsc:2|custl-6109-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:8068,x:32523,y:33466,varname:node_8068,prsc:2;n:type:ShaderForge.SFN_LightColor,id:3406,x:32431,y:32599,varname:node_3406,prsc:2;n:type:ShaderForge.SFN_LightVector,id:6869,x:31642,y:32528,varname:node_6869,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:9684,x:31642,y:32659,prsc:2,pt:True;n:type:ShaderForge.SFN_Dot,id:7782,x:31929,y:32563,cmnt:Lambert,varname:node_7782,prsc:2,dt:4|A-6869-OUT,B-9684-OUT;n:type:ShaderForge.SFN_Multiply,id:1941,x:32718,y:32633,cmnt:Diffuse Contribution,varname:node_1941,prsc:2|A-5749-RGB,B-3406-RGB;n:type:ShaderForge.SFN_Multiply,id:5085,x:33320,y:32840,cmnt:Attenuate and Color,varname:node_5085,prsc:2|A-1941-OUT,B-3883-OUT,C-2646-OUT;n:type:ShaderForge.SFN_AmbientLight,id:7528,x:32705,y:32409,varname:node_7528,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2460,x:33330,y:32330,cmnt:Ambient Light,varname:node_2460,prsc:2|A-5749-RGB,B-5153-OUT;n:type:ShaderForge.SFN_Step,id:1874,x:32735,y:33121,varname:node_1874,prsc:2|A-7272-OUT,B-8068-OUT;n:type:ShaderForge.SFN_Slider,id:7272,x:32366,y:33241,ptovrint:False,ptlb:Light Cutoff,ptin:_LightCutoff,varname:node_7272,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.33,max:1;n:type:ShaderForge.SFN_Multiply,id:2646,x:32982,y:33207,varname:node_2646,prsc:2|A-1874-OUT,B-8068-OUT;n:type:ShaderForge.SFN_If,id:3883,x:32685,y:32891,cmnt:TOONIFY,varname:node_3883,prsc:2|A-7782-OUT,B-3697-OUT,GT-4814-OUT,EQ-4814-OUT,LT-3790-OUT;n:type:ShaderForge.SFN_Slider,id:3697,x:31779,y:32995,ptovrint:False,ptlb:Color Sculpt,ptin:_ColorSculpt,varname:node_3697,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5193893,max:1;n:type:ShaderForge.SFN_Slider,id:2896,x:31779,y:33101,ptovrint:False,ptlb:Light Color Brightness,ptin:_LightColorBrightness,varname:node_2896,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:9875,x:31779,y:33193,ptovrint:False,ptlb:Dark Color Brightness,ptin:_DarkColorBrightness,varname:node_9875,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3932824,max:1;n:type:ShaderForge.SFN_Clamp,id:4814,x:32189,y:33013,varname:node_4814,prsc:2|IN-2896-OUT,MIN-3697-OUT,MAX-6236-OUT;n:type:ShaderForge.SFN_Clamp,id:3790,x:32189,y:33171,varname:node_3790,prsc:2|IN-9875-OUT,MIN-5171-OUT,MAX-3697-OUT;n:type:ShaderForge.SFN_Vector1,id:6236,x:31911,y:33275,varname:node_6236,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:5171,x:31911,y:33328,varname:node_5171,prsc:2,v1:0;n:type:ShaderForge.SFN_Add,id:6109,x:33779,y:32722,cmnt:diffuse plus ambient,varname:node_6109,prsc:2|A-2460-OUT,B-5085-OUT;n:type:ShaderForge.SFN_Color,id:5749,x:32331,y:32153,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_5749,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:1,c4:1;n:type:ShaderForge.SFN_Slider,id:9470,x:32847,y:32586,ptovrint:False,ptlb:Ambient Color Influence,ptin:_AmbientColorInfluence,varname:node_9470,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Vector4,id:2894,x:33004,y:32374,varname:node_2894,prsc:2,v1:1,v2:1,v3:1,v4:1;n:type:ShaderForge.SFN_Lerp,id:5153,x:33193,y:32492,varname:node_5153,prsc:2|A-2894-OUT,B-7528-RGB,T-9470-OUT;proporder:7272-3697-2896-9875-5749-9470;pass:END;sub:END;*/

Shader "THING" {
    Properties {
        _LightCutoff ("Light Cutoff", Range(0, 1)) = 0.33
        _ColorSculpt ("Color Sculpt", Range(0, 1)) = 0.5193893
        _LightColorBrightness ("Light Color Brightness", Range(0, 1)) = 1
        _DarkColorBrightness ("Dark Color Brightness", Range(0, 1)) = 0.3932824
        _Color ("Color", Color) = (0,0,1,1)
        _AmbientColorInfluence ("Ambient Color Influence", Range(0, 1)) = 0.5
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 3.0
            uniform float _LightCutoff;
            uniform float _ColorSculpt;
            uniform float _LightColorBrightness;
            uniform float _DarkColorBrightness;
            uniform float4 _Color;
            uniform float _AmbientColorInfluence;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
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
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_3883_if_leA = step(0.5*dot(lightDirection,normalDirection)+0.5,_ColorSculpt);
                float node_3883_if_leB = step(_ColorSculpt,0.5*dot(lightDirection,normalDirection)+0.5);
                float node_4814 = clamp(_LightColorBrightness,_ColorSculpt,1.0);
                float3 finalColor = ((_Color.rgb*lerp(float4(1,1,1,1),float4(UNITY_LIGHTMODEL_AMBIENT.rgb,0.0),_AmbientColorInfluence))+((_Color.rgb*_LightColor0.rgb)*lerp((node_3883_if_leA*clamp(_DarkColorBrightness,0.0,_ColorSculpt))+(node_3883_if_leB*node_4814),node_4814,node_3883_if_leA*node_3883_if_leB)*(step(_LightCutoff,attenuation)*attenuation))).rgb;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
            #pragma target 3.0
            uniform float _LightCutoff;
            uniform float _ColorSculpt;
            uniform float _LightColorBrightness;
            uniform float _DarkColorBrightness;
            uniform float4 _Color;
            uniform float _AmbientColorInfluence;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
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
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_3883_if_leA = step(0.5*dot(lightDirection,normalDirection)+0.5,_ColorSculpt);
                float node_3883_if_leB = step(_ColorSculpt,0.5*dot(lightDirection,normalDirection)+0.5);
                float node_4814 = clamp(_LightColorBrightness,_ColorSculpt,1.0);
                float3 finalColor = ((_Color.rgb*lerp(float4(1,1,1,1),float4(UNITY_LIGHTMODEL_AMBIENT.rgb,0.0),_AmbientColorInfluence))+((_Color.rgb*_LightColor0.rgb)*lerp((node_3883_if_leA*clamp(_DarkColorBrightness,0.0,_ColorSculpt))+(node_3883_if_leB*node_4814),node_4814,node_3883_if_leA*node_3883_if_leB)*(step(_LightCutoff,attenuation)*attenuation))).rgb;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 
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
