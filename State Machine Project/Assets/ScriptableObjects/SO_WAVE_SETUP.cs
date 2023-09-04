using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Setup")]

public class SO_WAVE_SETUP : ScriptableObject
{
    public int numberOfEnemies;
    public float duration;
    public float minEnemySpawnTime;
    public float maxEnemySpawnTime;
    public int waveNumber;
}
