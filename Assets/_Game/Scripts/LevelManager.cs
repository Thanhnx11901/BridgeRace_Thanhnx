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
    public int indexCurrentLevel;

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
        //sinh ra 3 bot 
        SpawnBot(colors,3);

        indexCurrentLevel = PlayerPrefs.GetInt("Current_Level");
    }

    //load level
    public void LoadLevel()
    {
        if (currentLevel != null) Destroy(currentLevel.gameObject);

        currentLevel = Instantiate(levels[indexCurrentLevel], transform);

        currentLevel.ShuffleListStartPoint();

        ChangeStateWinnerState();
        //set vị trí cho player và bot
        setPlayerAndBot();

    }

    public void NextLevel()
    {
        if (currentLevel != null) Destroy(currentLevel.gameObject);

        indexCurrentLevel++;

        PlayerPrefs.SetInt("Current_Level", indexCurrentLevel);

        currentLevel = Instantiate(levels[indexCurrentLevel], transform);

        currentLevel.ShuffleListStartPoint();

        ChangeStateWinnerState();

        //set vị trí cho player và bot
        setPlayerAndBot();



    }

    //sinh bot
    public void SpawnBot(List<EColor> eColors, int quantityBot)
    {
        for (int i = 1; i <= quantityBot; i++)
        {
            BotCtl botCtl = SimplePool.Spawn<BotCtl>(PoolType.Bot, transform);
            botCtl.gameObject.SetActive(false);
            botCtl.SetColor(eColors[i]);
            botCtls.Add(botCtl);
            
        }
    }

    public void setPlayerAndBot()
    {
        //Invoke(nameof(SetPosBot), 0);
        //Invoke(nameof(SetPosPlayer), 0);
        playerCtl.OnInit();
        playerCtl.TF.position = currentLevel.StartPoint[0].position;
        for (int i = 1; i < currentLevel.StartPoint.Count; i++)
        {
            botCtls[i-1].TF.position = currentLevel.StartPoint[i].position;
            botCtls[i-1].OnInit();
        }
    }

    //Tắt di chuyển của bot
    public void ChangeStateWinnerState()
    {
        for (int i = 0; i < botCtls.Count; i++)
        {
            botCtls[i].ChangeState(new WinnerState());
        }
    }

    //private void SetPosPlayer()
    //{
    //    playerCtl.TF.position = currentLevel.GetRandomStartPoint().position;
    //    playerCtl.OnInit();
    //}
    //private void SetPosBot()
    //{
    //    for (int i = 0; i < botCtls.Count; i++)
    //    {
    //        botCtls[i].TF.position = currentLevel.GetRandomStartPoint().position;
    //        botCtls[i].OnInit();
    //    }
    //}
}
