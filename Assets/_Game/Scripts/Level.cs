using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] public List<Platform> platforms;

    public Transform finishPoint;

    public Transform[] StartPoint;

    private void Start()
    {
       
    }

    public void SpawnBot(List<EColor> eColors)
    {
        for (int i = 1; i < 2; i++)
        {
            BotCtl botCtl = SimplePool.Spawn<BotCtl>(PoolType.Bot, transform);
            botCtl.TF.position = StartPoint[i].position;
            botCtl.SetColor(eColors[i]);
            botCtl.eColor = eColors[i];
        }
        platforms[0].SpawnBricks(true);
        platforms[1].SpawnBricks(false);
        platforms[2].SpawnBricks(false);
    }
}
