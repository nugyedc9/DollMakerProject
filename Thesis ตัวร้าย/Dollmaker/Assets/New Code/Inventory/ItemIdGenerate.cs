using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ItemIdGenerate : MonoBehaviour
{
    [SerializeField] public string id;
    public bool GenerateAwake, DestoryToPlayEvent;
    public UnityEvent EventAfterDestory;


    [ContextMenu("Generate grid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public void Awake()
    {
        if (GenerateAwake)
        {
            GenerateGuid();
        }
    }


    public void OnDestroy()
    {
        if (DestoryToPlayEvent)
        {
            EventAfterDestory.Invoke();
            DestoryToPlayEvent = false;
        }
    }


}
