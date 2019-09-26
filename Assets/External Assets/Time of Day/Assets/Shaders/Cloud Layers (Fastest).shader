// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Time of Day/Cloud Layers (Fastest)"
{
    Properties
    {
        _NoiseTexture ("Noise Texture (A)", 2D) = "white" {}
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent-450"
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
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            uniform float  TOD_OneOverGamma;
            uniform float3 TOD_CloudColor;
            uniform float3 TOD_LocalSunDirection;
            uniform float3 TOD_LocalMoonDirection;

            uniform float _SunGlow;
            uniform float _MoonGlow;
            uniform float _CloudDensity;
            uniform float _CloudSharpness;
            uniform float2 _CloudScale1;
            uniform float2 _CloudScale2;
            uniform float4 _CloudUV;

            uniform sampler2D _NoiseTexture;

            struct v2f {
                float4 position : POSITION;
                fixed3 color    : COLOR;
                float2 cloudUV  : TEXCOORD0;
                float3 params   : TEXCOORD1; // density, glow, shape
            };

            v2f vert(appdata_base v) {
                v2f o;

                // Vertex position and uv coordinates
                float3 vertnorm = normalize(v.vertex.xyz);
                float2 vertuv   = vertnorm.xz / pow(vertnorm.y + 0.1, 0.75);
                float  vertfade = saturate(100 * vertnorm.y * vertnorm.y);

                // Light source directions
                float3 sunvec   = -TOD_LocalSunDirection;
                float3 moonvec  = -TOD_LocalMoonDirection;

                // Write results
                o.position = UnityObjectToClipPos(v.vertex);
                o.color    = pow(TOD_CloudColor, TOD_OneOverGamma);
                o.cloudUV  = (vertuv + _CloudUV.xy) / _CloudScale1;
                o.params.x = _CloudDensity * vertfade;
                o.params.y = pow(_MoonGlow * max(0, dot(v.normal, moonvec))
                                 +_SunGlow * max(0, dot(v.normal, sunvec)), 10);
                o.params.z = _CloudSharpness * 0.15 - max(0, 1-_CloudSharpness) * 0.3;

                return o;
            }

            fixed4 frag(v2f i) : COLOR {
                fixed4 color = fixed4(i.color, 1);

                // Sample texture
                fixed noise1 = tex2D(_NoiseTexture, i.cloudUV.xy).a;
                fixed a = (noise1 - i.params.z) * i.params.x;
                fixed d = 0.1 * a;

                // Apply texture
                color.a = saturate(a);

                // Apply glow based shading
                color.rgb += i.params.y * saturate(1.5-a);

                // Apply density based shading
                color.rgb -= d;

                return color;
            }

            ENDCG
        }
    }

    Fallback off
}
