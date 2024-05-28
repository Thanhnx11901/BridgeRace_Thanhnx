using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectState : IState<BotCtl>
{
    float timer;
    float randomTime;

    public void OnEnter(BotCtl t)
    {
        timer = 0;
        randomTime = Random.Range(5f, 10f);
    }

    public void OnExecute(BotCtl t)
    {
        t.Move(t.targetBrick.TF.position);

        if(timer > randomTime)
        {
            t.ChangeState(new ReachDestinationState());
        }
        timer += Time.deltaTime; 
    }

    public void OnExit(BotCtl t)
    {

    }

}

