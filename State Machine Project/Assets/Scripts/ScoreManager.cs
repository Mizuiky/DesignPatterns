using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    public SOINT score;
    private int _currentScore;

    private int [] _rank;

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

        _rank = new int[3];

        GameManager.Instance.SaveManager.OnLoadGame += FillRank;        
    }

    public void Reset()
    {
        _currentScore = 0;
        score.value = _currentScore;
    }

    private void FillRank(object sender, SaveData data)
    {
        if(data.rank.Length > 0)
        {
            for (int i = 0; i < data.rank.Length; i++)
            {
                _rank[i] = data.rank[i];
            }
        }     
    }

    public int [] GetRankNumbers()
    {

        for (int i = 0; i < _rank.Length; i++)
        {
            if (_rank[i] == default(int))
            {
                _rank[i] = _currentScore;
                break;
            }            
        }

        for(int i = 0; i < _rank.Length; i++)
        {
            if (_currentScore > _rank[i])
            {
                _rank[i] = _currentScore;
                break;
            }                     
        }

        SortRankNumbers();

        return _rank;
    }

    private void SortRankNumbers()
    {
        if(_rank.Length > 1)
        {
            _rank = _rank.OrderByDescending(x => x).ToArray();
        }       
    }

    public void IncreaseScore(int points)
    {

        _currentScore += points;

        score.value = _currentScore;
    }

    public void OnDisable()
    {
        GameManager.Instance.SaveManager.OnLoadGame -= FillRank;
    }
}


