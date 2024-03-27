using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


    public class FileDataHanderler 
    {
    #region OldSaveFile
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileDataHanderler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadeddata = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadeddata = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error to load : " + fullPath + "\n" + e);
            }
        }
        return loadeddata;
    }


    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {

            Debug.LogError("Error to save : " + fullPath + "\n" + e);
        }
    }


    public void _Delete(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        if(File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

    }

    #endregion

    #region not work now
    /*   ISerializer serializer;
       string datapath, fileExtension;

       public FileDataHanderler(ISerializer serializer)
       {
           this.datapath = Application.persistentDataPath;
           this.fileExtension = "json";
           this.serializer = serializer;
       }


       string GetPathToFile(string filename)
       {
           return Path.Combine(datapath, string.Concat(filename, ".", fileExtension));
       }


       public void Delete(string name)
       {
           string fileLocation = GetPathToFile(name);

           if(File.Exists(fileLocation))
           {
               File.Delete(fileLocation);
           }
       }

       public void DeleteAll()
       {
         *//*foreach (string filepath in Directory.GetFiles(datapath))
           {
               File.Delete(filepath);
           }*//*
       }

       public GameData Load(string name)
       {
           string fileLocation = GetPathToFile(name);

           if (!File.Exists(fileLocation))
           {
               throw new ArgumentException($"No presisted GameData with name '{name}'");
           }

           return serializer.Deserilaizer<GameData>(File.ReadAllText(fileLocation));
       }

       public void Save(GameData data, bool overwrite = true)
       {
           string filelocation = GetPathToFile(data.Name);

           if(!overwrite && File.Exists(filelocation))
           {
               throw new IOException($"The file '{data.Name}.{fileExtension}' alrealy exists and cannot be overwritten.");
           }

           File.WriteAllText(filelocation, serializer.Serialize(data));

       }
   }*/

    #endregion
}
