// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Time of Day/Moon"
{
    Properties
    {
        _MainTex  ("Base (RGB)", 2D)    = "white" {}
        _Contrast ("Contrast",   float) = 0.75
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent-480"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
        }

        Fog
        {
            Mode Off
        }

        Pass
        {
            Cull Back
            ZWrite Off
            ZTest LEqual

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            uniform float3 _Color;
            uniform float _Phase;
            uniform float _Contrast;

            uniform sampler2D _MainTex;
            uniform float4 _MainTex_ST;

            struct v2f {
                float4 position   : POSITION;
                fixed4 color      : COLOR;
                float2 uv_MainTex : TEXCOORD0;
                float3 normal     : TEXCOORD1;
                float3 shadedir   : TEXCOORD2;
                float  shading    : TEXCOORD3;
            };

            v2f vert(appdata_base v) {
                v2f o;

                float phaseabs = abs(_Phase);
                float3 offset = float3(_Phase, -phaseabs, -phaseabs) * 5;

                float3 viewdir = normalize(ObjSpaceViewDir(v.vertex));

                o.position   = UnityObjectToClipPos(v.vertex);
                o.uv_MainTex = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.normal     = v.normal;
                o.shadedir   = normalize(viewdir + offset);
                o.shading    = 2 * (1-phaseabs);

                return o;
            }

            fixed4 frag(v2f i) : COLOR {
                fixed3 color = _Color;

                fixed shading = i.shading * max(0, dot(i.normal, i.shadedir));

                fixed3 moontex = tex2D(_MainTex, i.uv_MainTex).rgb;
                color *= moontex * shading;

                return fixed4(pow(color, _Contrast), 1);
            }

            ENDCG
        }
    }

    Fallback off
}
