using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform: MonoBehaviour
{
    [SerializeField] private Transform PointSpawnBrick;

    private List<EColor> distributedColors = new List<EColor>();

    public List<Brick> bricks = new List<Brick>();

    public void SpawnBricks(bool isAcitve)
    {
        this.distributedColors = SpawnRandomColors(LevelManager.Instance.colors);

        int colorIndex = 0;
        for (int x = 0; x < 10; x += 2)
        {
            for (int z = 0; z < 10; z += 2)
            {
                Brick brickSpawn = SimplePool.Spawn<Brick>(PoolType.Brick, transform);
                // Gán màu sắc từ danh sách đã phân phối và xáo trộn
                brickSpawn.SetColor(distributedColors[colorIndex++]);
                brickSpawn.IsActiveBrick(isAcitve);
                brickSpawn.TF.position = new Vector3(PointSpawnBrick.position.x + x, PointSpawnBrick.position.y, PointSpawnBrick.position.z + z);
                brickSpawn.TF.localScale = new Vector3(.3f, .3f, .6f);

                bricks.Add(brickSpawn);
            }
        }
    }

    public void ActiveColors(EColor eColor) {
        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].EColor == eColor)
            {
                bricks[i].IsActiveBrick(true);
            }
        }
    }

    private List<EColor> SpawnRandomColors(List<EColor> colors)
    {
        List<EColor> distributedColors = new List<EColor>();
        int totalBricks = (10 / 2) * (10 / 2); // Tính toán tổng số gạch sẽ được sinh ra
        int numColors = colors.Count;
        int bricksPerColor = totalBricks / numColors;
        int remainingBricks = totalBricks % numColors;

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
        return distributedColors;
    }


    // Hàm xáo trộn danh sách
    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public Vector3 GetPosBrick(int index)
    {
        return bricks[index].TF.position;
    }

    public List<Brick> GetBricksByColor(EColor eColor)
    {
        List<Brick> bricksByColor = new List<Brick>();
        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].EColor == eColor)
            {
                bricksByColor.Add(bricks[i]);
            }
        }
        return bricksByColor;
    }

}
