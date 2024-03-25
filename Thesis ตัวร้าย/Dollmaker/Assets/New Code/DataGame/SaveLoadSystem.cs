using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace System.Peristence
{
    [Serializable] public class GameData
    {
        public string Name;
        public string CurrentLevelName;
    }

    public interface ISaveable 
    {
       // SerializableGuid Id { get; set; }
    }

    public interface IBilnd<TData> where    TData : ISaveable
    {
        void bilnd(TData data);
    }

    public class SaveLoadSystem : PersistentSingleton<SaveLoadSystem>
    {
        [SerializeField] public GameData gameData;

        IDataGame dataGame;

        protected override void Awake()
        {
            base.Awake();
            dataGame = new FileDataHanderler(new JsonSerializer());
        }

        public void NewGame()
        {
            gameData = new GameData { Name = "New Game" , CurrentLevelName = "In House Scene" };
            SceneManager.LoadScene(gameData.CurrentLevelName);
        }

        public void SaveGame() =>    dataGame.Save(gameData);
        

        public void LoadGame(string gameName)
        {
            gameData = dataGame.Load(gameName);

            if(String.IsNullOrWhiteSpace(gameData.CurrentLevelName))
            {
                gameData.CurrentLevelName = "In House Scene";
            }

            SceneManager.LoadScene(gameData.CurrentLevelName);
        }

        public void ReloadGame() => LoadGame(gameData.Name);

        public void DeleteGame(string gameName) =>    dataGame.Delete(gameName);
        

    }


}
