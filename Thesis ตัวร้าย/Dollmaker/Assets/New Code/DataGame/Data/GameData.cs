using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static InventoryManager;
using static PlayerPickUpItem;
using static UnityEditor.Progress;

[System.Serializable]

public class GameData   
{

    public Vector3 playerPoS, flashLightPos;
    public quaternion PlayerRota, flashLightRota;

    // public SerializableDictionary<Item, int> Getitem;
    public List<Datainventoryslot> InventorySaveData;
    public Dictionary<string, bool> ItemOnScene;
    public InventoryData inventoryData;
    public List<string> pickedUpItemIds;
    public List<string> EventStroyPass;
    public List<GameObject> FinishdollSave;

    public GameData()
    {
        playerPoS = new Vector3 (-126.833f, 7.244f, -39.285f);
        PlayerRota = new quaternion(0, 180, 0, 0);
        flashLightPos = new Vector3 (-126.743f, 7.862f, -39.57f);
        flashLightRota = new quaternion(0, 180, 0, 0);
        InventorySaveData = new List<Datainventoryslot>();
        pickedUpItemIds = new List<string>();
        ItemOnScene = new Dictionary<string, bool>();
        EventStroyPass = new List<string>();  
    }



}
