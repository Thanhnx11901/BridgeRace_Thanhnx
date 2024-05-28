using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    private EColor eColor;

    public EColor EColor { get => eColor;}

    private void Awake()
    {
        eColor = EColor.None;
    }

    public void SetColor(EColor eColor)
    {
        this.eColor = eColor;
        meshRenderer.enabled = true;
        meshRenderer.material = LevelManager.Instance.dataColor.GetColor(eColor);
        
    }
}
