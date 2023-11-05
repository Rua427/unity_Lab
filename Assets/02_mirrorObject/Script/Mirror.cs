using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Mirror : MonoBehaviour
{
    public Camera playerCam;
    public Camera mirrorCam;

    public MeshRenderer screen;

    RenderTexture viewTexture;
    void Awake()
    {
        screen.material.SetInt("displayMask", 1);
        mirrorCam.enabled = false;
    }
    void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += Render;
    }
    void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= Render;
    }

    void Render(ScriptableRenderContext context, Camera camera)
    {
        if (!CameraUtility.VisibleFromCamera(screen, playerCam)) { return; }

        CreateViewTexture();

        MirrorCamPosCalculation();

        mirrorCam.projectionMatrix = playerCam.projectionMatrix;

        screen.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        screen.material.SetInt("displayMask", 1);
        UniversalRenderPipeline.RenderSingleCamera(context, mirrorCam);

        screen.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }

    void MirrorCamPosCalculation()
    {
        Matrix4x4 mirrorPos = transform.localToWorldMatrix;
        // xy plane
        mirrorPos.m22 *= -1;
        // xz plane
        mirrorPos.m02 *= -1;

        var distance = transform.worldToLocalMatrix * playerCam.transform.localToWorldMatrix;

        // reflect transform 계산
        var reflectTransform = mirrorPos * distance;
        reflectTransform.m00 *= -1;
        reflectTransform.m20 *= -1;

        mirrorCam.transform.SetPositionAndRotation(reflectTransform.GetColumn(3), reflectTransform.rotation);
    }

    void CreateViewTexture()
    {
        if (viewTexture == null || viewTexture.width != Screen.width || viewTexture.height != Screen.height)
        {
            if (viewTexture != null)
            {
                viewTexture.Release();
            }
            viewTexture = new RenderTexture(Screen.width, Screen.height, 0);
            mirrorCam.targetTexture = viewTexture;
            screen.material.SetTexture("_MainTex", viewTexture);
        }
    }
}
