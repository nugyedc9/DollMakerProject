using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollClothColor : MonoBehaviour
{
    public int pieceClothID;
    public GameObject ItemPrefab;
    public GameObject SpawnPoint;


    public void DropitemPrefabs()
    {
        var DropObj = Instantiate(ItemPrefab, SpawnPoint.transform.position, Quaternion.identity) as GameObject;
        DropObj.GetComponent<Rigidbody>().velocity = (SpawnPoint.transform.position).normalized ;
    }


}
