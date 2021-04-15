using System;
using UnityEngine; 
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[ExecuteAlways]
public class PlanarReflectionsRenderer : MonoBehaviour
{
    /// <summary>
    /// Global Vars
    /// </summary>

    [Tooltip("Select the resolution of reflection texture relative to screen resolution. Full means the best reflection quality but the worst performance. You should keep this setting set to Quarter or Third to get the best performance.")]
    public ResolutionScales ResolutionScale = ResolutionScales.Half;
    
    [Tooltip("Skip every n frame to gain performance. For example this setting set to EverySecondFrame will make refelction render with half the fps of your game. However setting to anything over NoSkipFrame creates effect where objects are a bit off their position when changing camera position/rotation.")]
    public SkipFrames SkipEveryNFrame = SkipFrames.NoSkipFrame;

    [Tooltip("Always make sure water layer is deselected (and your water plane is assigned to water layer)! Which layers of objects should we relfect? Usefull to skip in rendering f.e. small obejcts and make rendering reflections faster.")]
    public LayerMask LayersToReflect = -1;

    [Tooltip("Render objects in reflection texture with shadows? Usually you want to turn it off because it's hard to notice that there is lack of shadows in reflections.")]
    public bool RenderWithShadows = false;

    [Tooltip("Select to use occlusion culling when rendering objects to reflection texture. If you see some artifacts disable this option.")]
    public bool RenderWithOcclusionCulling = true;
   
    [Tooltip("Water plane")]
    public GameObject WaterPlane;

    [Tooltip("Offset reflection rendering to fix artifacts at geometry borders")]
    public float ClipPlaneOffset = 0.03f;

    [Tooltip("Which renderer should this script use to render reflections texture? If you are not using any custom post processing/rendering with render features you probably want to leave it set to 0.")]
    public int CameraRendererIndex = 0;

    [Serializable]
    public enum ResolutionScales
    {
        Full,
        Half,
        Third,
        Quarter
    }

    [Serializable]
    public enum SkipFrames
    {
        NoSkipFrame,
        EverySecondFrame,
        EveryThirdFrame,
        EveryFourthFrame
    }

    //local vars
    private Camera reflectionCamera;
    private RenderTexture reflectionTexture;
    private int reflectionTextureId = Shader.PropertyToID("_WaterReflectionTexture");

    //quality settings
    private bool fogBeforeReflections;
    private int maxLodBeforeReflections;
    private float lodBiasBeforeReflections;

    private int currentFrame = 0;

    private void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += RenderReflections;
    }

    private void Cleanup()
    {
        RenderPipelineManager.beginCameraRendering -= RenderReflections;

        if(reflectionCamera)
        {
            reflectionCamera.targetTexture = null;
              
            if(Application.isEditor)
            {
                DestroyImmediate(reflectionCamera.gameObject);
            } else
            {
                Destroy(reflectionCamera.gameObject);
            } 
        } 

        if(reflectionTexture)
        {
            RenderTexture.ReleaseTemporary(reflectionTexture);
        }
    }

    private void OnDisable()
    {
        Cleanup();
    }

    private void OnDestroy()
    {
        Cleanup();
    }

    private void UpdateCamera(Camera src, Camera dest)
    {
        if(dest == null)
            return;

        //copy normal camera settings
        dest.CopyFrom(src);

        //update occlusion culling and shadows rendering setting
        dest.useOcclusionCulling = RenderWithOcclusionCulling;
        UniversalAdditionalCameraData data = dest.gameObject.GetComponent<UniversalAdditionalCameraData>();
        if(data != null)
        {
            data.renderShadows = RenderWithShadows; 
        }
    }

    private void UpdateReflectionCamera(Camera realCamera)
    {
        //check if we allready created reflection camera
        if(reflectionCamera == null)
        {
            reflectionCamera = CreateReflectionCamera();
        }

        //get water plane informations
        Vector3 planePosition = new Vector3(0, 0, 0);
        Vector3 planeNormal = new Vector3(0, 1, 0);
        if(WaterPlane != null)
        {
            planeNormal = WaterPlane.transform.up;
            planePosition = WaterPlane.transform.position; 
        } 
        else
        {
            Debug.LogWarning("(Toon Water URP) Please attach water plane game object!");
        }

        UpdateCamera(realCamera, reflectionCamera);
        
        float w = -Vector3.Dot(planeNormal, planePosition) - ClipPlaneOffset;
        Vector4 reflectionPlane = new Vector4(planeNormal.x, planeNormal.y, planeNormal.z, w);

        Matrix4x4 reflection = Matrix4x4.identity;
        reflection *= Matrix4x4.Scale(new Vector3(1, -1, 1));

        CalculateReflectionMatrix(ref reflection, reflectionPlane);
        Vector3 oldPosition = realCamera.transform.position - new Vector3(0, planePosition.y * 2, 0);
        Vector3 newPosition = new Vector3(oldPosition.x, -oldPosition.y, oldPosition.z);
        reflectionCamera.transform.forward = Vector3.Scale(realCamera.transform.forward, new Vector3(1, -1, 1));
        reflectionCamera.worldToCameraMatrix = realCamera.worldToCameraMatrix * reflection;

        //calculate clip plane and projection matrix for reflection camera
        Vector4 clipPlane = WorldToCameraSpacePlane(reflectionCamera, planePosition - Vector3.up * 0.1f, planeNormal, 1.0f);
        Matrix4x4 projection = realCamera.CalculateObliqueMatrix(clipPlane);
        reflectionCamera.projectionMatrix = projection;
        reflectionCamera.cullingMask = LayersToReflect;
        reflectionCamera.transform.position = newPosition;
    }

    private Camera CreateReflectionCamera()
    {
        GameObject go = new GameObject("Planar Reflections Camera", typeof(Camera));
        UniversalAdditionalCameraData cameraData = go.AddComponent<UniversalAdditionalCameraData>();

        cameraData.requiresColorOption = CameraOverrideOption.Off;
        cameraData.requiresDepthOption = CameraOverrideOption.Off;
        cameraData.SetRenderer(CameraRendererIndex); 
         
        Camera reflectionCamera = go.GetComponent<Camera>();
        reflectionCamera.transform.SetPositionAndRotation(transform.position, transform.rotation);
        reflectionCamera.depth = -5;
        reflectionCamera.enabled = false;
        go.hideFlags = HideFlags.HideAndDontSave;

        return reflectionCamera;
    }
     
    private Vector4 WorldToCameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
    {
        Vector3 offsetPos = pos + normal * ClipPlaneOffset;
        Matrix4x4 m = cam.worldToCameraMatrix;
        Vector3 cameraPosition = m.MultiplyPoint(offsetPos);
        Vector3 cameraNormal = m.MultiplyVector(normal).normalized * sideSign;
        return new Vector4(cameraNormal.x, cameraNormal.y, cameraNormal.z, -Vector3.Dot(cameraPosition, cameraNormal));
    }

    private void CreatePlanarReflectionTexture(Camera cam)
    {
        //check if user changed render texture resolution if so destroy the texture and immediately create new one wih proper properties
        Vector2 textureRes = new Vector2((int) (cam.pixelWidth * UniversalRenderPipeline.asset.renderScale * GetScaleValue()), (int) (cam.pixelHeight * UniversalRenderPipeline.asset.renderScale * GetScaleValue()));
        if(reflectionTexture != null && (reflectionTexture.width != textureRes.x || reflectionTexture.height != textureRes.y))
        {
            RenderTexture.ReleaseTemporary(reflectionTexture);
            reflectionTexture = null;
        }

        if(reflectionTexture == null)
        { 
            RenderTextureFormat format = RenderTextureFormat.DefaultHDR;
            reflectionTexture = RenderTexture.GetTemporary((int) textureRes.x, (int) textureRes.y, 16, format); 
        }
        reflectionCamera.targetTexture = reflectionTexture;
    }
  
    private void RenderReflections(ScriptableRenderContext context, Camera camera)
    {
        if(camera.cameraType == CameraType.Reflection || camera.cameraType == CameraType.Preview)
            return;
         
        currentFrame++; 
        if(currentFrame % GetSkipFrameValue() != 0)
            return;

        UpdateReflectionCamera(camera);
        CreatePlanarReflectionTexture(camera);

        //remember current quality settings
        fogBeforeReflections = RenderSettings.fog;
        maxLodBeforeReflections = QualitySettings.maximumLODLevel;
        lodBiasBeforeReflections = QualitySettings.lodBias;

        //change quality settings to lower
        GL.invertCulling = true;
        RenderSettings.fog = false;
        QualitySettings.maximumLODLevel = 1;
        QualitySettings.lodBias = lodBiasBeforeReflections * 0.5f;

        //render
        UniversalRenderPipeline.RenderSingleCamera(context, reflectionCamera); 

        //restore quality settings
        GL.invertCulling = false;
        RenderSettings.fog = fogBeforeReflections;
        QualitySettings.maximumLODLevel = maxLodBeforeReflections;
        QualitySettings.lodBias = lodBiasBeforeReflections;

        //make reflection texture global and accessible in every shader
        Shader.SetGlobalTexture(reflectionTextureId, reflectionTexture);
    }

    private static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane)
    {
        reflectionMat.m00 = (1F - 2F * plane[0] * plane[0]);
        reflectionMat.m01 = (-2F * plane[0] * plane[1]);
        reflectionMat.m02 = (-2F * plane[0] * plane[2]);
        reflectionMat.m03 = (-2F * plane[3] * plane[0]);

        reflectionMat.m10 = (-2F * plane[1] * plane[0]);
        reflectionMat.m11 = (1F - 2F * plane[1] * plane[1]);
        reflectionMat.m12 = (-2F * plane[1] * plane[2]);
        reflectionMat.m13 = (-2F * plane[3] * plane[1]);

        reflectionMat.m20 = (-2F * plane[2] * plane[0]);
        reflectionMat.m21 = (-2F * plane[2] * plane[1]);
        reflectionMat.m22 = (1F - 2F * plane[2] * plane[2]);
        reflectionMat.m23 = (-2F * plane[3] * plane[2]);

        reflectionMat.m30 = 0F;
        reflectionMat.m31 = 0F;
        reflectionMat.m32 = 0F;
        reflectionMat.m33 = 1F;
    }

    private float GetScaleValue()
    {
        switch(ResolutionScale)
        {
            case ResolutionScales.Full:
                return 1f;
            case ResolutionScales.Half:
                return 0.5f;
            case ResolutionScales.Third:
                return 0.33f;
            case ResolutionScales.Quarter:
                return 0.25f;
            default:
                return 0.5f;
        }
    }

    private int GetSkipFrameValue()
    {
        switch(SkipEveryNFrame)
        {
            case SkipFrames.NoSkipFrame:
                return 1;
            case SkipFrames.EverySecondFrame:
                return 2;
            case SkipFrames.EveryThirdFrame:
                return 3;
            case SkipFrames.EveryFourthFrame:
                return 4;
            default:
                return 1;
        }
    }
}