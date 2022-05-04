using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool moving = true;

    bool isRunning = false;

    private float currentSpeed;
    private float moveSpeed;
    private float runSpeed;

    Vector2 moveDir;
    Vector2 mousePos;

    Camera cam;
    Rigidbody2D rb;
    PlayerStats playerStats;

    private void Awake()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Start()
    {
        moveSpeed = playerStats.moveSpeed;
        runSpeed = playerStats.runSpeed;
    }

    void Update()
    {
        if (moving)
        {
            ProcessInputs();
        }
        MovementCheck();
        RotateToCamera();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void SetMoving(bool val)
    {
        moving = val;
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        moveDir = new Vector2(moveX, moveY).normalized;
    }

    private void MovePlayer()
    {

        if (!isRunning)
        {
            currentSpeed = moveSpeed;
            rb.velocity = new Vector2((Mathf.Lerp(0, moveDir.x * currentSpeed, moveSpeed)), Mathf.Lerp(0, moveDir.y * currentSpeed, moveSpeed));
        }
        else if (isRunning)
        {
            currentSpeed = runSpeed;
            rb.velocity = new Vector2((Mathf.Lerp(0, moveDir.x * currentSpeed, runSpeed)), Mathf.Lerp(0, moveDir.y * currentSpeed, runSpeed));
        }
    }

    void MovementCheck()
    {
        float moveChecker = Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Vertical");

        if(moveChecker == 0)
        {
            moving = false;
        }
        else
        {
            moving = true;
        }
    }

    private void RotateToCamera()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mousePos.y - transform.position.y), (mousePos.x - transform.position.x)) * Mathf.Rad2Deg);
    }
}
