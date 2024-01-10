using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossCheck : MonoBehaviour
{

    [Header("Hp Thing")]

    public float MaxHp;
    public float curHp;
    public delegate void OnItemDamaged(int damage);
    public static event OnItemDamaged ItemDamagedEvent;

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

    public void TakeDamage(int damage)
    {
        curHp -= damage;
        if (curHp <= 0)
        {
            
        }

        if (ItemDamagedEvent != null)
        {
            ItemDamagedEvent.Invoke(damage);
        }
    }
}
