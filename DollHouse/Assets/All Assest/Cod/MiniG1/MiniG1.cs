using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniG1 : MonoBehaviour
{

    public float Beat;
    public bool hasStarted;

    // Start is called before the first frame update
    void Start()
    {
        Beat = Beat * 10f;
    }

    // Update is called once per frame
    void Update()
    {
       /*if (!hasStarted)
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                hasStarted = true;
            }
        }
        else
        {
            transform.position -= new Vector3(Beat   * Time.deltaTime, 0f , 0f );
        }*/

        transform.position -= new Vector3(Beat * Time.deltaTime, 0f, 0f); 
    }


}