using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventAnomaly : MonoBehaviour
{
    public GameObject target;
    public Camera cam;
    public int IDEvent;
    public float DestroyOBJDelay;

    public UnityEvent GranmaSeeYou;
    public UnityEvent DoorInSwingRoom;

    bool LookAtTarget;

    private bool IsVisible(Camera c, GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = target.transform.position;

        foreach (var plane in planes)
        {
            if(plane.GetDistanceToPoint(point) < 0)
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
    {
          if(IsVisible(cam, target))
        {

            if (IDEvent == 1)
            {
                GranmaSeeYou.Invoke();
                IDEvent = 0;
            }
            if(IDEvent == 2)
            {
                LookAtTarget = true;
            }

        }
        else
        {
            if (IDEvent == 2 && LookAtTarget)
            {
                DoorInSwingRoom.Invoke();
                IDEvent = 0;
            }

        }

          if(DestroyOBJDelay > 0) DestroyOBJDelay -= Time.deltaTime;
          else if (DestroyOBJDelay < 0)
        {
            Destroy(target);
            DestroyOBJDelay = 0;
        }

    }


    public void DelayDeleteOBJ(float Delay)
    {
        DestroyOBJDelay = Delay;
    }


}
