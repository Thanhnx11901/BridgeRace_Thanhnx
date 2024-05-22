using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public void ActiveMeshRenderer(Material material)
    {
        meshRenderer.enabled = true;
        meshRenderer.material = material;
    }
}
