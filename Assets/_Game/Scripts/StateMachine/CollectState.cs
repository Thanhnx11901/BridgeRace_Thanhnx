using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectState : IState<BotCtl>
{
    public void OnEnter(BotCtl t)
    {
        t.Move(t.targetBrick.TF.position);
    }

    public void OnExecute(BotCtl t)
    {
        if (t.IsEnoughBricks())
        {
            t.ChangeState(t.ReachDestinationState);
        }
        else
        {
            t.ChangeState(t.CollectState);
        }

    }

    public void OnExit(BotCtl t)
    {

    }

}

