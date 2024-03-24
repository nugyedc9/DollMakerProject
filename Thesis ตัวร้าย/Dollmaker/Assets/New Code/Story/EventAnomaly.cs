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

    public bool withoutBlock;
 public bool LookAtTarget;

    public UnityEvent GranmaSeeYou;
    public UnityEvent DoorInSwingRoom;
    public UnityEvent LittleGirlOut;
    public UnityEvent LittleGirlOutBehind;

     
    private bool IsVisible(Camera c, GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = target.transform.position;
        
        Vector3 cameraPosition = c.transform.position;
        Vector3 direction = point - cameraPosition;
        float distance = direction.magnitude;


        foreach (var plane in planes)
        {

            if(plane.GetDistanceToPoint(point) < 0)
            {
                return false;
            }
        }

        if (!withoutBlock)
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraPosition, direction.normalized, out hit, distance))
            {
                if (hit.collider.gameObject != target)
                {
                    return false;
                }
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
                LookAtTarget = true;
                DestroyOBJDelay = 3;
                IDEvent = 0;
            }
            if(IDEvent == 2)
            {
                LookAtTarget = true;
            }
            if(IDEvent == 3)
            {
                LittleGirlOut.Invoke();
                LookAtTarget = true;
                IDEvent = 0;
            }
            if(IDEvent == 4)
            {
                LookAtTarget = true;
                LittleGirlOutBehind.Invoke();
                IDEvent = 0;
            }

        }
        else
        {
            if (IDEvent == 2 && LookAtTarget)
            {
                DoorInSwingRoom.Invoke();
                IDEvent = 0;
            }
            if(IDEvent == 3)
            {
                IDEvent = 4;
            }
          

        }

          if(DestroyOBJDelay > 0&& LookAtTarget) DestroyOBJDelay -= Time.deltaTime;
          else if (DestroyOBJDelay < 0 )
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
