using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private Brick prefabBrick;
    [SerializeField] private Transform PointSpawnBrick;
    private List<EColor> colors = new List<EColor>();


    private void Start()
    {
        colors = LevelManager.Instance.colors;
        SpawnBricks(colors);
    }
    void SpawnBricks(List<EColor> colors)
    {
        if (colors == null || colors.Count == 0)
        {
            Debug.LogError("No colors specified.");
            return;
        }

        int totalBricks = (10 / 2) * (10 / 2); // Tính toán tổng số gạch sẽ được sinh ra
        int numColors = colors.Count;
        int bricksPerColor = totalBricks / numColors;
        int remainingBricks = totalBricks % numColors;

        List<EColor> distributedColors = new List<EColor>();

        // Phân phối màu sắc đều
        for (int i = 0; i < numColors; i++)
        {
            for (int j = 0; j < bricksPerColor; j++)
            {
                distributedColors.Add(colors[i]);
            }
        }

        // Thêm gạch dư vào danh sách
        for (int i = 0; i < remainingBricks; i++)
        {
            distributedColors.Add(colors[i % numColors]);
        }

        // Xáo trộn danh sách màu sắc
        Shuffle(distributedColors);

        int colorIndex = 0;
        for (int x = 0; x < 10; x += 2)
        {
            for (int z = 0; z < 10; z += 2)
            {
                Brick brickSpawn = SimplePool.Spawn<Brick>(PoolType.Brick, transform);
                // Gán màu sắc từ danh sách đã phân phối và xáo trộn
                brickSpawn.eColor = distributedColors[colorIndex++];

                brickSpawn.transform.localPosition = new Vector3(PointSpawnBrick.position.x + x, PointSpawnBrick.position.y, PointSpawnBrick.position.z + z);
            }
        }
    }

    // Hàm xáo trộn danh sách
    void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

}
