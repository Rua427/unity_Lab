using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTraveller : MonoBehaviour
{
    public GameObject graphicsObject;
    public GameObject graphicsClone { get; set; }
    public Vector3 previousOffsetFromPortal { get; set; }
    public Material[] originalMaterials { get; set; }
    public Material[] cloneMaterials { get; set; }

    // 텔레포트
    public virtual void Teleport(Transform fromPortal, Transform toPortal, Vector3 pos, Quaternion rot)
    {
        transform.position = pos;
        transform.rotation = rot;
    }
    //포탈 진입점
    public virtual void EnterPortalThreshold()
    {
        if (graphicsClone == null)
        {
            // 플레이어 clone 생성
            graphicsClone = Instantiate(graphicsObject);
            graphicsClone.transform.parent = graphicsObject.transform.parent;
            graphicsClone.transform.localScale = graphicsObject.transform.localScale;
            originalMaterials = GetMaterials(graphicsObject);
            cloneMaterials = GetMaterials(graphicsClone);
        }
        else
        {
            // clone 생성했으면 렌더링 활성화
            graphicsClone.SetActive(true);
        }
    }

    // 포탈 나감
    public virtual void ExitPortalThreshold()
    {
        graphicsClone.SetActive(false);
        for (int i = 0; i < originalMaterials.Length; i++)
        {
            originalMaterials[i].SetVector("sliceNormal", Vector3.zero);
        }
    }

    public void SetSliceOffsetDst(float dst, bool clone)
    {
        for (int i = 0; i < originalMaterials.Length; i++)
        {
            if (clone)
            {
                cloneMaterials[i].SetFloat("sliceOffsetDst", dst);
            }
            else
            {
                originalMaterials[i].SetFloat("slideOffsetDst", dst);
            }
        }
    }

    Material[] GetMaterials(GameObject g)
    {
        var renderers = g.GetComponentsInChildren<MeshRenderer>();
        var matList = new List<Material>();

        foreach (var renderer in renderers)
        {
            foreach (var mat in renderer.materials)
            {
                matList.Add(mat);
            }
        }

        return matList.ToArray();
    }
}
