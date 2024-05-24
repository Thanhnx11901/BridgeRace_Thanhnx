using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtl : CharacterCtl
{
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private GameObject brickPrefab;

    private Stack<GameObject> stackBricks = new Stack<GameObject>();

    public Dictionary<Collider, GameObject> Dict;

    private CharacterController controller;

    [SerializeField] private SkinnedMeshRenderer sMeshRenderer;

    private Vector3 moveDirection;
    private Vector3 currentPosBrick;

    [SerializeField] private Transform posRaycastCheckStair;


    public float speed = 6.0f;
    public float gravity = 20.0f;
    public float rotationSpeed = 700.0f;

    private float horizontal;
    private float vertical;

    public void OnInit()
    {
    }


    private void Start()
    {
        brickPrefab.GetComponent<Brick>().eColor = this.eColor;
        OnInit();
        controller = GetComponent<CharacterController>();
        currentPosBrick = new Vector3(0,1.2f,-.6f);
    }

    public void SetColor(EColor eColor)
    {
        this.eColor = eColor;
        sMeshRenderer.material.color = LevelManager.Instance.dataColor.GetColor(eColor);
    }

    private void Update()
    {
        Move();
        CheckStair();
    }

    private void Move()
    {
        if (controller.isGrounded)
        {
            // Lấy đầu vào từ joystick hoặc bàn phím
            horizontal = joystick.Horizontal;
            vertical = joystick.Vertical;

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
                this.speed = 10f;
            }


        }

        // Áp dụng trọng lực
        moveDirection.y -= gravity * Time.deltaTime;

        // Di chuyển nhân vật
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void CheckStair()
    {
        // raycast
        Vector3 posRaycast = posRaycastCheckStair.position;

        // Tạo raycast từ vị trí của player xuống dưới theo trục y
        RaycastHit hit;
        if (Physics.Raycast(posRaycast, Vector3.up, out hit,5f))
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
                        stair.ActiveMeshRenderer(this.eColor);
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
                    else if ((stair.EColor == EColor.None || stair.EColor != this.eColor) && vertical <= 0)
                    {
                        speed = 10f; 
                    }
                }
            }
        }
        // Vẽ raycast trong Scene view để dễ hình dung
        Debug.DrawRay(posRaycast, Vector3.up * 5f, Color.red);
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.gameObject.name);
        Door door = Cache.GetDoor(hit.collider);
        if (door != null)
        {
            if (vertical > 0)
            {
                door.DeactiveDoor();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        

        Brick brick = Cache.GetBrick(other);
        if (brick != null && brick.eColor == this.eColor)
        {
            brick.DeactiveBrick();
            AddBrick();
        }
    }


    public void AddBrick()
    {
        GameObject SpawnBrick = Instantiate(brickPrefab, transform);
        currentPosBrick.y += .3f;
        SpawnBrick.transform.localPosition = currentPosBrick;
        stackBricks.Push(SpawnBrick);
        //Debug.Log(stackBricks.Count);
    }
    private void RemoveBrick()
    {
        if (stackBricks.Count == 0) return;

        Destroy(stackBricks.Pop());

        currentPosBrick.y -= .3f;
    }
    private void ClearBrick()
    {
        if (stackBricks.Count == 0) return;

        while (stackBricks.Count > 0)
        {
            Destroy(stackBricks.Pop());
            currentPosBrick.y -= .3f;
        }
    }

}
