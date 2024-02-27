using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabTutorial : MonoBehaviour
{
    public PlayerAttack PAttack;
    public PlayerChangeCam Cam;
    public GameObject[] Tutorial;
    public GameObject GuideBook ,KeyItem, PrevButt, NextButt;
    [SerializeField] bool openTutor;
     public bool OpenTutor { get { return openTutor; } set { openTutor = value; } }
    public int PageNum;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!OpenTutor)
            {
                OpenTutor = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                OpenTutor = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }


        if(OpenTutor)
        {
            Time.timeScale = 0;
            GuideBook.SetActive(true);
            Tutorial[PageNum].SetActive(true);
            KeyItem.SetActive(true);
            PAttack.Attack = false;
            
        }
        else
        {
            Time.timeScale = 1;
            KeyItem.SetActive(false);
            if(Cam.OpenKeyItemInv == false )
            GuideBook.SetActive(false);
            Tutorial[PageNum].SetActive(false);
            
        }

        if (PageNum == 0) PrevButt.SetActive(false);
        else if (PageNum != 0) PrevButt.SetActive(true);
        if (PageNum == Tutorial.Length - 1) NextButt.SetActive(false);
        if (PageNum < Tutorial.Length - 1) NextButt.SetActive(true);

    }

    public void PageSelect(int index)
    {
        Tutorial[PageNum].SetActive(false);
        PageNum = index;
    }

    public void NextPage()
    {
        if (PageNum < Tutorial.Length - 1)
        {
            PageNum++;
            Tutorial[PageNum-1].SetActive(false);
            Tutorial[PageNum].SetActive(false);
        }
        if(PageNum >= Tutorial.Length - 1) PageNum = Tutorial.Length - 1;
    }

    public void PrevPage()
    {
        if (PageNum >= 0)
        {
            PageNum--;
            Tutorial[PageNum + 1].SetActive(false);
            Tutorial[PageNum].SetActive(false);
        }
        if(PageNum < 0) PageNum =0;

    }

}
