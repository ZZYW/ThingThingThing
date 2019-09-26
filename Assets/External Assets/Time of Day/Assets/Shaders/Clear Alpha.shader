// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Time of Day/Clear Alpha"
{
    SubShader
    {
        Tags
        {
            "Queue"="Transparent-455"
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
            ColorMask A

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f {
                float4 position : POSITION;
            };

            v2f vert(appdata_tan v) {
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : COLOR {
                return fixed4(0, 0, 0, 0);
            }

            ENDCG
        }
    }

    Fallback off
}
