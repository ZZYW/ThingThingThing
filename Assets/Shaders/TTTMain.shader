// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:True,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:1.065062,fgcg:0.9566504,fgcb:0.8224434,fgca:1,fgde:0.007,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:9361,x:34290,y:32411,varname:node_9361,prsc:2|custl-9928-OUT,voffset-5736-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:7039,x:32587,y:33530,varname:node_7039,prsc:2;n:type:ShaderForge.SFN_LightColor,id:8091,x:32495,y:32663,varname:node_8091,prsc:2;n:type:ShaderForge.SFN_LightVector,id:6368,x:31706,y:32592,varname:node_6368,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:4819,x:31706,y:32723,prsc:2,pt:True;n:type:ShaderForge.SFN_Dot,id:2226,x:31993,y:32627,cmnt:Lambert,varname:node_2226,prsc:2,dt:4|A-6368-OUT,B-4819-OUT;n:type:ShaderForge.SFN_Multiply,id:8660,x:32782,y:32697,cmnt:Diffuse Contribution,varname:node_8660,prsc:2|A-4722-OUT,B-8091-RGB;n:type:ShaderForge.SFN_Multiply,id:2310,x:33384,y:32904,cmnt:Attenuate and Color,varname:node_2310,prsc:2|A-8660-OUT,B-7044-OUT,C-5941-OUT;n:type:ShaderForge.SFN_AmbientLight,id:3222,x:32769,y:32473,varname:node_3222,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3043,x:33394,y:32394,cmnt:Ambient Light,varname:node_3043,prsc:2|A-4722-OUT,B-8573-OUT;n:type:ShaderForge.SFN_Step,id:7203,x:32896,y:33239,varname:node_7203,prsc:2|A-4485-OUT,B-7039-OUT;n:type:ShaderForge.SFN_Slider,id:4485,x:32455,y:33297,ptovrint:False,ptlb:Light Cutoff,ptin:_LightCutoff,varname:node_7272,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.33,max:1;n:type:ShaderForge.SFN_Multiply,id:5941,x:33149,y:33321,varname:node_5941,prsc:2|A-7203-OUT,B-7039-OUT;n:type:ShaderForge.SFN_If,id:7044,x:32749,y:32955,cmnt:TOONIFY,varname:node_7044,prsc:2|A-2226-OUT,B-537-OUT,GT-4160-OUT,EQ-4160-OUT,LT-789-OUT;n:type:ShaderForge.SFN_Slider,id:537,x:31843,y:33059,ptovrint:False,ptlb:Color Sculpt,ptin:_ColorSculpt,varname:node_3697,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5193893,max:1;n:type:ShaderForge.SFN_Slider,id:8529,x:31843,y:33165,ptovrint:False,ptlb:Light Color Brightness,ptin:_LightColorBrightness,varname:node_2896,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:638,x:31843,y:33257,ptovrint:False,ptlb:Dark Color Brightness,ptin:_DarkColorBrightness,varname:node_9875,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3932824,max:1;n:type:ShaderForge.SFN_Clamp,id:4160,x:32253,y:33077,varname:node_4160,prsc:2|IN-8529-OUT,MIN-537-OUT,MAX-6934-OUT;n:type:ShaderForge.SFN_Clamp,id:789,x:32253,y:33235,varname:node_789,prsc:2|IN-638-OUT,MIN-5224-OUT,MAX-537-OUT;n:type:ShaderForge.SFN_Vector1,id:6934,x:31975,y:33339,varname:node_6934,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:5224,x:31975,y:33392,varname:node_5224,prsc:2,v1:0;n:type:ShaderForge.SFN_Add,id:9928,x:33843,y:32786,cmnt:diffuse plus ambient,varname:node_9928,prsc:2|A-3043-OUT,B-2310-OUT;n:type:ShaderForge.SFN_Color,id:9985,x:31721,y:32197,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_5749,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:1,c4:1;n:type:ShaderForge.SFN_Slider,id:7891,x:32911,y:32650,ptovrint:False,ptlb:Ambient Color Influence,ptin:_AmbientColorInfluence,varname:node_9470,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Vector4,id:8488,x:33068,y:32438,varname:node_8488,prsc:2,v1:1,v2:1,v3:1,v4:1;n:type:ShaderForge.SFN_Lerp,id:8573,x:33257,y:32556,varname:node_8573,prsc:2|A-8488-OUT,B-3222-RGB,T-7891-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:4722,x:32484,y:32014,ptovrint:False,ptlb:UseRainbowColors,ptin:_UseRainbowColors,varname:node_6413,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-9985-RGB,B-2047-OUT;n:type:ShaderForge.SFN_Time,id:9272,x:31654,y:31849,varname:node_9272,prsc:2;n:type:ShaderForge.SFN_Sin,id:5497,x:32071,y:31789,varname:node_5497,prsc:2|IN-8762-OUT;n:type:ShaderForge.SFN_RemapRange,id:55,x:31989,y:31552,varname:node_55,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-5497-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:7894,x:31654,y:31693,varname:node_7894,prsc:2;n:type:ShaderForge.SFN_Add,id:8762,x:31888,y:31789,varname:node_8762,prsc:2|A-7894-Y,B-9272-T;n:type:ShaderForge.SFN_Lerp,id:7806,x:32168,y:31184,varname:node_7806,prsc:2|A-7178-RGB,B-241-RGB,T-55-OUT;n:type:ShaderForge.SFN_Lerp,id:9270,x:32168,y:31339,varname:node_9270,prsc:2|A-241-RGB,B-9856-RGB,T-55-OUT;n:type:ShaderForge.SFN_Lerp,id:2047,x:32460,y:31394,varname:node_2047,prsc:2|A-7806-OUT,B-9270-OUT,T-55-OUT;n:type:ShaderForge.SFN_Color,id:7178,x:31588,y:30837,ptovrint:False,ptlb:rainbow color 1,ptin:_rainbowcolor1,varname:node_4134,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:241,x:31449,y:30981,ptovrint:False,ptlb:rainbow color 2,ptin:_rainbowcolor2,varname:node_5621,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07586192,c2:0,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:9856,x:31498,y:31180,ptovrint:False,ptlb:rainbow color 3,ptin:_rainbowcolor3,varname:node_4861,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:0.006896496,c4:1;n:type:ShaderForge.SFN_Noise,id:1762,x:33961,y:33312,varname:node_1762,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:3088,x:33482,y:33121,varname:node_3088,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_SwitchProperty,id:5736,x:34092,y:32977,ptovrint:False,ptlb:VertexOffset,ptin:_VertexOffset,varname:node_4404,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-4924-OUT,B-7428-OUT;n:type:ShaderForge.SFN_Time,id:7614,x:33482,y:33331,varname:node_7614,prsc:2;n:type:ShaderForge.SFN_Add,id:2143,x:33742,y:33235,varname:node_2143,prsc:2|A-3088-UVOUT,B-7614-TSL;n:type:ShaderForge.SFN_Vector3,id:4924,x:33824,y:32965,varname:node_4924,prsc:2,v1:0,v2:0,v3:0;n:type:ShaderForge.SFN_Multiply,id:7428,x:34171,y:33355,varname:node_7428,prsc:2|A-1762-OUT,B-9355-OUT;n:type:ShaderForge.SFN_Slider,id:9355,x:33804,y:33488,ptovrint:False,ptlb:Vertex Offset Intensity,ptin:_VertexOffsetIntensity,varname:node_5869,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:5;proporder:5736-9355-4485-537-8529-638-9985-7891-4722-7178-241-9856;pass:END;sub:END;*/

Shader "ThingThingThing/Main" {
    Properties {
        [MaterialToggle] _VertexOffset ("VertexOffset", Float ) = 0
        _VertexOffsetIntensity ("Vertex Offset Intensity", Range(0, 5)) = 0
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x 
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
            uniform fixed _VertexOffset;
            uniform float _VertexOffsetIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float2 node_1762_skew = o.uv0 + 0.2127+o.uv0.x*0.3713*o.uv0.y;
                float2 node_1762_rnd = 4.789*sin(489.123*(node_1762_skew));
                float node_1762 = frac(node_1762_rnd.x*node_1762_rnd.y*(1+node_1762_skew.x));
                v.vertex.xyz += lerp( float3(0,0,0), (node_1762*_VertexOffsetIntensity), _VertexOffset );
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
                float4 node_9272 = _Time;
                float node_55 = (sin((i.posWorld.g+node_9272.g))*0.5+0.5);
                float3 _UseRainbowColors_var = lerp( _Color.rgb, lerp(lerp(_rainbowcolor1.rgb,_rainbowcolor2.rgb,node_55),lerp(_rainbowcolor2.rgb,_rainbowcolor3.rgb,node_55),node_55), _UseRainbowColors );
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x 
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
            uniform fixed _VertexOffset;
            uniform float _VertexOffsetIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float2 node_1762_skew = o.uv0 + 0.2127+o.uv0.x*0.3713*o.uv0.y;
                float2 node_1762_rnd = 4.789*sin(489.123*(node_1762_skew));
                float node_1762 = frac(node_1762_rnd.x*node_1762_rnd.y*(1+node_1762_skew.x));
                v.vertex.xyz += lerp( float3(0,0,0), (node_1762*_VertexOffsetIntensity), _VertexOffset );
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
                float4 node_9272 = _Time;
                float node_55 = (sin((i.posWorld.g+node_9272.g))*0.5+0.5);
                float3 _UseRainbowColors_var = lerp( _Color.rgb, lerp(lerp(_rainbowcolor1.rgb,_rainbowcolor2.rgb,node_55),lerp(_rainbowcolor2.rgb,_rainbowcolor3.rgb,node_55),node_55), _UseRainbowColors );
                float node_7044_if_leA = step(0.5*dot(lightDirection,normalDirection)+0.5,_ColorSculpt);
                float node_7044_if_leB = step(_ColorSculpt,0.5*dot(lightDirection,normalDirection)+0.5);
                float node_4160 = clamp(_LightColorBrightness,_ColorSculpt,1.0);
                float3 finalColor = ((_UseRainbowColors_var*lerp(float4(1,1,1,1),float4(UNITY_LIGHTMODEL_AMBIENT.rgb,0.0),_AmbientColorInfluence))+((_UseRainbowColors_var*_LightColor0.rgb)*lerp((node_7044_if_leA*clamp(_DarkColorBrightness,0.0,_ColorSculpt))+(node_7044_if_leB*node_4160),node_4160,node_7044_if_leA*node_7044_if_leB)*(step(_LightCutoff,attenuation)*attenuation))).rgb;
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x 
            #pragma target 3.0
            uniform fixed _VertexOffset;
            uniform float _VertexOffsetIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float2 node_1762_skew = o.uv0 + 0.2127+o.uv0.x*0.3713*o.uv0.y;
                float2 node_1762_rnd = 4.789*sin(489.123*(node_1762_skew));
                float node_1762 = frac(node_1762_rnd.x*node_1762_rnd.y*(1+node_1762_skew.x));
                v.vertex.xyz += lerp( float3(0,0,0), (node_1762*_VertexOffsetIntensity), _VertexOffset );
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
