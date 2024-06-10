using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<BotCtl>
{
    public void OnEnter(BotCtl t)
    {
        t.ChangeAnim(Constants.ANIM_IDLE);
    }

    public void OnExecute(BotCtl t)
    {
        t.FindBrick();
        t.ChangeState(new CollectState());
    }

    public void OnExit(BotCtl t)
    {

    }

}
