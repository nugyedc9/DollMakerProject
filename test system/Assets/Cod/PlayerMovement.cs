using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    float stopMove = 0;

    public float groundDrag;
    public float playerHeight;
    public LayerMask Ground;
    bool Grounded;
    bool canWalk = true;

    public Transform orientation;

    float horizontalInput;
    float vericalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        if(canWalk)
        {
        Grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);

        MyInput();
        SpeedControl(); 

        if (Grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
        }

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        vericalInput = Input.GetAxisRaw("Vertical");
    }

    public void MovePlayer()
    {
        moveDirection = orientation.forward * vericalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVal = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVal.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVal.normalized * moveSpeed;    
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public void StopMove()
    {
        rb.AddForce(moveDirection.normalized * stopMove, ForceMode.Force);
    }

    public  void walkAble()
    {
        canWalk = true;
    }

    public void Stopwalk()
    {
        canWalk = false;
    }
}
