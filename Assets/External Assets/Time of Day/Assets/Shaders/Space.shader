// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Time of Day/Space"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "black" {}
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent-490"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
        }

        Fog
        {
            Mode Off
        }

        Pass
        {
            Cull Front
            ZWrite Off
            ZTest LEqual

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            uniform float _Subtract;

            uniform sampler2D _MainTex;
            uniform float4 _MainTex_ST;

            struct v2f {
                float4 position   : POSITION;
                fixed4 color      : COLOR;
                float2 uv_MainTex : TEXCOORD0;
            };

            v2f vert(appdata_base v) {
                v2f o;

                float3 vertnorm = normalize(v.vertex.xyz);
                float2 starUV = vertnorm.xz / (vertnorm.y + 1);

                o.position = UnityObjectToClipPos(v.vertex);
                o.uv_MainTex = TRANSFORM_TEX(starUV, _MainTex);

                return o;
            }

            fixed4 frag(v2f i) : COLOR {
                return fixed4(saturate(tex2D(_MainTex, i.uv_MainTex).rgb - _Subtract), 1);
            }

            ENDCG
        }
    }

    Fallback off
}
