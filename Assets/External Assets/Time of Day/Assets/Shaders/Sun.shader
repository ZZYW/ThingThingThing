// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Time of Day/Sun"
{
    Properties
    {
        _MainTex ("Alpha (A)", 2D) = "white" {}
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent-460"
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
            Blend OneMinusDstColor One

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            uniform float4 _Color;

            uniform sampler2D _MainTex;
            uniform float4 _MainTex_ST;

            struct v2f {
                float4 position   : POSITION;
                float2 uv_MainTex : TEXCOORD0;
            };

            v2f vert(appdata_base v) {
                v2f o;

                o.position   = UnityObjectToClipPos(v.vertex);
                o.uv_MainTex = TRANSFORM_TEX(v.texcoord, _MainTex);

                return o;
            }

            fixed4 frag(v2f i) : COLOR {
                fixed time = 1 + abs(0.25 * _SinTime.z);
                return tex2D(_MainTex, i.uv_MainTex).a * _Color.a * _Color * 2 * time;
            }

            ENDCG
        }
    }

    Fallback off
}
