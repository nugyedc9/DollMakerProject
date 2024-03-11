using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrossState
{
    Idle,
    HoldUp,
    HitGhost
}


public class CrossAnim : MonoBehaviour
{
    public Animator anim;
    [SerializeField] CrossState stateCross;
    public CrossState StateCross { get { return stateCross; } set { stateCross = value; } }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("HoldUp", false);
        anim.SetBool("_HitGhost", false);

        switch (StateCross)
        {
            case CrossState.Idle:
                anim.SetBool("Idle", true);
                break;
            case CrossState.HoldUp:
                anim.SetBool("HoldUp", true);
                break;
            case CrossState.HitGhost:
                anim.SetBool("_HitGhost", true);
                break;
        }

    }


    public void SetState(CrossState state) 
    {
       StateCross = state;
    }

}
