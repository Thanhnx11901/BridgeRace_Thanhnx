using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : UICanvas
{
    public Text score;

    public void MainMenuButton()
    {
        UIManager.Ins.OpenUI<MianMenu>();
        Close(0);
    }

    public void NextLevelButton()
    {
        LevelManager.Instance.NextLevel();

        GameManager.ChangeState(GameState.Playing);

        UIManager.Ins.OpenUI<GamePlay>();
        Close(0);
    }
}
