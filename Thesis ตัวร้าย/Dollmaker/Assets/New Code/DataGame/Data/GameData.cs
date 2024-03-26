using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventoryManager;
using static UnityEditor.Progress;

[System.Serializable]

public class GameData   
{

    public Vector3 playerPoS;

    // public SerializableDictionary<Item, int> Getitem;
    public List<Datainventoryslot> InventorySaveData;
    public InventoryData inventoryData;

    public GameData()
    {
        playerPoS = new Vector3 (-126.833f, 7.244f, -39.285f);
        InventorySaveData = new List<Datainventoryslot>();
    }



}
