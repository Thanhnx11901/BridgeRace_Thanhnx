using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : UICanvas
{
    public void SettingButton()
    {
        UIManager.Ins.OpenUI<Setting>();
        Time.timeScale = 0;
    }
}
