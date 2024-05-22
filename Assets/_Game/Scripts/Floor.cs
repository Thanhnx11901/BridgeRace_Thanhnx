using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private GameObject prefabBirck;
    [SerializeField] private Transform PoinSpawnBrick;
    private void Start()
    {
        for (int x = 0; x < 10; x+=2)
        {
            for (int z = 0; z < 10; z+=2)
            {
                GameObject brickSpawn = Instantiate(prefabBirck);
                brickSpawn.transform.localPosition = new Vector3(PoinSpawnBrick.position.x + x, PoinSpawnBrick.position.y, PoinSpawnBrick.position.z + z);
            }
        } 
    }
}
