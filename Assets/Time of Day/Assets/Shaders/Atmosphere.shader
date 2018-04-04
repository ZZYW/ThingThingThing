// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Time of Day/Atmosphere"
{
    SubShader
    {
        Tags
        {
            "Queue"="Transparent-470"
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

            uniform float3 TOD_LocalSunDirection;
            uniform float3 TOD_LocalMoonDirection;
            uniform float3 TOD_SunColor;
            uniform float3 TOD_MoonColor;
            uniform float4 TOD_AdditiveColor;
            uniform float3 TOD_CloudColor;
            uniform float4 TOD_MoonHaloColor;

            uniform float _Contrast;
            uniform float _Haziness;
            uniform float _Horizon;
            uniform float _Fogginess;
            uniform float2 _OpticalDepth;
            uniform float3 _OneOverBeta;
            uniform float3 _BetaRayleigh;
            uniform float3 _BetaRayleighTheta;
            uniform float3 _BetaMie;
            uniform float3 _BetaMieTheta;
            uniform float2 _BetaMiePhase;
            uniform float3 _BetaNight;

            inline float3 L(float3 viewdir, float3 sundir) {
                float3 res;

                // Angle between sun and viewdir
                float cosTheta = max(0, dot(viewdir, sundir));

                // Angular dependency
                // See [3] page 2 equation (2) and (4)
                float angular = (1 + cosTheta*cosTheta);

                // Rayleigh and mie scattering factors
                // See [3] page 2 equation (3) and (4)
                float3 betaTheta = _BetaRayleighTheta
                                 + _BetaMieTheta / pow(_BetaMiePhase.x - _BetaMiePhase.y * cosTheta, 1.5);

                // Scattering solution
                // See [5] page 11
                res = angular * betaTheta * _OneOverBeta;

                return res;
            }

            inline float3 L() {
                return _BetaNight;
            }

            inline float3 T(float height) {
                float3 res;

                // Parameter value
                // See [7] page 70 equation (5.7)
                float h = clamp(height + _Horizon, 0.001, 1);
                float f = pow(h, _Haziness);

                // Optical depth integral approximation
                // See [7] page 71 equation (5.8)
                // See [7] page 71 equation (5.10)
                // See [7] page 76 equation (6.1)
                float sh = (1 - f) * 190000;
                float sr = sh + f * (_OpticalDepth.x - sh);
                float sm = sh + f * (_OpticalDepth.y - sh);

                // Rayleigh and mie scattering factors
                // See [3] page 2 equation (1) and (2)
                float3 beta = _BetaRayleigh * sr
                            + _BetaMie * sm;

                // Scattering solution
                // See [5] page 11
                res = exp(-beta);

                return res;
            }

            struct v2f {
                float4 position : POSITION;
                fixed4 color    : COLOR;
            };

            v2f vert(appdata_base v) {
                v2f o;

                // Scattering values
                float3 T_val  = T(v.normal.y);
                float3 E_sun  = TOD_SunColor;
                float3 E_moon = TOD_MoonColor;
                float3 L_sun  = L(-v.normal, TOD_LocalSunDirection);
                float3 L_moon = L();

                // Add scattering color
                o.color.rgb = (1-T_val) * (E_sun*L_sun + E_moon*L_moon);
                o.color.a   = 10 * max(max(o.color.r, o.color.g), o.color.b);

                // Add simple moon halo
                o.color += TOD_MoonHaloColor * pow(max(0, dot(TOD_LocalMoonDirection, -v.normal)), 10);

                // Add additive color
                o.color += TOD_AdditiveColor;

                // Add fog color
                o.color.rgb = lerp(o.color.rgb, TOD_CloudColor, _Fogginess);
                o.color.a += _Fogginess;

                // Clamp alpha to [0, 1]
                o.color.a = saturate(o.color.a);

                // Adjust output color according to gamma value
                o.color = pow(o.color, _Contrast);

                // Write position
                o.position = UnityObjectToClipPos(v.vertex);

                return o;
            }

            fixed4 frag(v2f i) : COLOR {
                return i.color;
            }

            ENDCG
        }
    }

    Fallback off
}
