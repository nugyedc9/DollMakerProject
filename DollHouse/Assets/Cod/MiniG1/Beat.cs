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
        if(Input.GetKeyDown(keyToPress))
        {
                MiniG2.Instance.Workingnow();
                Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "HeadBeat")
        {   
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "HeadBeat")
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
            SpawnBeat.Instance.missTake();
            Destroy(gameObject);
        }
    }

    public void ExitMini1()
    {
        Destroy(gameObject);
    }

}
