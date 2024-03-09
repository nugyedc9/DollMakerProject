using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineShow : MonoBehaviour
{
    public GameObject[] LineAfterCut;
    public GameObject[] LineBeforeCut;
    public BoxCollider2D[] boxCol;
    int CutNum;

    private void Start()
    {
        for (int i = 0; i < boxCol.Length; i++)
        {
                boxCol[i].enabled = false;      
        }
        boxCol[0].enabled = true;
    }


    public void LineCutShow()
    {
        if (CutNum < LineBeforeCut.Length)
        {
            LineAfterCut[CutNum].SetActive(true);
            boxCol[CutNum].enabled = false;
            LineBeforeCut[CutNum].SetActive(false);
            CutNum++;
            if(CutNum < LineAfterCut.Length )
            boxCol[CutNum].enabled = true;
        }
    }

    public void CloseAllLine()
    {
        for (int i = 0; i < LineAfterCut.Length; i++)
        {
            LineAfterCut[i].SetActive(false);
        }

        for (int i = 0;i < LineBeforeCut.Length; i++)
        {
            LineBeforeCut[i].SetActive(true);
        }

        for(int i = 0; i < boxCol.Length; i++)
        {
            boxCol[i].enabled = false;
        }
        CutNum = 0;
        boxCol[0].enabled = true;
    }
}
