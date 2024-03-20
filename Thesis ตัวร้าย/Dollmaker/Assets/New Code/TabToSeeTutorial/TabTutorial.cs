using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TabTutorial : MonoBehaviour
{
    public PlayerAttack PAttack;
    public PlayerPickUpItem playerpickup;
    public PlayerChangeCam Cam;
    public InputManager inputManager;
    public Animator InvShow;
    public GameObject[] Tutorial;
    public GameObject GuideBook ,FullInventory, PrevButt, NextButt;

    [Header("Text Objective")]
    public GameObject Sub;
    public Animator Animtext;

    [Header("Story boxCol")]
    public GameObject _2StoryCanWalk;
    public GameObject _6Story, Tutorialnote;

    [Header("---- Audio ----")]
    public AudioSource AudioSound;
    public AudioClip  NextS, PrevS;

    [SerializeField] bool openTutor;
     public bool OpenTutor { get { return openTutor; } set { openTutor = value; } }
    public int PageNum;

    [SerializeField] int pageCount;
    public int PageCount { get { return pageCount; } set { pageCount = value; } }

    bool PlayAnimInvTab, Hit_2Story, Hit_5Story = true, OpenObjective;


    private void Awake()
    {
        PageCount = Tutorial.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cam.camOnPerSon == true)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!OpenTutor && !PAttack.isPause && !playerpickup.OnNote)
                {
                    PageSelect(0);
                    OpenTutor = true;
                    InvShow.enabled = true;
                    PlayAnimInvTab = true;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;                    
                    inputManager.OnTab = true;
                   // InvShow.Play("OpenTabInv", 0, 0);

                    
                    if(!Hit_2Story)
                        _2StoryCanWalk.SetActive(true);
                    if (!Hit_5Story)
                    {
                        _6Story.SetActive(true);
                        OpenObjective = true;
                    }
                }
                else if(OpenTutor)
                {
                  
                    PlayAnimInvTab = true;
                    OpenTutor = false;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    inputManager.OnTab = false;
                    InvShow.Play("CloseTableInv", 0, 0);

                    Animtext.Play("MainToDo", 0, 0);
                    Sub.SetActive(false);

                    Hit_2Story = true;
                    Hit_5Story = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!OpenTutor && !PAttack.isPause && !playerpickup.OnNote)
                {
                    PageSelect(9);
                    OpenTutor = true;
                    InvShow.enabled = true;
                    PlayAnimInvTab = true;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    inputManager.OnTab = true;
                    // InvShow.Play("OpenTabInv", 0, 0);


                    if (!Hit_2Story)
                        _2StoryCanWalk.SetActive(true);
                    if (!Hit_5Story)
                    {
                        _6Story.SetActive(true);
                        OpenObjective = true;
                    }
                }
                else if (OpenTutor)
                {
                    PlayAnimInvTab = true;
                    OpenTutor = false;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    inputManager.OnTab = false;
                    InvShow.Play("CloseTableInv", 0, 0);

                    Animtext.Play("MainToDo", 0, 0);
                    Sub.SetActive(false);

                    Hit_2Story = true;
                    Hit_5Story = true;
                }
            }
        }

       /* if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(OpenTutor)
            {
                PlayAnimInvTab = true;
                OpenTutor = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                inputManager.OnTab = false;
                InvShow.Play("CloseTableInv", 0, 0);


                Hit_2Story = true;
                Hit_5Story = true;
            }
        }*/

        if(OpenTutor)
        {
            GuideBook.SetActive(true);
            Tutorial[PageNum].SetActive(true);
            FullInventory.SetActive(true);
            PAttack.Attack = false;
        }
        else
        {
            FullInventory.SetActive(false);
            if(Cam.OpenKeyItemInv == false )
            GuideBook.SetActive(false);
            Tutorial[PageNum].SetActive(false);
        }

        if (PageNum == 0 && OpenTutor)
        {
            InvShow.Play("OpenTabInv", 0, 0);
            Sub.SetActive(true);
            Animtext.Play("Opentab", 0, 0);
            PrevButt.SetActive(false);
        }

        else if (PageNum != 0 && OpenTutor)
        {
            InvShow.Play("NotInvPage", 0, 0);
            Sub.SetActive(false);
            Animtext.Play("Idle", 0, 0);
            PrevButt.SetActive(true);
        }
        if (PageNum == PageCount - 1) NextButt.SetActive(false);
        if (PageNum < PageCount - 1) NextButt.SetActive(true);

       if(OpenObjective && PageNum != 8 && openTutor)
        {
            Tutorialnote.SetActive(true);
        }
       else if(PageNum == 8 && OpenTutor)
        {
            OpenObjective = false;
            Tutorialnote.SetActive(false);
        }

    }

    public void PageSelect(int index)
    {
        Tutorial[PageNum].SetActive(false);
        PageNum = index;
    }

    public void NextPage()
    {
        if (PageNum < PageCount - 1)
        {
            AudioSound.clip = NextS;
            AudioSound.Play();
            PageNum++;
            Tutorial[PageNum-1].SetActive(false);
            Tutorial[PageNum].SetActive(false);
        }
        if(PageNum >= PageCount - 1) PageNum = PageCount - 1;
    }

    public void PrevPage()
    {
        if (PageNum >= 0)
        {
            AudioSound.clip = PrevS;
            AudioSound.Play();
            PageNum--;
            Tutorial[PageNum + 1].SetActive(false);
            Tutorial[PageNum].SetActive(false);
        }
        if(PageNum < 0) PageNum =0;

    }

    public void Story5()
    {
        Hit_5Story = false;
    }

}
