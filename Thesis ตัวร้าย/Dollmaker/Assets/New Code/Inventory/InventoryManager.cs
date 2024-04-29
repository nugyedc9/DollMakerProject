using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
/*using UnityEngine.UIElements;
using static InventoryManager;
using static UnityEditor.Progress;*/

public class InventoryManager : MonoBehaviour, IDataGame
{
    public static InventoryManager Instance;

    public InventoryData InvDataBase;
    public ItemDropDataCollect itemdropCollect;

public PlayerPickUpItem playerPickUpItem;
    public PlayerAttack pAttack;
    public TabTutorial tabTutorial;
    public PlayerChangeCam ChangeCam;
    public CrossAnim crossAnim;
    public GhostStateManager ghostStateManager;
    public Camera Cam;
    public FinishBasket Basket;
    public int maxstaxkitem;

    public InventorySlote[] inventoryslote;
    public List<Datainventoryslot> datainventorySlots = new List<Datainventoryslot>();
    public List<ItemDropData> itemDropDatas = new List<ItemDropData>();
    public List<GameObject> ItemDropOBj = new List<GameObject>();
    public GameObject[] ItemOnHand;
    public GameObject[] ItemPrefab;
    public float DropSpeed;
    public Transform DropPoint;
    public Vector3 DropPointPos;
    public GameObject inventoryItemPrefab;
    public TextMeshProUGUI CountText;

    inventoryItem DollCheckItem;



     [Header("CrossAction")]
    public Animator CorssAni;
    public GameObject CrossBar;
    [SerializeField] bool triggerCrossAnim;

    public Animator InvOpenAnim;
    public bool TriggerCrossAnim { get { return triggerCrossAnim; } set { triggerCrossAnim = value; } }

    [SerializeField] int SelectedSlot;
   public int selectedSlot { get { return SelectedSlot; } set { SelectedSlot = value; } }

    [SerializeField] int finishDollID;
    public int FinishDollID { get { return finishDollID; } set { finishDollID = value; } }

    [SerializeField] float dollCountFinish;
    public float DollCountFinish { get { return dollCountFinish; } set { dollCountFinish = value; } }

    float dollpushCount;
    public float DollpushCount { get { return dollpushCount; } set { dollpushCount = value; } }


    public UnityEvent Doll2CutScene,TakeDoll3, Ghost2Spawn, Ghost4Evnet, finishDoll6Event, Doll1Finish;

    private Vector3 DesDrop;
    bool drop,MakeDoll2 , take3data, Ghost2spawn, Ghost4Spawn, Finish6DollCheck,finishdoll1,haveDoll,DelayStart;
    float DelayGameOver;

    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        ChangeSelectedSlot(0);
        InvOpenAnim.enabled = false;
    }
    private void Update()
    {

        DropPointPos = DropPoint.position;

        

        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if(isNumber && number > 0 && number < 4)
            {
               
                ChangeSelectedSlot(number - 1);
            }
        }


        #region Item Show
        if (selectedSlot >= 0 && selectedSlot <= 5 && tabTutorial.OpenTutor == false && ChangeCam.camOnPerSon == true)
        {
            inventoryItem itemSlot = inventoryslote[selectedSlot].GetComponentInChildren<inventoryItem>();
            if (itemSlot != null && itemSlot.gameObject.CompareTag("Cross"))
            {
               // CrossBar.SetActive(true);
                pAttack.Attack = true;
                pAttack.CrossSlotOnHane = true;
                ItemOnHand[0].SetActive(true);
                if (triggerCrossAnim)
                {

                    crossAnim.SetState(CrossState.Idle);

                    triggerCrossAnim = false;
                }
                if (drop)
                {
                    DropitemPrefabs(DropPointPos, 0);
                    GetSelectedItem(true);
                    drop = false;
                }
            }
            else
            {
               // CrossBar.SetActive(false);
                pAttack.Attack = false;
                pAttack.CrossSlotOnHane = false;
                crossAnim.SetState(CrossState.Idle);
                ItemOnHand[0].SetActive(false);
                triggerCrossAnim = true;
            }

            if (itemSlot != null && itemSlot.gameObject.CompareTag("Shotgun"))
            {            
                pAttack.ShotgunOnhand = true;
                ItemOnHand[6].SetActive(true);

            }
            else
            {
                pAttack.ShotgunOnhand = false;
                ItemOnHand[6].SetActive(false);
            }


            if (itemSlot != null && itemSlot.gameObject.CompareTag("Doll"))
            {
                //ItemOnHand[1].SetActive(true);
                if (drop)
                {
                    DropitemPrefabs(DropPointPos, 1);
                    GetSelectedItem(true);
                    drop = false;
                }
               
            }
            else
            {
                ItemOnHand[1].SetActive(false);
            }

            if (itemSlot != null && itemSlot.gameObject.CompareTag("Scissors"))
            {
                playerPickUpItem.HaveScissor = true;
                ItemOnHand[2].SetActive(true);
                if (drop)
                {
                    DropitemPrefabs(DropPointPos, 2);
                    GetSelectedItem(true);
                    drop = false;
                }
            }
            else
            {
                playerPickUpItem.HaveScissor = false;
                ItemOnHand[2].SetActive(false);
            }

            if (itemSlot != null && itemSlot.gameObject.CompareTag("Cloth"))
            {
                ItemOnHand[3].SetActive(true);
                if (drop)
                {
                    DropitemPrefabs(DropPointPos, 3);
                    GetSelectedItem(true);
                    drop = false;
                }
            }
            else
            {
                ItemOnHand[3].SetActive(false);
            }

            #region ClothColor

            if (itemSlot != null && itemSlot.gameObject.CompareTag("RedCloth"))
            {
                //ItemOnHand[6].SetActive(true);
                if (drop)
                {
                    DropitemPrefabs(DropPointPos, 6);
                    GetSelectedItem(true);
                    drop = false;
                }
            }
            else
            {
               // ItemOnHand[3].SetActive(false);
            }

            if (itemSlot != null && itemSlot.gameObject.CompareTag("BlueCloth"))
            {
                //ItemOnHand[6].SetActive(true);
                if (drop)
                {
                    DropitemPrefabs(DropPointPos, 7);
                    GetSelectedItem(true);
                    drop = false;
                }
            }
            else
            {
                // ItemOnHand[3].SetActive(false);
            }
            if (itemSlot != null && itemSlot.gameObject.CompareTag("GreenCloth"))
            {
                //ItemOnHand[6].SetActive(true);
                if (drop)
                {
                    DropitemPrefabs(DropPointPos, 8);
                    GetSelectedItem(true);
                    drop = false;
                }
            }
            else
            {
                // ItemOnHand[3].SetActive(false);
            }
            if (itemSlot != null && itemSlot.gameObject.CompareTag("YellowCloth"))
            {
                //ItemOnHand[6].SetActive(true);
                if (drop)
                {
                    DropitemPrefabs(DropPointPos, 9);
                    GetSelectedItem(true);
                    drop = false;
                }
            }
            else
            {
                // ItemOnHand[3].SetActive(false);
            }

            #endregion


            #region PieceClothColor

            if (itemSlot != null && itemSlot.gameObject.CompareTag("PieceClothRed"))
            {
                //ItemOnHand[6].SetActive(true);
                if (drop)
                {
                    DropitemPrefabs(DropPointPos, 10);
                    GetSelectedItem(true);
                    drop = false;
                }
            }
            else
            {
                // ItemOnHand[3].SetActive(false);
            }

            if (itemSlot != null && itemSlot.gameObject.CompareTag("PieceClothBlue"))
            {
                //ItemOnHand[6].SetActive(true);
                if (drop)
                {
                    DropitemPrefabs(DropPointPos, 11);
                    GetSelectedItem(true);
                    drop = false;
                }
            }
            else
            {
                // ItemOnHand[3].SetActive(false);
            }
            if (itemSlot != null && itemSlot.gameObject.CompareTag("PieceClothGreen"))
            {
                //ItemOnHand[6].SetActive(true);
                if (drop)
                {
                    DropitemPrefabs(DropPointPos, 12);
                    GetSelectedItem(true);
                    drop = false;
                }
            }
            else
            {
                // ItemOnHand[3].SetActive(false);
            }
            if (itemSlot != null && itemSlot.gameObject.CompareTag("PieceClothYellow"))
            {
                //ItemOnHand[6].SetActive(true);
                if (drop)
                {
                    DropitemPrefabs(DropPointPos, 13);
                    GetSelectedItem(true);
                    drop = false;
                }
            }
            else
            {
                // ItemOnHand[3].SetActive(false);
            }

            #endregion

            #region FinishClothColor

            if (itemSlot != null && itemSlot.gameObject.CompareTag("FinishClothRed"))
            {
                //ItemOnHand[6].SetActive(true);
                if (drop)
                {
                    DropitemPrefabs(DropPointPos, 2);
                    GetSelectedItem(true);
                    drop = false;
                }
            }
            else
            {
                // ItemOnHand[3].SetActive(false);
            }

            if (itemSlot != null && itemSlot.gameObject.CompareTag("FinishClothBlue"))
            {
                //ItemOnHand[6].SetActive(true);
                if (drop)
                {
                    DropitemPrefabs(DropPointPos, 3);
                    GetSelectedItem(true);
                    drop = false;
                }
            }
            else
            {
                // ItemOnHand[3].SetActive(false);
            }
            if (itemSlot != null && itemSlot.gameObject.CompareTag("FinishClothGreen"))
            {
                //ItemOnHand[6].SetActive(true);
                if (drop)
                {
                    DropitemPrefabs(DropPointPos, 4);
                    GetSelectedItem(true);
                    drop = false;
                }
            }
            else
            {
                // ItemOnHand[3].SetActive(false);
            }
            if (itemSlot != null && itemSlot.gameObject.CompareTag("FinishClothYellow"))
            {
                //ItemOnHand[6].SetActive(true);
                if (drop)
                {
                    DropitemPrefabs(DropPointPos, 5);
                    GetSelectedItem(true);
                    drop = false;
                }
            }
            else
            {
                // ItemOnHand[3].SetActive(false);
            }

            #endregion

            if (itemSlot != null && itemSlot.gameObject.CompareTag("FinishDollRedBlue"))
            {
                FinishDollID = 0; playerPickUpItem.FDOnhand = true;
                ItemOnHand[7].SetActive(true);

               /* if (drop)
                {
                    DropitemPrefabs(DropPointPos, 19);
                    GetSelectedItem(true);
                    drop = false;
                }*/
            }
            else
            {
                playerPickUpItem.FDOnhand = false;
                ItemOnHand[7].SetActive(false);
            }
            if (itemSlot != null && itemSlot.gameObject.CompareTag("FinishDollGreen"))
            {
                FinishDollID = 1;


                if (itemSlot.Count > 0)
                {
                    playerPickUpItem.FDOnhand1 = true;
                    ghostStateManager.DollOnHand = true;
                    haveDoll = true;
                    ItemOnHand[8].SetActive(true);
                }
                else
                {
                    ghostStateManager.DollOnHand = false;
                    playerPickUpItem.FDOnhand1 = false;
                    haveDoll = false;
                }

                DollCheckItem = itemSlot;
                /*if (drop)
                {

                   // print("Drop");
                    DropitemPrefabs(DropPointPos, 20);
                    GetSelectedItem(true);
                    drop = false;
                }*/
            }
            else
            {
                playerPickUpItem.FDOnhand1 = false;
                ItemOnHand[8].SetActive(false);
                    ghostStateManager.DollOnHand = false;
                
            }

            if (itemSlot != null && itemSlot.gameObject.CompareTag("FinishDollYellow"))
            {
                FinishDollID = 2; playerPickUpItem.FDOnhand2 = true;
                ItemOnHand[9].SetActive(true);

               /* if (drop)
                {
                    DropitemPrefabs(DropPointPos, 21);
                    GetSelectedItem(true);
                    drop = false;
                }*/
            }
            else
            {
                playerPickUpItem.FDOnhand2 = false;
                ItemOnHand[9].SetActive(false);
            }

            if (itemSlot != null && itemSlot.gameObject.CompareTag("BloodyDollRed"))
            {
                FinishDollID = 3; playerPickUpItem.FDOnhand3 = true;
                ItemOnHand[10].SetActive(true);

               /* if (drop)
                {
                    DropitemPrefabs(DropPointPos, 22);
                    GetSelectedItem(true);
                    drop = false;
                }*/
            }
            else
            {
                playerPickUpItem.FDOnhand3 = false;
                ItemOnHand[10].SetActive(false);
            }
            if (itemSlot != null && itemSlot.gameObject.CompareTag("BloodyDollGreen"))
            {
                FinishDollID = 4; playerPickUpItem.FDOnhand4 = true;
                ItemOnHand[11].SetActive(true);

                /*if (drop)
                {
                    DropitemPrefabs(DropPointPos, 23);
                    GetSelectedItem(true);
                    drop = false;
                }*/
            }
            else
            {
                playerPickUpItem.FDOnhand4 = false;
                ItemOnHand[11].SetActive(false);
            }
            if (itemSlot != null && itemSlot.gameObject.CompareTag("BloodyDollYellow"))
            {
                FinishDollID = 5; playerPickUpItem.FDOnhand5 = true;
                ItemOnHand[12].SetActive(true);

               /* if (drop)
                {
                    DropitemPrefabs(DropPointPos, 24);
                    GetSelectedItem(true);
                    drop = false;
                }*/
            }
            else
            {
                playerPickUpItem.FDOnhand5 = false;
                ItemOnHand[12].SetActive(false);
            }



            if (itemSlot != null && itemSlot.gameObject.CompareTag("Key") || itemSlot != null && itemSlot.gameObject.CompareTag("Axe"))
            {
                if( !itemSlot.gameObject.CompareTag("Axe"))
                ItemOnHand[4].SetActive(true);
              //  playerPickUpItem.Key = true;
            }
            else
            {
                ItemOnHand[4].SetActive(false);
               // playerPickUpItem.Key = false;
            }


            if (itemSlot != null && itemSlot.gameObject.CompareTag("EyeWash"))
            {
                if (itemSlot.Count > 0)
                {
                    ItemOnHand[5].SetActive(true);
                    playerPickUpItem.HealOnhand = true;
                }
                else if (itemSlot.Count == 0)
                {
                    ItemOnHand[5].SetActive(false);
                    playerPickUpItem.HealOnhand = false;
                }

                if (drop)
                {
                    DropitemPrefabs(DropPointPos, 6);
                    GetSelectedItem(true);
                    drop = false;
                }
            }
            else
            {
                ItemOnHand[5].SetActive(false);
                playerPickUpItem.HealOnhand = false;
            }

            #endregion

            #region Drop Item
            /*  if (Input.GetKeyDown(KeyCode.G))
              {
                  if (itemSlot != null)
                      drop = true;
              }*/
            if (DollCheckItem != null)
            {
                if (DollCheckItem.Count == 0) ItemOnHand[8].SetActive(false);
            }

        }
        #endregion

        itemDropDatas.RemoveAll(data => data.droppedObject == null);
        ItemDropOBj.RemoveAll(item => item == null);

        #region MakeDollEvent

        if(dollCountFinish == 1)
        {
            if (!finishdoll1)
            {
                Doll1Finish.Invoke();
                finishdoll1 = true;
            }
        }

        if(dollCountFinish == 2)
        {
            if (!MakeDoll2)
            {
                Doll2CutScene.Invoke();
                //*dollCountFinish--;
                MakeDoll2 = true;
            }
        }

        if (DollpushCount == 3)
        {
            if (!take3data)
            {
                TakeDoll3.Invoke();
                DollCountFinish--;
                take3data = true;
            }
        }

        if(DollCountFinish == 6 && !haveDoll && Basket.NeedFinishDoll >= Basket.SlotCount && DelayGameOver == 0)
        {
            if (!Finish6DollCheck)
            {
                finishDoll6Event.Invoke();
                Finish6DollCheck = true;
            }
        }

        if(DollpushCount == 6)
        {
            if (!DelayStart)
            {
                DelayGameOver = 120f;
                DelayStart = true;
            }

            if(DelayGameOver > 0) DelayGameOver -= Time.deltaTime;
            else if(DelayGameOver < 0)
            {
                DelayGameOver = 0;
            }
        }

        if (ChangeCam.GhostDied1data)
        {
            if (MakeDoll2)
            {
                if (!Ghost2spawn)
                {
                    Ghost2Spawn.Invoke();
                    Ghost2spawn = true;
                }
            }

        }

        if (ChangeCam.GhostDied2data)
        {
            if (!Ghost4Spawn)
            {
                Ghost4Evnet.Invoke();
                Ghost4Spawn = true;
            }
        }
        else if(!ChangeCam.GhostDied2data && playerPickUpItem.FinishCloth == 3)
        {
            if (!Ghost4Spawn)
            {
                Ghost4Evnet.Invoke();
                Ghost4Spawn = true;
            }
        }

        #endregion


    }

    public void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventoryslote[selectedSlot].Deselect();
        }

        inventoryslote[newValue].Select();
        selectedSlot = newValue;
        inventoryItem itemSlot = inventoryslote[selectedSlot].GetComponentInChildren<inventoryItem>();
        if (itemSlot != null && itemSlot.gameObject.CompareTag("Cross"))
        {
            triggerCrossAnim = true;
        }
    }

    public bool AddItem(Item item)
    {
          datainventorySlots.Add(new Datainventoryslot(InvDataBase.GetId[item], item));


        for (int i = 0; i < inventoryslote.Length; i++)
        {
            InventorySlote slot = inventoryslote[i];
            inventoryItem itemSlot = slot.GetComponentInChildren<inventoryItem>();
            if (itemSlot != null && itemSlot.item == item && itemSlot.Count < maxstaxkitem &&
                itemSlot.item.stackable )
            {
        
               itemSlot.Count++;
                CountText.text = itemSlot.Count.ToString();
                itemSlot.RefreshCount();

                item.runOut = false;


                return true;
            }

        }

        for (int i = 0; i < inventoryslote.Length; i++)
        {
            InventorySlote slot = inventoryslote[i];
            inventoryItem itemSlot = slot.GetComponentInChildren<inventoryItem>();
            if (itemSlot == null)
            {
               // playerPickUpItem.ItemCount++;
                SpawnnewItem(item, slot);

                return true;
            }

        }

     /*   for (int i = 0;i < ItemDropOBj.Count; i++)
        {

            if (ItemDropOBj[i] == null)
            {
                if (i < itemDropDatas.Count)
                {
                    itemDropDatas.RemoveAt(i);
                    Debug.Log("RemoveDrop");
                }
            }
        }*/


        return false;
    }

    void SpawnnewItem(Item item, InventorySlote slot)
    {     
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        inventoryItem InventoryItem = newItemGo.GetComponent<inventoryItem>();
        InventoryItem.Count = 0;
        InventoryItem.InitialiseItem(item);
        //Debug.Log(InventoryItem.Count);




        newItemGo.tag = item.type.ToString();
    }

    public Item GetSelectedItem(bool use)
    {

        InventorySlote slot = inventoryslote[selectedSlot];
        inventoryItem itemSlot = slot.GetComponentInChildren<inventoryItem>();
        if (itemSlot != null)
        {
            Item item = itemSlot.item;
            if (use )
            {
               
                if (itemSlot.Count > 0)
                {
                    itemSlot.Count--;
                    itemSlot.RefreshCount();
                   // Debug.Log(itemSlot.Count);
                }
                if (itemSlot.Count == 0)
                {
                    item.runOut = true;
                }


                /*if (itemSlot.Count <= 0)
                {

                    foreach (Datainventoryslot dataSlot in datainventorySlots)
                    {
                        if (dataSlot.item == item)
                        {
                            datainventorySlots.Remove(dataSlot);
                            //  Debug.Log("Remove");W
                            break;
                        }
                    }

                    playerPickUpItem.ItemCount--;
                    Destroy(itemSlot.gameObject);


                }*/
            }
            return item;
        }

        return null;
    }


    public void DropitemPrefabs(Vector3 droppoint , int ItemId)
    {
        itemDropDatas.Clear();

        Ray R = Cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(R, out hit)) DesDrop = hit.point;
        else DesDrop = R.GetPoint(1000);




        var DropObj = Instantiate(ItemPrefab[ItemId], droppoint, Quaternion.identity) as GameObject;
        DropObj.GetComponent<Rigidbody>().velocity = (DesDrop - droppoint).normalized * DropSpeed;

        if (itemdropCollect.itemPositions.ContainsKey(ItemId))
        {

            itemdropCollect.itemPositions[ItemId] = droppoint;
        }
        else
        {

            itemdropCollect.itemPositions.Add(ItemId, droppoint);
        }


        itemDropDatas.Add(new ItemDropData(itemdropCollect.GetId[ItemPrefab[ItemId]], DropObj, droppoint));
        ItemDropOBj.Add(DropObj);


    }


    public void LoadData(GameData data)
    {
        data.inventoryData = InvDataBase;

        datainventorySlots.Clear();
        itemDropDatas.Clear();
        ItemDropOBj.Clear();

        int slotIndex = 0;

        foreach (var savedItem in data.InventorySaveData)
        {
            Item item = savedItem.item;
            int id = savedItem.ID;
            datainventorySlots.Add(new Datainventoryslot(id, item));

            if (slotIndex < inventoryslote.Length)
            {
                InventorySlote slot = inventoryslote[slotIndex];
                //SpawnnewItem(item, slot);
                AddItem(item);
                slotIndex++;
            }
            else break;


            //   AddItem(item);
        }

        foreach(var savedItem in data.itemDropDatas)
        {
            GameObject item = savedItem.droppedObject;
            int id = savedItem.itemID;
            Vector3 pos = savedItem.posirion;
            itemDropDatas.Add(new ItemDropData(id, item, pos));


            DropitemPrefabs(pos, id);

            break;
        }

        foreach (var savedItem in data.ItemDropObj)
        {
            ItemDropOBj.Add(savedItem);

            break;
        }

        DollCountFinish = data.DollCountData;
        take3data = data.GhostTakeDoll3;
        Ghost2spawn = data.Ghost2Spawn;
        MakeDoll2 = data.MakeDoll2;
        Ghost4Spawn = data.Ghost4DiedEvent;
        Finish6DollCheck = data.FinishDoll6;
        finishdoll1 = data.Doll1finish;

    }

    public void SaveData(GameData data)
    {
        data.inventoryData = InvDataBase;

        data.InventorySaveData.Clear();
        data.itemDropDatas.Clear();
        data.ItemDropObj.Clear();

        for (int i = 0; i < datainventorySlots.Count; i++)
        {
            Datainventoryslot slot = datainventorySlots[i];

            slot.item = InvDataBase.GetItem[slot.ID];


            data.InventorySaveData.Add(slot);
        }



        for (int i = 0;i < itemDropDatas.Count; i++)
        {
            ItemDropData newdrop = itemDropDatas[i];

            newdrop.droppedObject = itemdropCollect.GetItem[newdrop.itemID];

            data.itemDropDatas.Add(newdrop);
         
        }



        for (int i = 0; i < ItemDropOBj.Count; i++)
        {

            GameObject item = ItemDropOBj[i];


            data.ItemDropObj.Add(item);
        }

        data.DollCountData = DollCountFinish;
        data.GhostTakeDoll3 = take3data;
        data.Ghost2Spawn = Ghost2spawn;
        data.MakeDoll2 = MakeDoll2;
        data.Ghost4DiedEvent = Ghost4Spawn;
        data.FinishDoll6 = Finish6DollCheck;
        data.Doll1finish = finishdoll1;

    }

    public void deleteData(GameData data)
    {
       
    }


    [System.Serializable]
    public class Datainventoryslot
    {
        public int ID;
        public Item item;
        

        public Datainventoryslot(int iD, Item item)
        {
            ID = iD;
            this.item = item;
        }
    }


    [System.Serializable]

    public class ItemDropData 
    {
        public int itemID;
        public GameObject droppedObject;
        public Vector3 posirion;

        public ItemDropData(int id, GameObject obj, Vector3 pos)
        {
            itemID = id;
            droppedObject = obj;
            posirion = pos;
        }
    }

    public void LostCross()
    {
        for (int i = 0; i < inventoryslote.Length; i++)
        {
            inventoryItem itemSlot = inventoryslote[i].GetComponentInChildren<inventoryItem>();
            if (itemSlot != null && itemSlot.gameObject.CompareTag("Cross"))
            {
                selectedSlot = i;
                GetSelectedItem(true);
            }

        }
    }

}
