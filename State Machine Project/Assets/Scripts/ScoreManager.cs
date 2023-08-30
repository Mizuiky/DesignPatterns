using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public SOINT score;
    private int _currentScore;

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
    }

    public void UpdateScore(int points)
    {
        _currentScore += points;

        score.value = _currentScore;
    }
}
