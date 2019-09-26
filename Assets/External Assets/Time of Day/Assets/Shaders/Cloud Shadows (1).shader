// Upgrade NOTE: replaced '_Projector' with 'unity_Projector'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Time of Day/Cloud Shadows (1)"
{
    Properties
    {
        _NoiseTexture ("Noise Texture (A)", 2D) = "white" {}
    }

    SubShader
    {
        Pass
        {
            Offset -1, -1
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            uniform float3 TOD_LocalLightDirection;

            uniform float _Alpha;
            uniform float _CloudDensity;
            uniform float _CloudSharpness;
            uniform float2 _CloudScale1;
            uniform float2 _CloudScale2;
            uniform float4 _CloudUV;

            uniform sampler2D _NoiseTexture;
            uniform float4x4 unity_Projector;

            struct v2f {
                float4 pos     : POSITION;
                float4 cloudUV : TEXCOORD0;
                float  shape   : TEXCOORD1;
            };

            v2f vert(appdata_base v)
            {
                v2f o;

                float3 vertnorm = -TOD_LocalLightDirection;
                float2 vertuv   = vertnorm.xz / pow(vertnorm.y + 0.1, 0.75);

                float4 projPos  = mul(unity_Projector, v.vertex);
                float2 uvoffset = 0.5 + projPos.xy / projPos.w;

                o.pos        = UnityObjectToClipPos(v.vertex);
                o.cloudUV.xy = uvoffset + (vertuv + _CloudUV.xy) / _CloudScale1;
                o.cloudUV.zw = uvoffset + (vertuv + _CloudUV.zw) / _CloudScale2;
                o.shape      = _CloudSharpness * 0.15 - max(0, 1-_CloudSharpness) * 0.3;

                return o;
            }

            fixed4 frag(v2f i) : COLOR
            {
                fixed alpha1 = tex2D(_NoiseTexture, i.cloudUV.xy).a;
                fixed alpha2 = tex2D(_NoiseTexture, i.cloudUV.zw).a;
                fixed alpha = (alpha1 * alpha2) - i.shape * _CloudDensity;

                return fixed4(0, 0, 0, saturate(alpha) * _Alpha);
            }

            ENDCG
        }
    }

    Fallback off
}