using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }
    public void DestroyBrick()
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
