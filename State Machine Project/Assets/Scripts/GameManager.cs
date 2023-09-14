using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private PlayerController _playerController;

    [SerializeField]
    private UIController _uiController;

    [SerializeField]
    private PoolCreator _poolManager;

    [SerializeField]
    private ScoreManager _scoreManager;

    [SerializeField]
    private EnemySpawner _spawner;

    private SaveManager _saveManager;

    public PlayerController Player { get { return _playerController; } private set { } }

    public UIController UI { get { return _uiController; } private set { } }

    public PoolCreator PoolManager { get { return _poolManager; } private set { } }

    public ScoreManager ScoreManager { get { return _scoreManager; } private set { } }

    public SaveManager SaveManager { get { return _saveManager; } private set { } }

    public void Awake()
    {
        if (Instance == null) Instance = GetComponent<GameManager>();
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Start()
    {
        Init();
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            GameOver();
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
            _playerController.KillPlayer();
        }
    }

    private void Init()
    {
        _saveManager = new SaveManager();

        _scoreManager.Init();

        _saveManager.Init();

        _playerController.onPlayerDeath += GameOver;

        _spawner.StartWave();      
    }

    private void GameOver()
    {
        var rank = _scoreManager.GetRankNumbers();

        _saveManager.SaveRankData(rank);

        _saveManager.SavePlayerData("Mizuiky", PlayerType.Girl);

        _saveManager.Save();

        _uiController.OpenRankScreen(rank);

    }

    public void ResetGame()
    {
        _uiController.Reset();
        _scoreManager.Reset();
        _poolManager.Reset();
        _spawner.Reset();
        _playerController.Reset();     
    }

    public void OnDisable()
    {
        _playerController.onPlayerDeath -= GameOver;
    }
}
