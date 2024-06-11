using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<BotCtl>
{
    private int random;
    public void OnEnter(BotCtl t)
    {
        t.ChangeAnim(Constants.ANIM_IDLE);
        random = Random.Range(1, 10);
        t.RandomCountFindBrick = random;
        t.FindBrick();
    }

    public void OnExecute(BotCtl t)
    {
        t.ChangeState(new CollectState());
    }

    public void OnExit(BotCtl t)
    {

    }

}
