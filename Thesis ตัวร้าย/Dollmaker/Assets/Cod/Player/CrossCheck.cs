using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossCheck : MonoBehaviour
{

    [Header("Hp Thing")]

    public float MaxHp;
    public float curHp;


    // Start is called before the first frame update
    void Start()
    {
        curHp = MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (curHp < 0)
            curHp = 0;
           
    }

    public void TakeDamage()
    {
       curHp -= Time.deltaTime;
        
    }

    public void ReCharge()
    {
        curHp += Time.deltaTime;
    }
}
