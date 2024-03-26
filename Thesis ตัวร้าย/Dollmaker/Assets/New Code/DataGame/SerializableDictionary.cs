using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{

    [SerializeField] private List<TKey> GetID = new List<TKey>();

    [SerializeField] private List<TValue> GetItem = new List<TValue>();

    public void OnAfterDeserialize()
    {
       GetID.Clear();
        GetItem.Clear();
        foreach(KeyValuePair<TKey, TValue> pair in this)
        {
            GetID.Add(pair.Key);
            GetItem.Add(pair.Value);
        }
    }

    public void OnBeforeSerialize()
    {
        this.Clear();

        for(int i = 0; i < GetID.Count; i++)
        {
            this.Add(GetID[i], GetItem[i]);
        }
    }
}
