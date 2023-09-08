using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public SOINT score;
    private int _currentScore;

    private RankSetup[] _rank;

    void Start()
    {
        Init();
    }

    void Update()
    {
        
    }

    private void Init()
    {

        _currentScore = 0;
        score.value = _currentScore;

        GameManager.Instance.SaveManager.OnLoadGame += FillRankList;
    }

    private void FillRankList(object sender, SaveData data)
    {

        for(int i = 0; i < data.rank.Length; i++)
        {
            _rank[i] = data.rank[i];
        }
    }

    private void UpdateScorePoints(int id)
    {

        for(int i = 0; i < _rank.Length; i++)
        {
            if (_rank[i].playerId == id)
                _rank[i].playerId = _currentScore;
        }
    }

    public void IncreaseScore(int points)
    {

        _currentScore += points;

        score.value = _currentScore;
    }
}

public class RankSetup
{
    public string PlayerName;
    public int rankNumber;
    public int playerId;
}


