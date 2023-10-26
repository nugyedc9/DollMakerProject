using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beat : MonoBehaviour
{

    public bool canBePressed;
    public KeyCode keyToPress;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                MiniG2.Instance.Workingnow();
                Destroy(gameObject);
            }
        }
        if ( !transform.parent.gameObject.activeSelf)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BeatA")
        {   

            canBePressed = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "BeatA")
        {

            canBePressed = false;
            StartCoroutine(DelayDestroy());
        }
    }

    IEnumerator DelayDestroy()
    {
        while(true)
        {

            yield return new WaitForSeconds(0.1f);
          //  SpawnBeat.Instance.missTake();
            Destroy(gameObject);
        }
    }

    public void ExitMini1()
    {
        Destroy(gameObject);
    }

}
