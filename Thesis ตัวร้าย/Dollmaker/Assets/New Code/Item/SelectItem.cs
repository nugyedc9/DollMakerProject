using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectItem : MonoBehaviour
{
    public int SelectedItem = 0;


    public void Start()
    {
        
    }
    public void Update()
    {
        selectItem();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectedItem = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectedItem = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectedItem = 2;
        }

    }

    void selectItem()
    {
        int i = 0;
        foreach (Transform item in transform)
        {
            if(i == SelectedItem) item.gameObject.SetActive(true);
            else item.gameObject.SetActive(false);
            i++;
        }
    }

    public void Slot1()
    {
        SelectedItem = 0;
    }
    public void Slot2()
    {
        SelectedItem = 1;
    }
    public void Slot3()
    {
        SelectedItem = 2;
    }

}
