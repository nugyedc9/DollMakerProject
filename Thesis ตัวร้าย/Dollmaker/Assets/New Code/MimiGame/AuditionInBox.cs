using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuditionInBox : MonoBehaviour
{

    public MiniGameAuidition AuditionGAme;
    // Start is called before the first frame update
    public int InSlot, AuditionNumber;
    private int now;
    private AuditionPrefab Getin;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        now = Getin.Number;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

            if (collision.gameObject.tag == "AuditionPrefabs")
            {
             Getin = collision.gameObject.GetComponent<AuditionPrefab>();
                AuditionNumber = Getin.Number;
                    AuditionGAme.AddBoxNumber(AuditionNumber);
            }
    }
        
}
