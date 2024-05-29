using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public PlayerCtl playerCtl;
    public DataColor dataColor;
    public List<EColor> colors = new List<EColor>();
    public List<Level> levels = new List<Level>();
    public Level currentLevel;

    public List<BotCtl> botCtls = new List<BotCtl>();

    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        // sinh list màu random màu sắc
        colors = dataColor.GetRandomEnumColors(4);
    }
    private void Start()
    {
        // random màu sắc cho player và sinh bot
        playerCtl.SetColor(colors[0]);

        //currentLevel = Instantiate(levels[0], transform);

        SpawnBot(colors);

        playerCtl.TF.position = currentLevel.GetRandomStartPoint().position;
        for (int i = 0; i < botCtls.Count; i++)
        {
            botCtls[i].TF.position = currentLevel.GetRandomStartPoint().position;
        }


        // sinh level
        //LoadLevel(1);
    }

    public void LoadLevel(int indexLevel)
    {
        if (currentLevel != null) Destroy(currentLevel.gameObject);

        

        //set vị trí cho player và bot
        
    }

    public void SpawnBot(List<EColor> eColors)
    {
        for (int i = 1; i < 4; i++)
        {
            BotCtl botCtl = SimplePool.Spawn<BotCtl>(PoolType.Bot, transform);
            botCtl.SetColor(eColors[i]);
            botCtls.Add(botCtl);
        }
    }
}
