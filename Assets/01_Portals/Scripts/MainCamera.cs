using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Portal[] protals;

    private void Awake()
    {
        protals = FindObjectsOfType<Portal>();
    }

    void OnPreCull()
    {
        for (int i = 0; i < protals.Length; i++)
        {

            protals[i].PrePortalRender();
        }
        for (int i = 0; i < protals.Length; i++)
        {

            protals[i].Render();
        }
        for (int i = 0; i < protals.Length; i++)
        {

            protals[i].PostPortalRender();
        }
    }
}
