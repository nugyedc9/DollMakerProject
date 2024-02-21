using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _2DFacing3D : MonoBehaviour
{

    public Transform Cam;
    public GameObject ObjLookAtCam;

    public List<Sprite> sprite = new List<Sprite>(4);

    enum Facing { Up,  Down, Left, Rigth };
    private Facing facing;

    private float angle, sign = 1, signAngle;

    private Animator animator;
    private SpriteRenderer _sprite;
    private Transform _ObjLookAtCam;
    private Transform _T;
    private Vector3 direction;
    private bool ToutCome;


    private void OnValidate()
    {
        if(ObjLookAtCam == null ) { Debug.Log("Push Obj"); }
        else animator = ObjLookAtCam.GetComponent<Animator>();

        if( animator != null )
        {
            animator.enabled = false;
            animator.enabled = true;
        }

        if(animator != null && animator.isActiveAndEnabled )
        {
            ToutCome = conTainsParam("direction");
        }

    }

    public bool conTainsParam( string _ParaName)
    {
        foreach (AnimatorControllerParameter param in animator.parameters )
        {
            if (param.name == _ParaName)
                return true;
        }
        return false;
    }
    private void Awake()
    {
        _T = transform;

        if( ObjLookAtCam != null )
        {
            animator = ObjLookAtCam.GetComponent<Animator>();
            _sprite = GetComponent<SpriteRenderer>();
            _ObjLookAtCam = ObjLookAtCam.transform;
        }
    }

    // Update is called once per frame
    void Update()   
    {
        Vector3 forward = _T.forward;
        forward.y = 0;
        Vector3 direction2 = _T.InverseTransformPoint( Cam.position );
        sign = (direction2.x >= 0) ? -1 : 1;
        angle = Vector3.Angle(direction, forward);
        signAngle = angle * sign;

        direction = Cam.position - _T.position;
        direction.y = 0;
        _ObjLookAtCam.rotation = Quaternion.LookRotation(-direction, _T.up);

       if(animator.isActiveAndEnabled == true)
        {
            animator.SetInteger("direction", (int)facing);
        }
        else
        {
            _sprite.sprite = sprite[(int)facing];
        }
    }

    public virtual void LateUpdate()
    {
        if(angle < 45.0f)
        {
            facing = Facing.Up;
        }
        else if (angle < 135.0f)
        {
            facing = sign < 0 ? Facing.Rigth : Facing.Left;
        }
        else
        {
            facing = Facing.Down;
        }
    }
}
