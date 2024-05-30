using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerState : IState<BotCtl>
{
    public void OnEnter(BotCtl t)
    {
        t.SetMove(false);
        t.IsActiveAgent(false);
    }

    public void OnExecute(BotCtl t)
    {
        t.ChangeAnim("Dance");
    }

    public void OnExit(BotCtl t)
    {

    }

}