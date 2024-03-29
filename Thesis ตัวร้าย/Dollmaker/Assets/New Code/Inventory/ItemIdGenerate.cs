using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIdGenerate : MonoBehaviour
{
    [SerializeField] public string id;
    public bool GenerateAwake;
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

}
