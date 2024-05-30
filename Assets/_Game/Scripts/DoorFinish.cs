using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFinish : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private MeshRenderer colonm1;
    [SerializeField] private MeshRenderer colonm2;

    public void DeactiveDoor(EColor eColor)
    {
        boxCollider.isTrigger = true;
        StartCoroutine(DisappearAndReappear());
        colonm1.material = LevelManager.Instance.dataColor.GetColor(eColor);
        colonm2.material = LevelManager.Instance.dataColor.GetColor(eColor);
    }

    IEnumerator DisappearAndReappear()
    {
        yield return new WaitForSeconds(0.1f);
        boxCollider.isTrigger = false;
    }
}
