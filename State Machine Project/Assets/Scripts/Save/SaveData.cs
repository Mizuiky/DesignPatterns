using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Girl,
    Boy
}

[System.Serializable]
public class SaveData
{
    public int [] rank { get; set; }

    public PlayerType playerType { get; set; }

    public string playerName { get; set; }
}
