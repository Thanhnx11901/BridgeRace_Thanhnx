using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private MeshRenderer colonm1;
    [SerializeField] private MeshRenderer colonm2;
    public Platform platform;
    public void DeactiveDoor(EColor eColor)
    {
        boxCollider.isTrigger = true;
        StartCoroutine(DisappearAndReappear());
        platform.ActiveColors(eColor);
        colonm1.material = LevelManager.Instance.dataColor.GetColor(eColor);
        colonm2.material = LevelManager.Instance.dataColor.GetColor(eColor);

    }

    IEnumerator DisappearAndReappear()
    {
        yield return new WaitForSeconds(0.1f);
        boxCollider.isTrigger = false;
    }
}
