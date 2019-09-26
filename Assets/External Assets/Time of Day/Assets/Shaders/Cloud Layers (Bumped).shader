// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Time of Day/Cloud Layers (Bumped)"
{
    Properties
    {
        _NoiseTexture1 ("Noise Texture 1 (A)", 2D) = "white" {}
        _NoiseTexture2 ("Noise Texture 2 (A)", 2D) = "white" {}
        _NoiseNormals1 ("Noise Normals 1",     2D) = "bump"  {}
        _NoiseNormals2 ("Noise Normals 2",     2D) = "bump"  {}
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
            uniform float3 TOD_LocalLightDirection;

            uniform float _SunGlow;
            uniform float _MoonGlow;
            uniform float _CloudDensity;
            uniform float _CloudSharpness;
            uniform float2 _CloudScale1;
            uniform float2 _CloudScale2;
            uniform float4 _CloudUV;

            uniform sampler2D _NoiseTexture1;
            uniform sampler2D _NoiseTexture2;
            uniform sampler2D _NoiseNormals1;
            uniform sampler2D _NoiseNormals2;

            struct v2f {
                float4 position : POSITION;
                fixed3 color    : COLOR;
                float4 cloudUV  : TEXCOORD0;
                float3 lightdir : TEXCOORD1;
                float3 params   : TEXCOORD2; // density, glow, gammahalf
            };

            v2f vert(appdata_tan v) {
                v2f o;

                // Vertex position and uv coordinates
                float3 vertnorm = normalize(v.vertex.xyz);
                float2 vertuv   = vertnorm.xz / pow(vertnorm.y + 0.1, 0.75);
                float  vertfade = saturate(100 * vertnorm.y * vertnorm.y);

                // Light source directions
                float3 lightvec = -TOD_LocalLightDirection;
                float3 sunvec   = -TOD_LocalSunDirection;
                float3 moonvec  = -TOD_LocalMoonDirection;

                // Write results
                o.position   = UnityObjectToClipPos(v.vertex);
                o.color      = pow(TOD_CloudColor, TOD_OneOverGamma);
                o.cloudUV.xy = (vertuv + _CloudUV.xy) / _CloudScale1;
                o.cloudUV.zw = (vertuv + _CloudUV.zw) / _CloudScale2;
                o.params.x   = _CloudDensity * vertfade;
                o.params.y   = pow(_MoonGlow * max(0, dot(v.normal, moonvec))
                                   +_SunGlow * max(0, dot(v.normal, sunvec)), 10)
                             * TOD_OneOverGamma * 0.5;
                o.params.z   = TOD_OneOverGamma * 0.5;

                TANGENT_SPACE_ROTATION;
                o.lightdir = mul(rotation, lightvec);

                return o;
            }

            fixed4 frag(v2f i) : COLOR {
                fixed4 color = fixed4(i.color, 1);

                // Sample texture
                fixed noise1 = tex2D(_NoiseTexture1, i.cloudUV.xy).a;
                fixed noise2 = tex2D(_NoiseTexture2, i.cloudUV.zw).a;
                fixed a = pow(noise1 * noise2, _CloudSharpness) * i.params.x;
                fixed d = 0.1 + 1 / exp(0.2 * a);

                // Apply texture
                color.a = saturate(a);

                // Sample normals
                fixed4 noiseNormal1 = tex2D(_NoiseNormals1, i.cloudUV.xy);
                fixed4 noiseNormal2 = tex2D(_NoiseNormals2, i.cloudUV.zw);
                fixed3 cloud_normal = UnpackNormal(0.5 * (noiseNormal1 + noiseNormal2));
                fixed cloud_shading = (1 + dot(cloud_normal, i.lightdir)) / 2; // [0, 1]

                // Apply glow based shading
                color.rgb += i.params.y * saturate(1.5-a);

                // Apply normal map based shading
                color.rgb *= 0.1 + pow(cloud_shading, i.params.z);

                // Apply density based shading
                color.rgb *= d;

                return color;
            }

            ENDCG
        }
    }

    Fallback off
}
