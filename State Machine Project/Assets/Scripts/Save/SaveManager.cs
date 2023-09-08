using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Text;
using System.IO;

public class SaveManager
{

    public event EventHandler<SaveData> OnLoadGame;

    private SaveData saveData;

    private string directoryPath = Path.Combine(Application.persistentDataPath, "/GameData");
    private string filePath = Path.Combine(Application.persistentDataPath, "/GameData/SaveData");

    public void Init()
    {

        //Check if has directory
        if(Directory.Exists(directoryPath))
        {
            //Check if save file exist
            if(!File.Exists(filePath))
            {
                saveData = new SaveData();

                return;
            }

            Load();

            return;
        }

        Directory.CreateDirectory(directoryPath);

        saveData = new SaveData();
    }

    private void Load()
    {
        string fileString = File.ReadAllText(filePath);

        if(fileString != "")
        {
            try
            {
                saveData = JsonUtility.FromJson<SaveData>(fileString);
            }
            catch(Exception e)
            {
                Debug.Log("Exception" + e.Message);
            }

            if (saveData != null)
                OnLoadGame?.Invoke(this, saveData);
        }
    }

    private void WriteFile()
    {

    }

    private void Save()
    {
        string fileText = "";

        try
        {
            //Serialize: convert save data into json
            fileText = JsonUtility.ToJson(saveData);
        }
        catch (Exception e)
        {
            Debug.Log("Exception" + e.Message);
        }

        if(fileText != "")
        {
            //write save data into file path
            File.WriteAllText(filePath, fileText);
        }           
    }

    public void SaveRankData(RankSetup [] rank)
    {
        saveData.rank = rank;
    }

    public void SavePlayerData(int id, PlayerType type)
    {
        saveData.playerId = id;
        saveData.playerType = type;
    }
}
