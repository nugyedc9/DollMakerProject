using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItemCanva : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 lastMousePosition;
    private float x, y, z, Timer;

    public MiniGameAuidition minigame;
    public Animator ScissorAnim;

    public bool blockX, blockY, blockZ;

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
            minigame.ButtonCutLine();
            gameObject.GetComponent<Animator>().enabled = true;
            ScissorAnim.Play("CutLine");           
            Timer = 2;
            Draging = false;
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
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Draging) { 
        Vector2 curremtMousePosition = eventData.position;
            Vector2 diff = curremtMousePosition - lastMousePosition;

            transform.position = Input.mousePosition;

            //   RectTransform rectTransform = GetComponent<RectTransform>();

            x = y = z = 0;

            if (!blockX)
                x = diff.x;

            if (!blockY)
                y = diff.y;

            if (!blockZ)
                z = transform.localPosition.z;

            //   rectTransform.position = rectTransform.position + new Vector3(x, y, z);

            lastMousePosition = curremtMousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (toOrginal)
            transform.localPosition = orginalPosition;
    }
}
