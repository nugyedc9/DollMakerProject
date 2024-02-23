using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    private float NomalSpeed;
    public bool speedForTest;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        NomalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        if (Input.GetKeyDown(KeyCode.P))
        {
            speed = 10;
        }
        if (speedForTest)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                speed = 10;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = NomalSpeed;
            }
        }
    }

    public void ProcessMove(Vector2 Input)
    {
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = Input.x;
            moveDirection.z = Input.y;
            controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
            playerVelocity.y += gravity * Time.deltaTime;
            if (isGrounded && playerVelocity.y < 0)
                playerVelocity.y = -2f;
            controller.Move(playerVelocity * Time.deltaTime);        
    }

    public void Jump()
    {
            if (isGrounded)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            }       
    }


}
