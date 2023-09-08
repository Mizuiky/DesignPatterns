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
    public RankSetup[] rank;
    public PlayerType playerType;
    public int playerId;
}
