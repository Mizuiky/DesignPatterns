using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private UIController uiController;

    [SerializeField]
    private PoolCreator poolManager;

    [SerializeField]
    private ScoreManager scoreManager;

    public PlayerController Player { get { return playerController; } private set { } }

    public UIController UI { get { return uiController; } private set { } }

    public PoolCreator PoolManager { get { return poolManager; } private set { } }

    public ScoreManager ScoreManager { get { return scoreManager; } private set { } }

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
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
}
