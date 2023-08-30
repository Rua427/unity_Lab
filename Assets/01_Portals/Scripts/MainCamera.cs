using UnityEngine;
using UnityEngine.Rendering;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Portal[] portals;

    private void Awake()
    {
        portals = FindObjectsOfType<Portal>();

    }

    void OnEnable()
    {
        // RenderPipelineManager에 이벤트 등록
        RenderPipelineManager.beginCameraRendering += BeginRender;
        // 이외에도..
        // RenderPipelineManager.endCameraRendering
        // RenderPipelineManager.beginContextRendering
        // RenderPipelineManager.endContextRendering
    }

    void OnDisable()
    {
        // 이벤트 해제
        RenderPipelineManager.beginCameraRendering -= BeginRender;
    }

    // 등록할 이벤트
    void BeginRender(ScriptableRenderContext context, Camera camera)
    {
        // Render 처리
        for (int i = 0; i < portals.Length; i++)
        {

            portals[i].PrePortalRender();
        }
        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].Render(context);
        }
        for (int i = 0; i < portals.Length; i++)
        {

            portals[i].PostPortalRender();
        }
    }


    // 실행 안됨
    // beginCameraRendering
    void OnPreCull()
    {
        Debug.Log("REnder");

    }

    // private void OnBecameVisible()
    // {
    //     Debug.Log("visible");
    // }

    // private void OnBecameInvisible()
    // {
    //     Debug.Log("invisible");
    // }
    // private void OnWillRenderObject()
    // {
    //     Debug.Log("WillRender");
    // }
    // // 실행 안됨
    // private void OnPreRender()
    // {
    //     Debug.Log("Pre render");
    // }

    // private void OnRenderObject()
    // {
    //     Debug.Log("REnderObject");
    // }

    // //실행 안됨
    // private void OnPostRender()
    // {
    //     Debug.Log("Post Render");
    // }
    // private void OnRenderImage(RenderTexture src, RenderTexture dest)
    // {
    //     Debug.Log("RenderImage");
    // }

    // private void OnGUI()
    // {
    //     Debug.Log("GUI");
    // }

    // private void OnDrawGizmos()
    // {
    //     Debug.Log("Gizmos");
    // }

    // private void RenderPipelineManager_endCameraRendering(ScriptableRenderContext context, Camera camera)
    // {
    //     Debug.Log("REnder");

    // }
}
