using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class BotCtl : CharacterCtl
{
    [SerializeField] private Transform posRaycastCheckStair;

    [SerializeField] private NavMeshAgent agent;

    private int randomCountFindBrick;

    public Platform currentPlatform;

    public Brick targetBrick;

    private bool isMove;

    protected IState<BotCtl> currentState;

    private IdleState idleState;
    private CollectState collectState;

    private ReachDestinationState reachDestinationState;

    private WinnerState winnerState;



    public NavMeshAgent Agent { get => agent; }

    public IdleState IdleState { get => idleState;}
    public CollectState CollectState { get => collectState;}
    public ReachDestinationState ReachDestinationState { get => reachDestinationState;}
    public WinnerState WinnerState { get => winnerState;}
    public int RandomCountFindBrick { get => randomCountFindBrick; set => randomCountFindBrick = value; }

    public void OnInit()
    {
        TF.rotation = Quaternion.Euler(0, 0, 0);
        isMove = true;
        Agent.enabled = true;
        Agent.velocity = Agent.velocity.normalized;

        if (LevelManager.Instance.currentLevel != null)
        {
            currentPlatform = LevelManager.Instance.currentLevel.platforms[0];
        }

        gameObject.SetActive(true);

        ClearBrick();

        ChangeAnim(Constants.ANIM_IDLE);
        Invoke(nameof(ChangeIdleState), 0f);
    }
    private void ChangeIdleState()
    {
        ChangeState(idleState);
    }
    private void Start()
    {
        idleState = new IdleState();
        collectState = new CollectState();
        reachDestinationState = new ReachDestinationState();
        winnerState = new WinnerState();
        OnInit();
    }
    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
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
            Agent.SetDestination(pos);
            ChangeAnim(Constants.ANIM_RUN);
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
        }

        DoorFinish doorFinish = Cache.GetDoorFinishs(collision.collider);
        if (doorFinish != null)
        {
            doorFinish.DeactiveDoor(this.eColor);
        }

        Finish finish = Cache.GetFinish(collision.collider);

        if (finish != null && !GameManager.IsState(GameState.Lose))
        {
            Time.timeScale = 0;

            GameManager.ChangeState(GameState.Lose);

            ShowUILose();
        }
    }

    private void ShowUILose()
    {
        UIManager.Ins.OpenUI<Lose>();
        UIManager.Ins.CloseUI<GamePlay>();
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
        Agent.velocity = Agent.velocity.normalized;

        List<Brick> bricks = currentPlatform.GetBricksByColor(eColor);

        float minDistance = float.MaxValue;

        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].IsActiveBrick() == true)
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

    public bool IsEnoughBricks()
    {
        return stackBricks.Count > RandomCountFindBrick;
    }

    public void IsActiveAgent(bool isActive)
    {
        Agent.enabled = isActive;
    }

    public void SetMove(bool isMove)
    {
        this.isMove = isMove;
    }
}
