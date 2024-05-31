using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class PlayerCtl : CharacterCtl
{
    [SerializeField] private FixedJoystick joystick;

    [SerializeField] private Transform posRaycastCheckStair;

    [SerializeField] private CharacterController controller;

    private Vector3 moveDirection;

    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private float rotationSpeed;

    private bool isMoving;

    private float horizontal;
    private float vertical;

    public void OnInit()
    {
        TF.rotation = Quaternion.Euler(0, 0, 0);
        isMoving = true;
        speed = 10f;
        ClearBrick();
        gameObject.SetActive(true);

    }

    protected void Start()
    {
        OnInit();
    }

    protected void Update()
    {
        Move();
        CheckStair();
    }

    private void Move()
    {
        if (isMoving)
        {
            if (controller.isGrounded)
            {
                // Lấy đầu vào từ joystick hoặc bàn phím
                //horizontal = joystick.Horizontal;
                //vertical = joystick.Vertical;
                
                horizontal = Input.GetAxis("Horizontal");
                vertical = Input.GetAxis("Vertical");



                // Di chuyển trên mặt đất
                moveDirection = new Vector3(horizontal * speed, 0, vertical * speed);

                // Xoay nhân vật dựa trên đầu vào từ joystick
                if (horizontal != 0 || vertical != 0)
                {
                    ChangeAnim("Running");
                    Vector3 direction = new Vector3(horizontal, 0, vertical);
                    Quaternion rotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                }
                else
                {
                    ChangeAnim("Idle");
                }

                if (vertical < 0)
                {
                    speed = 10f;
                }
            }

            // Áp dụng trọng lực
            moveDirection.y -= gravity * Time.deltaTime;

            // Di chuyển nhân vật
            controller.Move(moveDirection * Time.deltaTime);
        }
        
    }

    private void CheckStair()
    {
        // raycast
        Vector3 posRaycast = posRaycastCheckStair.position;

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
                    if (stair.EColor == EColor.None || stair.EColor != this.eColor && vertical > 0)
                    {
                        stair.SetColor(this.eColor);
                        RemoveBrick();
                    }
                }
                // không có gạnh trên người 
                if (stackBricks.Count <= 0)
                {
                    if ((stair.EColor == EColor.None || stair.EColor != this.eColor) && vertical > 0)
                    {
                        speed = 0;
                    }
                }
            }
        }
        // Vẽ raycast trong Scene view để dễ hình dung
        Debug.DrawRay(posRaycast, Vector3.up * 5f, Color.red);
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Door door = Cache.GetDoor(hit.collider);
        if (door != null)
        {
            if (vertical > 0)
            {
                door.DeactiveDoor(this.eColor);
            }
        }

        DoorFinish doorFinish = Cache.GetDoorFinishs(hit.collider);
        if (doorFinish != null)
        {
            if (vertical > 0)
            {
                doorFinish.DeactiveDoor(this.eColor);
            }
        }

        Finish finish = Cache.GetFinish(hit.collider);

        if (finish != null)
        {
            //Set player và bot 
            SetPlayerAndBotsOnWin(finish);

            //Show Ui Win
            Invoke(nameof(ShowUIWin), 2f);
        }
    }

    private void ShowUIWin()
    {
        UIManager.Ins.OpenUI<Win>();
        UIManager.Ins.CloseUI<GamePlay>();
    }


    private void OnTriggerEnter(Collider other)
    {
        Brick brick = Cache.GetBrick(other);
        if (brick != null && brick.EColor == this.eColor)
        {
            brick.DeactiveBrick();
            AddBrick();
        }
    }

    private void SetPlayerAndBotsOnWin(Finish finish)
    {

        //Set Player
        isMoving = false;
        ChangeAnim("Victory");
        GameManager.ChangeState(GameState.Win);

        finish.finishColonms[0].SetColor(eColor);
        TF.position = finish.finishColonms[0].GetPoint();
        TF.rotation = Quaternion.Euler(0, 180f, 0);
        ClearBrick();


        //Set Bot
        LevelManager.Instance.ChangeStateWinnerState();
        List<BotCtl> botCtls = LevelManager.Instance.botCtls;
        for (int i = 0; i < 2; i++)
        {
            finish.finishColonms[i+1].SetColor(botCtls[i].eColor);
            botCtls[i].TF.position = finish.finishColonms[i+1].GetPoint();
            botCtls[i].TF.rotation = Quaternion.Euler(0, 180f, 0);
            botCtls[i].ClearBrick();

        }
    }
}
