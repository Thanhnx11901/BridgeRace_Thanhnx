using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    private EColor eColor;
    private MeshRenderer meshRenderer;

    public EColor EColor { get => eColor;}

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        eColor = EColor.None;
    }
    public void ActiveMeshRenderer(EColor eColor)
    {
        meshRenderer.enabled = true;
        meshRenderer.material.color = LevelManager.Instance.dataColor.GetColor(eColor);
        this.eColor = eColor;
    }
}
