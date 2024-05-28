using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReachDestinationState : IState<BotCtl>
{
    public void OnEnter(BotCtl t)
    {

    }

    public void OnExecute(BotCtl t)
    {
        t.Move(LevelManager.Instance.levels[0].finishPoint.position);
        t.CheckStair();
        if(t.stackBricks.Count <= 0)
        {
            t.agent.velocity = Vector3.zero;
            t.ChangeState(new IdleState());
        }
    }

    public void OnExit(BotCtl t)
    {

    }

}
