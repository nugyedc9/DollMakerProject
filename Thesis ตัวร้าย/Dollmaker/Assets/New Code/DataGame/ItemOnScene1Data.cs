using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/ItemOnScene1")]
public class ItemOnScene1Data : ScriptableObject, ISerializationCallbackReceiver
{

    public GameObject[] itemInGame;
    public Dictionary<string, GameObject> itemIDMap = new Dictionary<string, GameObject>();
    public void OnAfterDeserialize()
    {
        itemIDMap.Clear();

      
    }

    public void OnBeforeSerialize()
    {
       
    }
}
