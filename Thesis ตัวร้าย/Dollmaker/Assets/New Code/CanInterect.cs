using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class CanInterect : MonoBehaviour
{

    [Header("Ghost vision cone")]
    public GameObject canInterect;
    public float VisionRange;
    public float VisionAngle;
    public LayerMask PlayerLayer;
    public int VisionConeResolution = 360;

    private void Start()
    {
        #region Vision Cone
        VisionAngle *= Mathf.Deg2Rad;
        #endregion
    }
    // Update is called once per frame
    void Update()
    {
        DrawVisionCone();     
    }

    public void DrawVisionCone()
    {
        int[] triangles = new int[(VisionConeResolution - 1) * 3];
        Vector3[] Vertices = new Vector3[VisionConeResolution + 1];
        Vertices[0] = Vector3.zero;
        float Currentangle = -VisionAngle / 2;
        float angleIcrement = VisionAngle / (VisionConeResolution - 1);
        float Sine;
        float Cosine;

        for (int i = 0; i < VisionConeResolution; i++)
        {
            Sine = Mathf.Sin(Currentangle);
            Cosine = Mathf.Cos(Currentangle);
            Vector3 RaycastDirection = (transform.forward * Cosine) + (transform.right * Sine);
            Vector3 VertForward = (Vector3.forward * Cosine) + (Vector3.right * Sine);
            if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit hit, VisionRange, PlayerLayer))
            {
                Vertices[i + 1] = VertForward * hit.distance;
                canInterect.SetActive(true);
            }
            else canInterect.SetActive(false);



            Currentangle += angleIcrement;

        }
            for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
            {
                triangles[i] = 0;
                triangles[i + 1] = j + 1;
                triangles[i + 2] = j + 2;
            }
        
        
    }
 
}
