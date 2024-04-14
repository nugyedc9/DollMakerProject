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
    public int storyCountSave, FinishDollInInv, FinishCloth, failCloth, KeyID,
        DollsCoveredCount;
    public float CurCrossHP, DollCountData;

    // public SerializableDictionary<Item, int> Getitem;
    public List<Datainventoryslot> InventorySaveData;
    public List<ItemDropData> itemDropDatas;
    public List<GameObject> ItemDropObj;

    public Dictionary<string, bool> ItemOnScene;
    public SerializableLightOn<string, bool> LightOn;
   
    public List<string> pickedUpItemIds;
    public List<string> EventStroyPass;
    public List<int> FinishDollOnBasket;
    public bool flashLighGet, Note1Get, Note2Get, OpenWall1, GhostDied1, GhostTakeDoll3, Ghost2Spawn,
        _1Sleep, GhostSpawnAfterCovered2, GhostSpawnAfterCovered3, MakeDoll2, GhostDied2, Ghost4DiedEvent,
        FinishDoll6;
    
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
        crossCheck = new CrossCheck();
        LightOn = new SerializableLightOn<string, bool>();

 flashLighGet = false;
        Note1Get = false;
        Note2Get = false;
        OpenWall1 = false;
        GhostDied1 = false;
        GhostTakeDoll3 = false;
        Ghost2Spawn= false;
        _1Sleep = false;
        GhostSpawnAfterCovered2 = false;
        GhostSpawnAfterCovered3 = false;
        MakeDoll2 = false;
        Ghost4DiedEvent = false;
        FinishDoll6 = false;

         storyCountSave = 0;
        FinishDollInInv = 0;
        KeyID = 0;
        CurCrossHP = 0;
        DollCountData = 0;
        FinishCloth = 0;
        failCloth = 0;
        DollsCoveredCount = 0;


    }



}
