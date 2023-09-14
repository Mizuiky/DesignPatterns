using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    public SOINT score;
    private int _currentScore;

    private Stack<int> rankStack;

    public void Init()
    {

        _currentScore = 0;
        score.value = _currentScore;

        rankStack = new Stack<int>();

        GameManager.Instance.SaveManager.OnLoadGame += FillRank;        
    }

    public void Reset()
    {
        _currentScore = 0;
        score.value = _currentScore;
    }

    private void FillRank(object sender, SaveData data)
    {
    
        if (data.rank.Length > 0)
        {

            for (int i = 0; i < data.rank.Length; i++)
            {
                rankStack.Push(data.rank[i]);
            }
        }     
    }

    public int [] GetRankNumbers()
    {
        rankStack.Push(_currentScore);

        return SortRankNumbers(rankStack.Distinct().ToArray());
    }

    private int [] SortRankNumbers(int [] rank)
    {
        //distinct : to remove duplicate values
        rank = rank.OrderByDescending(x => x).Take(3).ToArray();

        return rank;
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


