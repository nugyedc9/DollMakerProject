using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataGameManager : MonoBehaviour
{

    [Header("File Strage Config")]
    [SerializeField] private string fileName;

    public GameObject PlayerPosition;

    private GameData gameData;
    private List<IDataGame> DataGameObjects;
    private FileDataHanderler dataHandler;

    public static DataGameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {

        }
        Instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHanderler(Application.dataPath, fileName);
        this.DataGameObjects = FindAllDataGameObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData.PlayerPoS(PlayerPosition.transform.position);
        this.gameData = new GameData();
    }

    public void LoadGame()
    {

        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            NewGame();
        }

        foreach (IDataGame dataGameObj in DataGameObjects)
        {
            dataGameObj.LoadData(gameData);
        }

    }

    public void SaveGame()
    {
        foreach (IDataGame dataGameObj in DataGameObjects)
        {
            dataGameObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    /*    private void OnApplicationQuit()
        {
            SaveGame();
        }
    */

    private List<IDataGame> FindAllDataGameObjects()
    {
        IEnumerable<IDataGame> dataGameObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataGame>();

        return new List<IDataGame>(dataGameObjects);
    }

}
