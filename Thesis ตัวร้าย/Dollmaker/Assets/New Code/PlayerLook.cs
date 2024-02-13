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

    int num;

    public void Awake()
    {
        num = 0;    
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        xRotation -= (mouseY * Time.deltaTime) * ySensetivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam[num].transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        CamHold[num].transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensetivity);
    }


    public void camNum(int Num)
    {
        num = Num;
    }
}
