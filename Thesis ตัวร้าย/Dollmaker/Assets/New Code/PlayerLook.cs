using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Windows;

public class PlayerLook : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    public CinemachineVirtualCamera[] camHide;
    public GameObject[] CamHold;
    private PlayerChangeCam PCam;
    private float xRotation = 0f;

    public float xSensetivity = 30f;
    public float ySensetivity = 30f;

    private void Awake()
    {
        PCam = FindAnyObjectByType<PlayerChangeCam>();
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        xRotation -= (mouseY * Time.deltaTime) * ySensetivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        CamHold[0].transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensetivity);
    }

    public void ProcesslookOnDesk(Vector2 input)
    {

        if ( PCam.IDInterect.id >= 0 && PCam.IDInterect.id < PCam.HideSpot.Count) { 
            float mouseX = input.x;
            float mouseY = input.y;
            xRotation += (mouseX * Time.deltaTime) * ySensetivity;
            if (PCam.IDInterect.id == 0)
                xRotation = Mathf.Clamp(xRotation, 0f, 160f);
            if (PCam.IDInterect.id == 1)
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);
            if (PCam.IDInterect.id == 2)
                xRotation = Mathf.Clamp(xRotation, 80f, 270f);
             if(PCam.IDInterect.id == 3)
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);
            camHide[PCam.IDInterect.id].transform.localRotation = Quaternion.Euler(0f, xRotation, 0f);
            //CamHold[1].transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensetivity);
        }
    }

}
