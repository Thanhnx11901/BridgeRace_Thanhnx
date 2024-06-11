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
        Vector3 pointNavMesh = LevelManager.Instance.currentLevel.finishPoint.position;
        t.Move(pointNavMesh);
        t.CheckStair();
        if (t.stackBricks.Count <= 0)
        {
            t.Agent.velocity = Vector3.zero;

            t.ChangeState(t.IdleState);
        }
    }

    public void OnExit(BotCtl t)
    {

    }

}
