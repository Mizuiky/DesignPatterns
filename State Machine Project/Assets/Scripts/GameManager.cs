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

    private void Init()
    {
        _saveManager = new SaveManager();
        _saveManager.Init();
    }

    public void Start()
    {
        Init();
    }
}
