using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Windows;

public class PlayerLook : MonoBehaviour
{
    public CinemachineVirtualCamera[] cam;
    public GameObject[] CamHold;
    private float xRotation = 0f;

    public float xSensetivity = 30f;
    public float ySensetivity = 30f;



    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        xRotation -= (mouseY * Time.deltaTime) * ySensetivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam[0].transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        CamHold[0].transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensetivity);
    }

    public void ProcesslookOnDesk(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        xRotation += (mouseX * Time.deltaTime) * ySensetivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam[1].transform.localRotation = Quaternion.Euler(0f, xRotation, 0f);
        //CamHold[1].transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensetivity);
    }

}
