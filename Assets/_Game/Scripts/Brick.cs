using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : GameUnit
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private BoxCollider boxCollider;

    private EColor eColor;

    public EColor EColor { get => eColor; }

    public void SetColor(EColor eColor)
    {
        this.eColor = eColor;
        meshRenderer.material = LevelManager.Instance.dataColor.GetColor(eColor);

    }
    public void IsActiveBrick(bool isActive)
    {
        meshRenderer.enabled = isActive;
        boxCollider.enabled = isActive;
    }

    public void DeactiveBrick()
    {
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        StartCoroutine(DisappearAndReappear());

    }
    public bool IsActiveBrick()
    {
        if(meshRenderer.enabled == true && boxCollider.enabled == true)
        {
            return true;
        }
        return false;
    }

    IEnumerator DisappearAndReappear()
    {
        yield return new WaitForSeconds(2f);
        meshRenderer.enabled = true;
        boxCollider.enabled = true;
    }
}
