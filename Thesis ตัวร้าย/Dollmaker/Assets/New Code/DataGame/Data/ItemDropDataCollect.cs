using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(menuName = "Scriptable object/ItemDropDatas")]
public class ItemDropDataCollect : ScriptableObject, ISerializationCallbackReceiver
{
    public GameObject[] Items;
    public Dictionary<GameObject, int> GetId = new Dictionary<GameObject, int>();
    public Dictionary<int, GameObject> GetItem = new Dictionary<int, GameObject>();
    public Dictionary<int, Vector3> itemPositions = new Dictionary<int, Vector3>();

    public void OnAfterDeserialize()
    {
        GetId = new Dictionary<GameObject, int>();
        GetItem = new Dictionary<int, GameObject>();
        for (int i = 0; i < Items.Length; i++)
        {
            GetId.Add(Items[i], i);
            GetItem.Add(i, Items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
       
    }


}
