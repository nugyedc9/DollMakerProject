using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairClimb : MonoBehaviour
{

    Rigidbody rigidBody;
     [SerializeField] GameObject stepRayUpper;
     [SerializeField] GameObject stepRayLower;
     [SerializeField] float stepHeight = 0.3f;
     [SerializeField] float stepSmooth = 2f;
    public LayerMask layerMask;


    private void Awake()
     {
         rigidBody = GetComponent<Rigidbody>();

         stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z);
     }

     private void FixedUpdate()
     {
         stepClimb();
       /* Debug.DrawRay(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward) * 0.3f, Color.green);
        Debug.DrawRay(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward) * 0.5f, Color.red);
        Debug.DrawRay(stepRayLower.transform.position, transform.TransformDirection(1.5f, 0, 1) * 0.3f, Color.yellow);
        Debug.DrawRay(stepRayUpper.transform.position, transform.TransformDirection(1.5f, 0, 1) * 0.5f, Color.blue);*/
    }

     void stepClimb()
     {
         RaycastHit hitLower;
        //LayerMask layerMask = ~LayerMask.GetMask("Wall");
        if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.3f,layerMask) || Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(Vector3.back), out hitLower, 0.3f,layerMask))
         {
            RaycastHit hitUpper;
             if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.5f,layerMask)|| !Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.back), out hitUpper, 0.5f, layerMask))
             {
                rigidBody.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
             }
         }

         RaycastHit hitLower45;
         if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitLower45, 0.3f,layerMask) || Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitLower45, 0.3f, layerMask))
         {                  
            RaycastHit hitUpper45;
             if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitUpper45, 0.5f, layerMask) || !Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitUpper45, 0.5f, layerMask))
             {  
                 rigidBody.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
             }
         }

         RaycastHit hitLowerMinus45;
         if (Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitLowerMinus45, 0.3f, layerMask) || Physics.Raycast(stepRayLower.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitLowerMinus45, 0.3f, layerMask))
         {
            RaycastHit hitUpperMinus45;
             if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitUpperMinus45, 0.5f, layerMask) || !Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitUpperMinus45, 0.5f, layerMask ))
             {
                rigidBody.position -= new Vector3(0f, -stepSmooth * Time.deltaTime, 0f);
             }
         }
     }
}
