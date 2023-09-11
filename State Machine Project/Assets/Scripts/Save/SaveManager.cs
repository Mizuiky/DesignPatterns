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

    private SaveData _saveData;

    private string _directoryPath;
    private string _persistentData;

    private string _filePath;

    public void Init()
    {
        _persistentData = Application.persistentDataPath;

        _directoryPath = Path.Combine(_persistentData , "GameData");

        _filePath = Path.Combine(Application.persistentDataPath, "GameData/SaveData.txt");

        //Check if has directory
        if (Directory.Exists(_directoryPath))
        {
            //Check if save file exist
            if(!File.Exists(_filePath))
            {
                _saveData = new SaveData();
                _saveData.rank = new int[3];

                return;
            }

            Load();

            return;
        }

        Directory.CreateDirectory(_directoryPath);

        _saveData = new SaveData();
        _saveData.rank = new int[3];
    }

    public void Load()
    {
        string fileString = File.ReadAllText(_filePath);

        if(fileString != "")
        {
            try
            {
                _saveData = JsonConvert.DeserializeObject<SaveData>(fileString);
            }
            catch(Exception e)
            {
                Debug.Log("Exception" + e.Message);
            }

            if (_saveData != null)
                OnLoadGame?.Invoke(this, _saveData);
        }
    }

    public void Save()
    {
        string output = "";

        try
        {
            //Serialize: convert save data into json
            output = JsonConvert.SerializeObject(_saveData);
        }
        catch (Exception e)
        {
            Debug.Log("Exception" + e.Message);
        }

        if(output != "")
        {
            //write save data into file path
            File.WriteAllText(_filePath, output);
        }           
    }

    public void SaveRankData(int [] rank)
    {
        _saveData.rank = rank;
    }

    public void SavePlayerData(string name, PlayerType type)
    {
        _saveData.playerName = name;
        _saveData.playerType = type;
    }

    public void CreateSaveData()
    {
        int[] rank = new int[3];

        rank[0] = 100;

        rank[1] = 300;

        _saveData.playerName = "Inuyasha";
        _saveData.playerType = PlayerType.Boy;

        _saveData.rank = rank;

    }
}
