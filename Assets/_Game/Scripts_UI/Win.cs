using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : UICanvas
{

    private void Start()
    {

    }
    public void RetryButton()
    {
        UIManager.Ins.ShowUILoading(() =>
        {

            LevelManager.Instance.LoadLevel();
            GameManager.ChangeState(GameState.Playing);
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<GamePlay>();

        });
    }

    public void NextLevelButton()
    {
        UIManager.Ins.ShowUILoading(() =>
        {

            LevelManager.Instance.NextLevel();

            GameManager.ChangeState(GameState.Playing);

            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<GamePlay>();

        });
    }
}
