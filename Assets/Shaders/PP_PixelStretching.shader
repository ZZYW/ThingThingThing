// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:1,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:False,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:1,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:6,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:1,qpre:4,rntp:5,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:1.065062,fgcg:0.9566504,fgcb:0.8224434,fgca:1,fgde:0.007,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32740,y:33254,varname:node_2865,prsc:2|emission-7542-RGB;n:type:ShaderForge.SFN_TexCoord,id:4219,x:31415,y:33089,cmnt:Default coordinates,varname:node_4219,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2dAsset,id:4430,x:31860,y:33426,ptovrint:False,ptlb:MainTex,ptin:_MainTex,cmnt:MainTex contains the color of the scene,varname:node_9933,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7542,x:32278,y:33333,varname:node_1672,prsc:2,ntxv:0,isnm:False|UVIN-2166-OUT,TEX-4430-TEX;n:type:ShaderForge.SFN_Clamp,id:6695,x:32291,y:32760,varname:node_6695,prsc:2|IN-4219-U,MIN-5483-OUT,MAX-8020-OUT;n:type:ShaderForge.SFN_Slider,id:5483,x:31614,y:32599,ptovrint:False,ptlb:x,ptin:_x,varname:node_5483,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:0.499;n:type:ShaderForge.SFN_Slider,id:8505,x:31528,y:32819,ptovrint:False,ptlb:y,ptin:_y,varname:node_8505,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:0.499;n:type:ShaderForge.SFN_Clamp,id:3950,x:32252,y:33026,varname:node_3950,prsc:2|IN-4219-V,MIN-8505-OUT,MAX-4984-OUT;n:type:ShaderForge.SFN_Append,id:2166,x:32509,y:32941,varname:node_2166,prsc:2|A-6695-OUT,B-3950-OUT;n:type:ShaderForge.SFN_OneMinus,id:8020,x:32104,y:32851,varname:node_8020,prsc:2|IN-5483-OUT;n:type:ShaderForge.SFN_OneMinus,id:4984,x:32023,y:33076,varname:node_4984,prsc:2|IN-8505-OUT;proporder:4430-5483-8505;pass:END;sub:END;*/

Shader "ThingThingThing/Post Processing/pixelStretching" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _x ("x", Range(0, 0.499)) = 0
        _y ("y", Range(0, 0.499)) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Overlay+1"
            "RenderType"="Overlay"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZTest Always
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _x;
            uniform float _y;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float2 node_2166 = float2(clamp(i.uv0.r,_x,(1.0 - _x)),clamp(i.uv0.g,_y,(1.0 - _y)));
                float4 node_1672 = tex2D(_MainTex,TRANSFORM_TEX(node_2166, _MainTex));
                float3 emissive = node_1672.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
