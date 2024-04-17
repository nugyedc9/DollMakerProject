using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
public class Event : MonoBehaviour, IDataGame
{


    [Header("Light on / off")]
    public GameObject[] LightSwitchOn;
    public GameObject[] LightSwitchOff;
    public GameObject LightOn;
    public GameObject LightOff;
    public AudioSource LightSound;
    public bool TurnLight;

    private BoxCollider collider;

    bool _1Game;
    [SerializeField] public string id;


    [ContextMenu("Generate grid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    public void TurnOnLight()
    {
        LightSound.Play();

        if (!TurnLight)
        {
            for (int i = 0; i < LightSwitchOn.Length; i++)
            {
                LightSwitchOn[i].SetActive(true);
                LightSwitchOff[i].SetActive(false);
            }

            LightOn.SetActive(true);
            LightOff.SetActive(false);

            TurnLight = true;
        }
        else
        {

            for (int i = 0; i < LightSwitchOn.Length; i++)
            {
                LightSwitchOn[i].SetActive(false);
                LightSwitchOff[i].SetActive(true);
            }
            LightOn.SetActive(false);
            LightOff.SetActive(true);

            TurnLight = false;
        }
    }

    public void LoadData(GameData data)
    {
        _1Game = data.Savelight1;

        if (_1Game)
        {
            data.LightOn.TryGetValue(id, out TurnLight);
        }


        if(TurnLight)
        {
            LightOn.SetActive(true);
            LightOff.SetActive(false);

        }
    }

    public void SaveData(GameData data)
    {
        if (data.LightOn.ContainsKey(id))
        {
            data.LightOn.Remove(id);
        }
        data.LightOn.Add(id, TurnLight);
        if (!_1Game)
        {
            _1Game = true;
        }
        data.Savelight1 = _1Game;
    }

    public void deleteData(GameData data)
    {

    }

    public void ToggleCollider(bool state)
    {
        if (collider != null)
        {
            collider.enabled = state;
        }

    }



}



