using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public SpawnBeat spawnBeat;

    public int Score;
    public int MaxScore;

    // Start is called before the first frame update
    void Start()
    {
        spawnBeat = GetComponent<SpawnBeat>();
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeScore()
    {
        Score++;
    }
}
