using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;


public class BotCtl : CharacterCtl
{
    [SerializeField] private Transform posRaycastCheckStair;

    public NavMeshAgent agent;
    public Platform currentPlatform;
    public Brick targetBrick;

    private bool isMove;

    protected IState<BotCtl> currentState;

    public void OnInit()
    {
        isMove = true;

        agent.enabled = true;

        agent.velocity = agent.velocity.normalized;

        if (LevelManager.Instance.currentLevel != null)
        {
            currentPlatform = LevelManager.Instance.currentLevel.platforms[0];
        }
        gameObject.SetActive(true);

        ClearBrick();

        ChangeAnim("Idle");

        Invoke(nameof(ChangeIdleState), 0.000001f);
    }

    private void ChangeIdleState(){
        ChangeState(new IdleState());
    }

    private void Start()
    {
        OnInit();
        ChangeState(new IdleState());
    }
    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
        //Debug.Log(this.name);
        //Debug.Log(Agent.velocity.normalized);

    }

    public void ChangeState(IState<BotCtl> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public void Move(Vector3 pos)
    {
        if (isMove)
        {
            agent.SetDestination(pos);
            ChangeAnim("Running");
        }
    }

    public void CheckStair()
    {
        // raycast
        Vector3 posRaycast = posRaycastCheckStair.position;

        // Tạo raycast từ vị trí của player xuống dưới theo trục y
        RaycastHit hit;
        if (Physics.Raycast(posRaycast, Vector3.up, out hit, 5f))
        {
            Stair stair = Cache.GetStair(hit.collider);
            if (stair != null)
            {
                // có gạnh trên người
                if (stackBricks.Count > 0)
                {
                    // gạch không màu hoặc màu khác player
                    if (stair.EColor == EColor.None || stair.EColor != this.eColor)
                    {
                        stair.SetColor(this.eColor);
                        RemoveBrick();
                    }
                }
            }
        }
        // Vẽ raycast trong Scene view để dễ hình dung
        Debug.DrawRay(posRaycast, Vector3.up * 5f, Color.red);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Door door = Cache.GetDoor(collision.collider);
        if(door != null)
        {
            door.DeactiveDoor(this.eColor);
            currentPlatform = door.platform;
            Debug.Log("Door");
        }

        DoorFinish doorFinish = Cache.GetDoorFinishs(collision.collider);
        if (doorFinish != null)
        {
            doorFinish.DeactiveDoor(this.eColor);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Brick brick = Cache.GetBrick(other);
        if(brick != null && brick.EColor == eColor)
        {
            brick.DeactiveBrick();
            AddBrick();
            FindBrick();
        }
    }

    public void FindBrick()
    {
        agent.velocity = agent.velocity.normalized;

        List<Brick> bricks = currentPlatform.GetBricksByColor(eColor);

        float minDistance = float.MaxValue;

        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].isActiveBrick() == true)
            {
                float distance = Vector3.Distance(transform.position, bricks[i].transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    targetBrick = bricks[i];
                }
            }
            
        }
    }

    public void IsActiveAgent(bool isActive)
    {
        agent.enabled = isActive;
    }

    public void SetMove(bool isMove)
    {
        this.isMove = isMove;
    }
}
