using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerMap : MonoBehaviour
{
    public Transform target;
    public Vector3 Offset;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + Offset;
    }
}
