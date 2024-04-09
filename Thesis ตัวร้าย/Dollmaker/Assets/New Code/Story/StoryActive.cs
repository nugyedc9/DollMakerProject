using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class StoryActive : MonoBehaviour
{

    public float DelayBoxShow;
    public bool LookActiveEvent;
    [SerializeField] int Id;
    public int ID { get { return Id; }  }
    public UnityEvent EventActive;
    private BoxCollider Box;
    private PlayerChangeCam Pcam;

    [SerializeField] public string id;
    [ContextMenu("Generate grid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public void Awake()
    {
        Box = GetComponent<BoxCollider>();
        Pcam = GameObject.FindObjectOfType<PlayerChangeCam>();
    }

    public void OnTriggerEnter(Collider other)
    {
       
        if(other.gameObject.tag == "Player")
        {
            EventActive.Invoke();
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        if(DelayBoxShow > 0)
        {
            DelayBoxShow -= Time.deltaTime;
        }
        else if (DelayBoxShow < 0)
        {
            Box.enabled = true;
            DelayBoxShow = 0;
        }
    }

    public void DeleteAnother()
    {
        Destroy(gameObject);
    }

    public void LookActiveevent()
    {
        if (LookActiveEvent)
        {
            EventActive.Invoke();
            Destroy(gameObject);
        }
    }

}
