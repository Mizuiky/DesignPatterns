using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public List<WaveSetup> waveSetup;

    [Header("Spawn Range")]
    public float maxXposition;
    public float minXposition;
    public float maxZposition;
    public float minZposition;

    [SerializeField]
    private int _currentWave;

    [SerializeField]
    private float waveTime;

    public void Start()
    {
        Init();
    }

    public void Update()
    {
        if(waveTime < waveSetup[_currentWave].duration)
        {
            waveTime += Time.deltaTime;
        }
        else
        {
            FinishWave();
        }
    }

    private void Init()
    {

        _currentWave = 0;
        waveTime = 0;

        GameManager.Instance.PoolManager.Init();

        StartWave();
    }

    private void StartWave()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        float nextEnemyTime;

        for (int i = 0; i < waveSetup[_currentWave].numberOfEnemies; i++)
        {
            var enemy = GameManager.Instance.PoolManager.GetEnemy();
            enemy.transform.position = GetRandomPosition();

            nextEnemyTime = GetRandomEnemySpawnTime();

            yield return new WaitForSeconds(nextEnemyTime);
        }
    }


    private Vector3 GetRandomPosition()
    {
        float xRange = Random.Range(minXposition, maxXposition);
        float zRange = Random.Range(minZposition, maxZposition);

        return new Vector3(xRange, -0.04f, zRange);
    }

    private float GetRandomEnemySpawnTime()
    {
        float time = Random.Range(waveSetup[_currentWave].minEnemySpawnTime, waveSetup[_currentWave].maxEnemySpawnTime);
        return time;
    }

    private void FinishWave()
    {
        Debug.Log("finish wave");

        GameManager.Instance.PoolManager.CleanPool();

        _currentWave++;

        if(_currentWave < waveSetup.Count)
            StartWave();
    }
}

[System.Serializable]
public class WaveSetup
{
    public int numberOfEnemies;
    public float duration;
    public float minEnemySpawnTime;
    public float maxEnemySpawnTime;
    public int waveNumber;
}
