using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventoryManager;
using static PlayerPickUpItem;
using static UnityEditor.Progress;

[System.Serializable]

public class GameData   
{

    public Vector3 playerPoS, flashLightPos;

    // public SerializableDictionary<Item, int> Getitem;
    public List<Datainventoryslot> InventorySaveData;
    public Dictionary<string, bool> ItemOnScene;
    public InventoryData inventoryData;
    public List<string> pickedUpItemIds;

    public GameData()
    {
        playerPoS = new Vector3 (-126.833f, 7.244f, -39.285f);
        flashLightPos = new Vector3 (-126.743f, 7.862f, -39.57f);
        InventorySaveData = new List<Datainventoryslot>();
        pickedUpItemIds = new List<string>();
        ItemOnScene = new Dictionary<string, bool>();
    }



}
