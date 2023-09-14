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
                _saveData.rank = new int[1];

                return;
            }

            Load();

            return;
        }

        Directory.CreateDirectory(_directoryPath);

        _saveData = new SaveData();
        _saveData.rank = new int[1];
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
        if(rank.Length > _saveData.rank.Length)
        { 
            int [] array = new int[rank.Length];

            rank.CopyTo(array, 0);

            _saveData.rank = array;
        }
        else
        {
            _saveData.rank = rank;
        }       
    }

    public void SavePlayerData(string name, PlayerType type)
    {
        _saveData.playerName = name;
        _saveData.playerType = type;
    }
}
