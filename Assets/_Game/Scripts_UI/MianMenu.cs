using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MianMenu : UICanvas
{
    public void PlayButton()
    {
        //load level

        UIManager.Ins.ShowUILoading(() =>
        {
            GameManager.ChangeState(GameState.Playing);
            LevelManager.Instance.LoadLevel();

            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<GamePlay>();

        });
    }
}
