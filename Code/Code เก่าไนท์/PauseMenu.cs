using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
   public Canvas canvas;

     void Start()
    {
        canvas = GetComponent<Canvas>();
    }

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.enabled = !canvas.enabled;
            pause();
        }
    }

    public void pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;      
    }

     
    


}
