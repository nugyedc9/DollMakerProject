using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System.Peristence
{
    public class JsonSerializer : ISerializer
    {
        public T Deserilaizer<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public string Serialize<T>(T obj)
        {
            return JsonUtility.ToJson(obj, true);
        }

    }
}
