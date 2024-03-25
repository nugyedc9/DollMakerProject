using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float SpeedForRun = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    private float NomalSpeed;
    public bool speedForTest;

    public Slider StaminaBar;
    public float Stamina;
    float MaxStamina;
    bool RunOutSt, Notrun, playRunSound, PLayWalkSound;
    public PlayerAttack PAttack;

    [Header("Sound")]
    public AudioSource AudioOut;
    public AudioClip WalkSound, RunSound;

    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        NomalSpeed = speed;
        AudioOut.enabled = false;
        MaxStamina = Stamina;
        StaminaBar.maxValue = MaxStamina;
        Notrun = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        StaminaBar.value = Stamina;

        if (Notrun)
        {
            if (Stamina < MaxStamina)
            {
                Stamina += 0.5f * Time.deltaTime;
            }
            else if (Stamina >= Stamina / 3)
            {
                RunOutSt = false;
            }
        }

        if (speedForTest)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                PAttack.Run = true;
                Notrun = false;
                if (!RunOutSt)
                {
                    if (Stamina > 0)
                    {
                        speed = SpeedForRun;
                        Stamina -= Time.deltaTime;
                    }
                }

                 if (Stamina <= 0 )
                {
                    speed = NomalSpeed;
                    RunOutSt = true;
                    Stamina += 0.5f * Time.deltaTime;
                }
                else if( Stamina >= Stamina / 3)
                {
                    RunOutSt = false;
                }

            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = NomalSpeed;
                PAttack.Run = false;
                Notrun = true;
            }
        }

        #region SoundWalk
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Notrun)
            { 
                AudioOut.clip = WalkSound;
                AudioOut.enabled = true;
                playRunSound = false;
                if (!PLayWalkSound)
                {
                    AudioOut.Play();
                    PLayWalkSound = true;
                }
            }
            else
            {
                AudioOut.clip = RunSound;
                if (!playRunSound)
                {
                    AudioOut.Play();
                    playRunSound = true;
                }
                PLayWalkSound = false;
            }
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            AudioOut.enabled = false;
        }
        #endregion


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
