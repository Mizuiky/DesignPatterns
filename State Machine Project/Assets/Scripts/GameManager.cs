using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private PlayerController player;

    public PlayerController Player { get { return player; } private set { } }

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
