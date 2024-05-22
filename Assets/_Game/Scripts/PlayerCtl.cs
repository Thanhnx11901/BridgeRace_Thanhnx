using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtl : CharacterCtl
{
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private GameObject brickPrefab;

    private Stack<GameObject> stackBricks = new Stack<GameObject>();
    private List<GameObject> ListStairs = new List<GameObject>();


    private CharacterController controller;

    [SerializeField] private Material CurrentMateral;

    private Vector3 moveDirection;
    private Vector3 currentPosBrick;


    public float speed = 6.0f;
    public float gravity = 20.0f;
    public float rotationSpeed = 700.0f;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentPosBrick = new Vector3(0,1.2f,-.6f);
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            // Lấy đầu vào từ joystick hoặc bàn phím
            float horizontal = joystick.Horizontal;
            float vertical = joystick.Vertical;

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

        }

        // Áp dụng trọng lực
        moveDirection.y -= gravity * Time.deltaTime;

        // Di chuyển nhân vật
        controller.Move(moveDirection * Time.deltaTime);
    }

    

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Brick"))
        {
            Brick brick = hit.collider.GetComponent<Brick>();

            brick.DestroyBrick();

            AddBrick();

        }else if (hit.collider.CompareTag("Stair"))
        {
            Stair stair = hit.collider.GetComponent<Stair>();
            stair.ActiveMeshRenderer(CurrentMateral);
            //RemoveBrick();

        }
    }

    private void AddBrick()
    {
        GameObject SpawnBrick = Instantiate(brickPrefab, transform);
        currentPosBrick.y += .3f;
        SpawnBrick.transform.localPosition = currentPosBrick;
        stackBricks.Push(SpawnBrick);
        Debug.Log(stackBricks.Count);
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
