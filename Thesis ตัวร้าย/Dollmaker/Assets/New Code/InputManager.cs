using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    private PlayerInput.OnLookActions onLook;
    private PlayerMotor motor;
    private PlayerLook look;
    private bool CanWalk = true;
    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        onLook = playerInput.OnLook;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        onFoot.Jump.performed += ctx => motor.Jump();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        look.ProcessLook(onLook.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
        onLook.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
        onLook.Disable();
    }

    public void StopWalk()
    {
        if (CanWalk)
        {
            CanWalk = false;
            look.camNum(0);
            onFoot.Disable();
        }
        else
        {
            onFoot.Enable();
            look.camNum(0);
            CanWalk = true;
        }
        
    }

}
