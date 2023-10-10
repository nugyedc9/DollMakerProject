using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanPlayMini1 : MonoBehaviour
{

    public GameObject canClick;
    public bool Cloth, Doll;
    public GameObject Dollobj;
    public GameObject Clothobj;

    // Start is called before the first frame update
    void Start()
    {
        Dollobj.SetActive(false);
        Clothobj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Cloth && Doll)
        {
            canClick.SetActive(true);
        }
        else
            canClick.SetActive(false);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cloth")
        {
            Cloth = true;
            Clothobj.SetActive(true);
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.tag == "Doll")
        {
            Doll = true;
            Dollobj.SetActive(true);
            Destroy(collision.gameObject);
        }
    }


}
