using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerMap : MonoBehaviour
{
    public Transform Player;
    public Transform[] PosOnMap;
  //  public Vector3 Offset;
    int posid;
    public int PosId { get { return posid; } set { posid = value; } }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = target.transform.position + Offset;
    }

    public void PlayerOnMap()
    {
        Player.position = PosOnMap[PosId].position;
    }

}
