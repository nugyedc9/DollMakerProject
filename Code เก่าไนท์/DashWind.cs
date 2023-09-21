using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashWind : MonoBehaviour
{
    public Transform orientation;
    public Transform playercam;
    private Rigidbody rbDash;
    private FirstPersonController PDash;
    AudioSource AudioP;

    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;

    public float dashCD;
    private float dashCDTimer;


    public bool useCamForward = true;
    public bool allowAllDirection = true;
    

    public KeyCode dashKey = KeyCode.LeftShift;


    private void Start()
    {
        rbDash = GetComponent<Rigidbody>();
        PDash = GetComponent<FirstPersonController>();
        AudioP = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey))
            Dash();

        if (dashCDTimer > 0)
            dashCDTimer -= Time.deltaTime;
    }


    private void Dash()
    {
        Transform forwardT;

        if (dashCDTimer > 0) return;
        else dashCDTimer = dashCD;

        if (useCamForward)
            forwardT = playercam;
        else
            forwardT = orientation;

        Vector3 direction = GetDiretion(forwardT);

        Vector3 forceToApply = direction * dashForce + orientation.up * dashUpwardForce;

        rbDash.AddForce(forceToApply, ForceMode.Impulse);

        Invoke(nameof(ResetDash), dashDuration);
    }

    private void ResetDash()
    {
        
    }

    private Vector3 GetDiretion(Transform forwardT)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();

        if (allowAllDirection)
            direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        else
            direction = forwardT.forward;

        if (verticalInput == 0 && horizontalInput == 0)
            direction = forwardT.forward;

        return direction.normalized;


    }
}
