using System.Collections;


    public interface IDataGame
    {
        void LoadData(GameData data);

        void SaveData(GameData data);

        void deleteData(GameData data);

      
    }



/*
        void Save(GameData data, bool overwrite = true);

        GameData Load(string name);

        void Delete(string name);
        void DeleteAll();
*/  // IEnumerable<string> ListSave();