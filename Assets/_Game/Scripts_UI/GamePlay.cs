using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : UICanvas
{
    private void Start() {
        
    }
    public void SettingButton()
    {
        UIManager.Ins.OpenUI<Setting>();
        Time.timeScale = 0;
    }
}
