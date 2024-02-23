using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutLineFail : MonoBehaviour
{
    public MiniGameAuidition miniAudition;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Scissors")
        {
            miniAudition.cutLine = true;
        }
    }

}
