using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;

    public void DeactiveDoor()
    {
        boxCollider.isTrigger = true;
        StartCoroutine(DisappearAndReappear());

    }

    IEnumerator DisappearAndReappear()
    {
        yield return new WaitForSeconds(0.1f);
        boxCollider.isTrigger = false;
    }
}
