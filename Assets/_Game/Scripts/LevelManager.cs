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
        playerCtl.gameObject.SetActive(false);
        //sinh bot
        SpawnBot(colors);

        //load level
        //LoadLevel(0);

        
    }

    //load level
    public void LoadLevel(int indexLevel)
    {
        if (currentLevel != null) Destroy(currentLevel.gameObject);

        currentLevel = Instantiate(levels[indexLevel], transform);


        //set vị trí cho player và bot
        playerCtl.TF.position = currentLevel.GetRandomStartPoint().position;
        playerCtl.OnInit();

        for (int i = 0; i < botCtls.Count; i++)
        {
            botCtls[i].TF.position = currentLevel.GetRandomStartPoint().position;
            botCtls[i].OnInit();
        }
    }

    public void NextLevel()
    {
        if (currentLevel != null) Destroy(currentLevel.gameObject);

        currentLevel = Instantiate(levels[1], transform);

        //set vị trí cho player và bot
        for (int i = 0; i < botCtls.Count; i++)
        {
            botCtls[i].TF.position = currentLevel.GetRandomStartPoint().position;
            botCtls[i].OnInit();
        }
        Invoke(nameof(SetPos), 0.00000001f);

    }

    //sinh bot
    public void SpawnBot(List<EColor> eColors)
    {
        for (int i = 1; i < 4; i++)
        {
            BotCtl botCtl = SimplePool.Spawn<BotCtl>(PoolType.Bot, transform);
            botCtl.gameObject.SetActive(false);
            botCtl.SetColor(eColors[i]);
            botCtls.Add(botCtl);
            
        }
    }

    //Tắt di chuyển của bot
    public void DeativeMove()
    {
        for (int i = 0; i < botCtls.Count; i++)
        {
            //botCtls[i].SetMove(false);
            //botCtls[i].IsActiveAgent(false);
            botCtls[i].ChangeState(new WinnerState());
        }
    }

    private void SetPos()
    {
        playerCtl.TF.position = currentLevel.GetRandomStartPoint().position;
        playerCtl.OnInit();
    }
}
