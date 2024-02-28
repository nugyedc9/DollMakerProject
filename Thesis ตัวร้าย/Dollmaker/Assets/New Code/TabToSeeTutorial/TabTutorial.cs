using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabTutorial : MonoBehaviour
{
    public PlayerAttack PAttack;
    public PlayerChangeCam Cam;
    public InputManager inputManager;
    public Animator InvShow;
    public GameObject[] Tutorial;
    public GameObject GuideBook ,FullInventory, PrevButt, NextButt;

    [Header("Story boxCol")]
    public GameObject _2StoryCanWalk;
    public GameObject _5StoryCanWalk;


    [SerializeField] bool openTutor;
     public bool OpenTutor { get { return openTutor; } set { openTutor = value; } }
    public int PageNum;

    [SerializeField] int pageCount;
    public int PageCount { get { return pageCount; } set { pageCount = value; } }

    bool PlayAnimInvTab, Hit_2Story, Hit_5Story = true;


    private void Awake()
    {
        PageCount = 8;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cam.camOnPerSon == true)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!OpenTutor)
                {
                    OpenTutor = true;
                    InvShow.enabled = true;
                    PlayAnimInvTab = true;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;                    
                    inputManager.OnTab = true;
                    InvShow.Play("OpenTabInv", 0, 0);
                    
                    if(!Hit_2Story)
                        _2StoryCanWalk.SetActive(true);
                    if (!Hit_5Story)
                        _5StoryCanWalk.SetActive(true);
                }
                else
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
            }
        }

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

        if (PageNum == 0) PrevButt.SetActive(false);
        else if (PageNum != 0) PrevButt.SetActive(true);
        if (PageNum == pageCount - 1) NextButt.SetActive(false);
        if (PageNum < pageCount - 1) NextButt.SetActive(true);

    }

    public void PageSelect(int index)
    {
        Tutorial[PageNum].SetActive(false);
        PageNum = index;
    }

    public void NextPage()
    {
        if (PageNum < pageCount - 1)
        {
            PageNum++;
            Tutorial[PageNum-1].SetActive(false);
            Tutorial[PageNum].SetActive(false);
        }
        if(PageNum >= pageCount - 1) PageNum = pageCount - 1;
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

    public void Story5()
    {
        Hit_5Story = false;
    }

}
