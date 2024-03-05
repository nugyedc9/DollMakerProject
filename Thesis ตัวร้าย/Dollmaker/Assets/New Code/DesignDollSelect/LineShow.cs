using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineShow : MonoBehaviour
{
    public GameObject[] LineAfterCut;
    int CutNum;

    public void LineCutShow()
    {
        LineAfterCut[CutNum].SetActive(true);
        CutNum++;
    }

    public void CloseAllLine()
    {
        for (int i = 0; i < LineAfterCut.Length; i++)
        {
            LineAfterCut[i].SetActive(false);
        }
        CutNum = 0;
    }
}
