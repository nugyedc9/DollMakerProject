using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItemCanva : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 lastMousePosition;
    private float Timer;

    [Header("Cloth Design")]
    public GameObject Cloth;
    public DesignSelect designSelect;

    [Header("ItemType")]
    public bool cloth;

    public MiniGameAuidition minigame;
    public CanPlayMini1 canplayMinIgame;
    public PlayerAttack PushdollCloth;
    public PlayerPickUpItem playerPickUpItem;
    public Animator ScissorAnim;  
    public bool toOrginal;
    private bool Draging;

    private Vector3 orginalPosition;

    private void Start()
    {
        orginalPosition = transform.localPosition;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "MiniGCutCanva")
        {
            minigame.cutLine = true;
            gameObject.GetComponent<Animator>().enabled = true;
            ScissorAnim.Play("CutLine");           
            Timer = 2;
            Draging = false;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (cloth)
        {
            if (collision.gameObject.tag == "ClothDrop")
            {
                transform.localPosition = orginalPosition;
                Cloth.SetActive(true);
                designSelect.HaveCloth = true;
                PushdollCloth.pushItemInbasket();
            }
        }
    }


    private void Update()
    {
        if(Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        if (!Draging)
        {
            if (Timer < 0)
            {
                transform.localPosition = orginalPosition;
                gameObject.GetComponent<Animator>().enabled = false;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastMousePosition = eventData.position;
        Draging = true;
        toOrginal = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Draging) { 
        Vector2 curremtMousePosition = eventData.position;
            Vector2 diff = curremtMousePosition - lastMousePosition;

            transform.position = Input.mousePosition;

            lastMousePosition = curremtMousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (toOrginal)
            transform.localPosition = orginalPosition;
    }
}
