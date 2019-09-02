using UnityEngine;

public partial class TOD_Sky : MonoBehaviour
{
    protected void OnEnable()
    {
        Components = GetComponent<TOD_Components>();

        if (!Components)
        {
            Debug.LogError("TOD_Components not found. Disabling script.");
            this.enabled = false;
            return;
        }

        Components.Initialize();
        Update();
    }

    protected void Update()
    {
        // If sun shafts are being used we have to assure that the alpha values in the render buffer are sane
        if (Components.SunShafts != null && Components.SunShafts.enabled)
        {
            if (!Components.ClearRenderer.enabled)
            {
                Components.ClearRenderer.enabled = true;
            }
        }
        else if (Components.ClearRenderer.enabled)
        {
            Components.ClearRenderer.enabled = false;
        }

        // The cycle should never have insane values
        Cycle.CheckRange();

        // Those parameters only need verification in editor mode
        #if UNITY_EDITOR
        Atmosphere.CheckRange();
        Stars.CheckRange();
        Day.CheckRange();
        Night.CheckRange();
        Light.CheckRange();
        Clouds.CheckRange();
        World.CheckRange();
        #endif

        // Setup child objects
        SetupQualitySettings();
        SetupSunAndMoon();
        SetupScattering();

        // Setup render settings
        if (World.SetFogColor)
        {
            var fogColor = SampleFogColor();

            #if UNITY_EDITOR
            if (RenderSettings.fogColor != fogColor)
            #endif
            {
                RenderSettings.fogColor = fogColor;
            }
        }
        if (World.SetAmbientLight)
        {
            #if UNITY_EDITOR
            if (RenderSettings.ambientLight != AmbientColor)
            #endif
            {
                RenderSettings.ambientLight = AmbientColor;
            }
        }

        // Precalculations
        Vector4 cloudUV = Components.Animation.CloudUV + Components.Animation.OffsetUV;

        // Setup global shader parameters
        {
            Shader.SetGlobalFloat("TOD_Gamma",         Gamma);
            Shader.SetGlobalFloat("TOD_OneOverGamma",  OneOverGamma);
            Shader.SetGlobalColor("TOD_LightColor",    LightColor);
            Shader.SetGlobalColor("TOD_CloudColor",    CloudColor);
            Shader.SetGlobalColor("TOD_SunColor",      SunColor);
            Shader.SetGlobalColor("TOD_MoonColor",     MoonColor);
            Shader.SetGlobalColor("TOD_AdditiveColor", AdditiveColor);
            Shader.SetGlobalColor("TOD_MoonHaloColor", MoonHaloColor);

            Shader.SetGlobalVector("TOD_SunDirection",   SunDirection);
            Shader.SetGlobalVector("TOD_MoonDirection",  MoonDirection);
            Shader.SetGlobalVector("TOD_LightDirection", LightDirection);

            Shader.SetGlobalVector("TOD_LocalSunDirection",
                                   Components.DomeTransform.InverseTransformDirection(SunDirection));
            Shader.SetGlobalVector("TOD_LocalMoonDirection",
                                   Components.DomeTransform.InverseTransformDirection(MoonDirection));
            Shader.SetGlobalVector("TOD_LocalLightDirection",
                                   Components.DomeTransform.InverseTransformDirection(LightDirection));
        }

        // Setup atmosphere shader
        if (Components.AtmosphereShader != null)
        {
            Components.AtmosphereShader.SetFloat("_Contrast",           Atmosphere.Contrast * OneOverGamma);
            Components.AtmosphereShader.SetFloat("_Haziness",           Atmosphere.Haziness);
            Components.AtmosphereShader.SetFloat("_Fogginess",          Atmosphere.Fogginess);
            Components.AtmosphereShader.SetFloat("_Horizon",            World.HorizonOffset);
            Components.AtmosphereShader.SetVector("_OpticalDepth",      opticalDepth);
            Components.AtmosphereShader.SetVector("_OneOverBeta",       oneOverBeta);
            Components.AtmosphereShader.SetVector("_BetaRayleigh",      betaRayleigh);
            Components.AtmosphereShader.SetVector("_BetaRayleighTheta", betaRayleighTheta);
            Components.AtmosphereShader.SetVector("_BetaMie",           betaMie);
            Components.AtmosphereShader.SetVector("_BetaMieTheta",      betaMieTheta);
            Components.AtmosphereShader.SetVector("_BetaMiePhase",      betaMiePhase);
            Components.AtmosphereShader.SetVector("_BetaNight",         betaNight);
        }

        // Setup cloud shader
        if (Components.CloudShader != null)
        {
            float sunGlow  = (1-Atmosphere.Fogginess) * LerpValue;
            float moonGlow = (1-Atmosphere.Fogginess) * 0.6f * (1 - Mathf.Abs(Cycle.MoonPhase));

            Components.CloudShader.SetFloat("_SunGlow",        sunGlow);
            Components.CloudShader.SetFloat("_MoonGlow",       moonGlow);
            Components.CloudShader.SetFloat("_CloudDensity",   Clouds.Density);
            Components.CloudShader.SetFloat("_CloudSharpness", Clouds.Sharpness);
            Components.CloudShader.SetVector("_CloudScale1",   Clouds.Scale1);
            Components.CloudShader.SetVector("_CloudScale2",   Clouds.Scale2);
            Components.CloudShader.SetVector("_CloudUV",       cloudUV);
        }

        // Setup space shader
        if (Components.SpaceShader != null)
        {
            var mainTextureScale = new Vector2(Stars.Tiling, Stars.Tiling);
            #if UNITY_EDITOR
            if (Components.SpaceShader.mainTextureScale != mainTextureScale)
            #endif
            {
                Components.SpaceShader.mainTextureScale = mainTextureScale;
            }
            Components.SpaceShader.SetFloat("_Subtract", 1-Mathf.Pow(Stars.Density, 0.1f));
        }

        // Setup sun shader
        if (Components.SunShader != null)
        {
            Components.SunShader.SetColor("_Color", Day.SunMeshColor * LerpValue * (1-Atmosphere.Fogginess));
        }

        // Setup moon shader
        if (Components.MoonShader != null)
        {
            Components.MoonShader.SetColor("_Color", Night.MoonMeshColor);
            Components.MoonShader.SetFloat("_Phase", Cycle.MoonPhase);
        }

        // Setup shadow shader
        if (Components.ShadowShader != null)
        {
            float shadowAlpha = Clouds.ShadowStrength * Mathf.Clamp01(1f - LightZenith / 90f);

            Components.ShadowShader.SetFloat("_Alpha",          shadowAlpha);
            Components.ShadowShader.SetFloat("_CloudDensity",   Clouds.Density);
            Components.ShadowShader.SetFloat("_CloudSharpness", Clouds.Sharpness);
            Components.ShadowShader.SetVector("_CloudScale1",   Clouds.Scale1);
            Components.ShadowShader.SetVector("_CloudScale2",   Clouds.Scale2);
            Components.ShadowShader.SetVector("_CloudUV",       cloudUV);
        }

        // Setup shadow projector
        if (Components.ShadowProjector != null)
        {
            var enabled          = Clouds.ShadowStrength != 0 && Components.ShadowShader != null;
            var farClipPlane     = Radius * 2;
            var orthographicSize = Radius;

            #if UNITY_EDITOR
            if (Components.ShadowProjector.enabled != enabled)
            #endif
            {
                Components.ShadowProjector.enabled = enabled;
            }

            #if UNITY_EDITOR
            if (Components.ShadowProjector.farClipPlane != farClipPlane)
            #endif
            {
                Components.ShadowProjector.farClipPlane = farClipPlane;
            }

            #if UNITY_EDITOR
            if (Components.ShadowProjector.orthographicSize != orthographicSize)
            #endif
            {
                Components.ShadowProjector.orthographicSize = orthographicSize;
            }
        }
    }
}
