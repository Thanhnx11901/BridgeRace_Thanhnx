using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class BotCtl : CharacterCtl
{
    public NavMeshAgent agent;
    public Platform  currentPlatform;
    public Brick targetBrick;
    [SerializeField] private Transform posRaycastCheckStair;



    protected IState<BotCtl> currentState;

    public void Onit()
    {
        agent.velocity = agent.velocity.normalized;
        ClearBrick();
    }

    private void Start()
    {
        ChangeState(new IdleState());
        currentPlatform = LevelManager.Instance.levels[0].platforms[0];
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
        agent.SetDestination(pos);
        ChangeAnim("Running");
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
        if (collision.gameObject.CompareTag("Door"))
        {
            Door door = collision.gameObject.GetComponent<Door>();
            door.DeactiveDoor(this.eColor);
            currentPlatform = door.platform;
            Debug.Log("Door");
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
}
