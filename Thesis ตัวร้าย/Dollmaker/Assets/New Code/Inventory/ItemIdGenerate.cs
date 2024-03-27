using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIdGenerate : MonoBehaviour
{
    [SerializeField] public string id;
    [ContextMenu("Generate grid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

}
