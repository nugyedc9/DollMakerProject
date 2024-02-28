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
    private bool CanWalk = true, Ondesk, HoldSpace;

    [SerializeField] bool onTab;
    public bool OnTab { get { return onTab; } set {  onTab = value; } }

    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        onLook = playerInput.OnLook;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
       // onFoot.Jump.performed += ctx => motor.Jump();
    }
    // Update is called once per frame

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) HoldSpace = true;
        else if (Input.GetKeyUp(KeyCode.Space)) HoldSpace = false;
    }

    void FixedUpdate()
    {
        if (CanWalk)
        {
            motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        }
    }
    private void LateUpdate()
    {
        if (!Ondesk && !OnTab)
            look.ProcessLook(onLook.Look.ReadValue<Vector2>());
/*        else
        {
            if(!HoldSpace)
            look.ProcesslookOnDesk(onLook.Look.ReadValue<Vector2>());
        }*/
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
            Ondesk = true;
            CanWalk = false;           
        }
        else
        {
            Ondesk = false;
            CanWalk = true;
        }
        
    }

}
