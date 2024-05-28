using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickAdd : GameUnit
{
    [SerializeField] private MeshRenderer meshRenderer;

    public void SetColor(EColor eColor)
    {
        meshRenderer.material = LevelManager.Instance.dataColor.GetColor(eColor);
    }
}
