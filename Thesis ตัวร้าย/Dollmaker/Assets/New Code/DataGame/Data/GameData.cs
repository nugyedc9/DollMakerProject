using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static InventoryManager;
/*using static PlayerPickUpItem;
using static UnityEditor.Progress;*/

[System.Serializable]

public class GameData   
{

    public Vector3 playerPoS, flashLightPos;
    public quaternion PlayerRota, flashLightRota;
    public int storyCountSave, FinishDollInInv, KeyID;
    public float CurCrossHP;

    // public SerializableDictionary<Item, int> Getitem;
    public List<Datainventoryslot> InventorySaveData;
    public List<ItemDropData> itemDropDatas;
    public List<GameObject> ItemDropObj;

    public Dictionary<string, bool> ItemOnScene;
   
    public List<string> pickedUpItemIds;
    public List<string> EventStroyPass;
    public List<int> FinishDollOnBasket;
    public bool flashLighGet, Note1Get, OpenWall1, GhostDied1;
    
    public InventoryData inventoryData;
    public CrossCheck crossCheck;


    public GameData()
    {
        playerPoS = new Vector3 (-126.833f, 7.244f, -39.285f);
        PlayerRota = new quaternion(0, 180, 0, 0);
        flashLightPos = new Vector3 (-126.743f, 7.862f, -39.57f);
        flashLightRota = new quaternion(0, 180, 0, 0);


        InventorySaveData = new List<Datainventoryslot>();
        itemDropDatas = new List<ItemDropData>();
        ItemDropObj = new List<GameObject>();
        pickedUpItemIds = new List<string>();
        ItemOnScene = new Dictionary<string, bool>();
        EventStroyPass = new List<string>();  
        FinishDollOnBasket = new List<int>();   



        storyCountSave = 0;
        flashLighGet = false;
        Note1Get = false;
        OpenWall1 = false;
        GhostDied1 = false;
        FinishDollInInv = 0;
        KeyID = 0;
        CurCrossHP = 0;
        crossCheck = new CrossCheck();

    }



}
