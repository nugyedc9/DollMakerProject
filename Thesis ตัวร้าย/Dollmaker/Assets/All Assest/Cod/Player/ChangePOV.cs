using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class ChangePOV
{
    static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>(); 

    public static CinemachineVirtualCamera ActiveCamera = null;
        

    public static bool IsActiveCamera(CinemachineVirtualCamera cam)
    {
        return cam == ActiveCamera;
    }

    public static void SwitchCamera(CinemachineVirtualCamera cam)
    {
        cam.Priority = 10;
        ActiveCamera = cam;

        foreach (CinemachineVirtualCamera v in cameras)
        {
            if(v != cam && v.Priority != 0)
            {
                v.Priority = 0;
            }
        }
    }

    public static void Register(CinemachineVirtualCamera cam)
    {
        cameras.Add(cam);
        //Debug.Log("camera Register: " + cam);
    }

    public static void UnRegister(CinemachineVirtualCamera cam)
    {
        cameras.Remove(cam);
        //Debug.Log("camera UnRegister: " + cam);
    }
}
