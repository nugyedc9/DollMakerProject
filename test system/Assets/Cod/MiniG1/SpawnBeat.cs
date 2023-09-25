using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBeat : MonoBehaviour
{
    [SerializeField] GameObject[] BeatPref;
    [SerializeField] float secondSpawn = 0.5f;
    [SerializeField] float Posx;
    [SerializeField] float Posy;
    public bool SpawnIt = false;
    Coroutine BeatSpawnCoroutine;
    [Header ("Score Thing")]
    public float Score;
    public float MaxScore;
    public float Miss;
    [Header ("GameObj")]
    public GameObject miniG1Canva;
    public GameObject Head;
    public GameObject PressButtom;

    public static SpawnBeat Instance;

    

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
       // StartCoroutine(BeatSpawn());
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyUp(KeyCode.Space))
        {
            Head.SetActive(true);
            PressButtom.SetActive(false);
            SpawnIt = true;
            BeatSpawnCoroutine = StartCoroutine(BeatSpawn());
        }
        if(Score == MaxScore)
        {
            miniG1Canva.SetActive(false);
            Head.SetActive(false);
            PressButtom.SetActive(true) ;
            Score = 0;
        }
        if(Miss >= 1 && SpawnIt)
        {
            StopCoroutine(BeatSpawnCoroutine);
            SpawnIt = false;
            PressButtom.SetActive(true );
            Miss = 0;
            Score = 0;
        }

    }

    IEnumerator BeatSpawn()
    {
        while (true)
        {
            var position = new Vector3(Posx , Posy);
            GameObject spawnedObject = Instantiate(BeatPref[Random.Range(0, BeatPref.Length)], position, Quaternion.identity);
            GameObject beatSpawn = GameObject.FindGameObjectWithTag("BeatSpawn");
            if (beatSpawn != null)           
            spawnedObject.transform.SetParent(beatSpawn.transform, false);            
            yield return new WaitForSeconds(secondSpawn);
        }
    }

    public void MakeScore()
    {
        Score++;
    }

    public void missTake()
    {
        Miss++;
    }

    IEnumerable WaitMiss()
    {
        yield return new WaitForSeconds(3);
        Miss = 0;
    }


}
