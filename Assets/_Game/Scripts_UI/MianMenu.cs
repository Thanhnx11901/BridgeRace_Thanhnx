using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MianMenu : UICanvas
{
    public void PlayButton()
    {
        //đổi trạng thái của game
        GameManager.ChangeState(GameState.Playing);

        //load level
        LevelManager.Instance.LoadLevel(0);

        UIManager.Ins.OpenUI<GamePlay>();
        Close(0);
    }
}
