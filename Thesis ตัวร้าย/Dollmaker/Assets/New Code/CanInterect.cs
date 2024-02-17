using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class CanInterect : MonoBehaviour
{

    [Header("Ghost vision cone")]
    public GameObject HeadVistion;
    public Material VisionConeMaterial;
    public float VisionRange, HearRange;
    public float VisionAngle;
    public LayerMask VisionObstructingLayer;
    public LayerMask PlayerLayer;
    public int VisionConeResolution = 120;
    public bool Cansee;
    Mesh VisionConeMesh;
    MeshFilter MeshFilter_;

    bool hitPLayer;

    public Canvas CanIterectUI; 
    public LayerMask groundLayer; 

    private bool isThrown = false;
    private bool isCanvasAboveObject = false;


    private void Start()
    {
        #region Vision Cone
        transform.AddComponent<MeshRenderer>().material = VisionConeMaterial;
        MeshFilter_ = transform.AddComponent<MeshFilter>();
        VisionConeMesh = new Mesh();
        VisionAngle *= Mathf.Deg2Rad;
        #endregion
    }
    // Update is called once per frame
    void Update()
    {
        DrawVisionCone();

        if (isThrown && !isCanvasAboveObject)
        {
            print("MakeIt");
            CheckIfCanvasAboveObject();
        }
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

        if (Cansee)
        {
            for (int i = 0; i < VisionConeResolution; i++)
            {
                Sine = Mathf.Sin(Currentangle);
                Cosine = Mathf.Cos(Currentangle);
                Vector3 RaycastDirection = (transform.forward * Cosine) + (transform.right * Sine);
                Vector3 VertForward = (Vector3.forward * Cosine) + (Vector3.right * Sine);
                if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit hit, VisionRange, VisionObstructingLayer))
                {
                    Vertices[i + 1] = VertForward * hit.distance;
                }
                else
                {

                    Vertices[i + 1] = VertForward * VisionRange;
                    if (Physics.Raycast(transform.position, RaycastDirection, out hit, VisionRange, PlayerLayer))
                    {
                        Vertices[i + 1] = VertForward * hit.distance;
                        hitPLayer = true;
                    }
                    else hitPLayer = false;

                }


                Currentangle += angleIcrement;

            }
            for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
            {
                triangles[i] = 0;
                triangles[i + 1] = j + 1;
                triangles[i + 2] = j + 2;
            }
            VisionConeMesh.Clear();
            VisionConeMesh.vertices = Vertices;
            VisionConeMesh.triangles = triangles;
            MeshFilter_.mesh = VisionConeMesh;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isThrown = true;
        }
    }

    void CheckIfCanvasAboveObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            if (hit.collider.CompareTag("InterectItemTell"))
            {
                isCanvasAboveObject = true;
                UpdateCanvasPositionAndRotation(hit.point);
            }
        }
    }

    void UpdateCanvasPositionAndRotation(Vector3 position)
    {

        CanIterectUI.transform.position = position + Vector3.up * 2f;


        CanIterectUI.transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
    }
}
