using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GhostStateManager state;
    public int DMGBullet;



    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ghost")
        {
            state = other.GetComponent<GhostStateManager>();
            state.BulletHit(DMGBullet);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
