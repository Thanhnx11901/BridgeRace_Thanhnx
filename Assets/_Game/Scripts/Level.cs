using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] public List<Platform> platforms;

    public Transform finishPoint;

    public List<Transform> StartPoint;

    private void Start()
    {
        SpawnBrickPlatform();
    }

    //sinh gạnh trên platform
    private void SpawnBrickPlatform()
    {
        platforms[0].SpawnBricks(true);
        for (int i = 1; i < platforms.Count; i++)
        {
            platforms[i].SpawnBricks(false);
        }
    }

    public void ShuffleListStartPoint()
    {
        ShuffleList(StartPoint);
    }

    public void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
