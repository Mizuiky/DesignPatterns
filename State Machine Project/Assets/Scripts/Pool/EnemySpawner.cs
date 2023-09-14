using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public List<SO_WAVE_SETUP> waveSetup;

    [Header("Spawn Range")]
    public float maxXposition;
    public float minXposition;
    public float maxZposition;
    public float minZposition;

    public bool EndWave { get { return _hasFinishedWaves; } set { _hasFinishedWaves = value; } }

    [SerializeField]
    private int _currentWave;

    [SerializeField]
    private float waveTime;

    private bool _hasFinishedWaves;

    public void Start()
    {
        Init();
    }

    public void Update()
    {
        if(!_hasFinishedWaves)
        {
            if (waveTime < waveSetup[_currentWave].duration)
            {
                waveTime += Time.deltaTime;
            }
            else
            {
                FinishWave();
            }
        }      
    }

    private void Init()
    {
        _hasFinishedWaves = false;

        _currentWave = 0;
        waveTime = 0;
    }

    public void Reset()
    {
        Init();

        StartWave();
    }

    public void StartWave()
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
            var spawnedEnemy = GameManager.Instance.PoolManager.GetPoolObject(PoolType.Enemy);

            if(spawnedEnemy != null)
            {
                spawnedEnemy.SetPosition = GetRandomPosition();

                spawnedEnemy.OnActivate();

                nextEnemyTime = GetNextSpawnTime();

                yield return new WaitForSeconds(nextEnemyTime);
            }            
        }
    }

    private Vector3 GetRandomPosition()
    {
        float xRange = Random.Range(minXposition, maxXposition);
        float zRange = Random.Range(minZposition, maxZposition);

        return new Vector3(xRange, -0.04f, zRange);
    }

    private float GetNextSpawnTime()
    {
        float time = Random.Range(waveSetup[_currentWave].minEnemySpawnTime, waveSetup[_currentWave].maxEnemySpawnTime);
        return time;
    }

    private void FinishWave()
    {
        Debug.Log("finish wave");

        _currentWave++;

        if(_currentWave < waveSetup.Count)
        {
            waveTime = 0;
            StartWave();
        }        
        else
        {
            _hasFinishedWaves = true;
            Debug.Log("has finished waves");
        }
    }
}
