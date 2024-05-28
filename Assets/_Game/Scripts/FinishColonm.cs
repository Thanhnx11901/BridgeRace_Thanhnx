using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishColonm : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Transform point;

    public void SetColor(EColor color)
    {
        meshRenderer.material = LevelManager.Instance.dataColor.GetColor(color);
    }
    public Vector3 GetPoint()
    {
        return point.position;
    }

}
