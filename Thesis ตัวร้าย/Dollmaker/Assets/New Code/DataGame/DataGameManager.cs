using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace System.Peristence.Editor
{
    [CustomEditor(typeof(SaveLoadSystem))]

    public class DataGameManager : UnityEditor.Editor 
    {

        public override void OnInspectorGUI()
        {
           SaveLoadSystem saveLoadSystem = (SaveLoadSystem)target;
            string gameName = saveLoadSystem.gameData.Name;

            DrawDefaultInspector();

            if(GUILayout.Button("Save Game"))
            {
                saveLoadSystem.SaveGame();
            }
            if (GUILayout.Button("Load Game"))
            {
                saveLoadSystem.LoadGame(gameName);
            }
            if (GUILayout.Button("Delete Game"))
            {
                saveLoadSystem.DeleteGame(gameName);
            }
        }

        #region Old SaveGAem
        /*[Header("File Strage Config")]
        [SerializeField] private string fileName;


        private GameData gameData;
        private List<IDataGame> DataGameObjects;
        private FileDataHanderler dataHandler;

        bool StartNewGame;

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
            if (!StartNewGame) 
            LoadGame();


        }

        public void NewGame()
        {
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

        *//*    private void OnApplicationQuit()
            {
                SaveGame();
            }
        *//*

        private List<IDataGame> FindAllDataGameObjects()
        {
            IEnumerable<IDataGame> dataGameObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataGame>();

            return new List<IDataGame>(dataGameObjects);
        }


        public void StartNewGameButton()
        {
            StartNewGame = true;
        }*/
        #endregion


    }
}
