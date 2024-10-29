using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lose : UICanvas
{
    private void Start()
    {

    }

    public void RetryButton()
    {
        Time.timeScale = 1;
        UIManager.Ins.ShowUILoading(() =>
        {
            LevelManager.Instance.LoadLevel();
            GameManager.ChangeState(GameState.Playing);
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<GamePlay>();

        });
    }
}
