using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : GameUnit
{
    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;

    public EColor eColor;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        meshRenderer.material.color = LevelManager.Instance.dataColor.GetColor(eColor);
    }
    public void DeactiveBrick()
    {
        meshRenderer.enabled = false;
        boxCollider.enabled = false;

        StartCoroutine(DisappearAndReappear());

    }

    IEnumerator DisappearAndReappear()
    {
        yield return new WaitForSeconds(2f);
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
    }
}
