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

    //lấy ra vị trí bắt đầu ngẫu nhiên
    public Transform GetRandomStartPoint()
    {

        // Lấy vị trí ngẫu nhiên từ danh sách
        int randomIndex = Random.Range(0, StartPoint.Count);
        Transform randomPoint = StartPoint[randomIndex];

        // Loại bỏ phần tử đã lấy ra khỏi danh sách
        StartPoint.RemoveAt(randomIndex);

        return randomPoint;
    }
}
